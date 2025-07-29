//
// Created by Jayce Lee on 25. 7. 1.
//

#include "../include/users_handler.hpp"
#include <nlohmann/json.hpp>
#include <string>
#include <unordered_map>
#include <mutex>
#include <unordered_map>
#include <pqxx/pqxx>

extern std::unordered_map<std::string, std::string> users_db;
extern std::mutex user_db_mutex;

#include "crow/http_response.h"

// 임시 사용자 데이터 구조 예시
struct User {
    std::string id;
    std::string password;
    std::string nickname;
    std::string email;
};

static std::unordered_map<std::string, User> user_db; // 임시 DB

// PostgreSQL 기반 사용자 정보 조회
crow::response get_user(pqxx::connection& conn, const crow::request& req, const std::string& user_id) {
    try {
        pqxx::work txn(conn);
        pqxx::result r = txn.exec_params("SELECT id, nickname, email FROM users WHERE id = $1", user_id);
        if (r.empty()) {
            return crow::response(404, "User not found");
        }
        nlohmann::json res;
        res["id"] = r[0][0].as<std::string>();
        res["nickname"] = r[0][1].as<std::string>();
        res["email"] = r[0][2].as<std::string>();
        return crow::response(res.dump());
    } catch (const std::exception& e) {
        return crow::response(500, e.what());
    }
}

// PostgreSQL 기반 사용자 정보 수정
crow::response update_user(pqxx::connection& conn, const crow::request& req, const std::string& user_id) {
    try {
        auto body = crow::json::load(req.body);
        if (!body) {
            return crow::response(400, "Invalid JSON");
        }
        pqxx::work txn(conn);
        if (body.has("password")) {
            txn.exec_params("UPDATE users SET password = $1 WHERE id = $2", std::string(body["password"].s()), user_id);
        }
        if (body.has("nickname")) {
            txn.exec_params("UPDATE users SET nickname = $1 WHERE id = $2", std::string(body["nickname"].s()), user_id);
        }
        if (body.has("email")) {
            txn.exec_params("UPDATE users SET email = $1 WHERE id = $2", std::string(body["email"].s()), user_id);
        }
        txn.commit();
        return crow::response(200, "User updated");
    } catch (const std::exception& e) {
        return crow::response(500, e.what());
    }
}

crow::response logout_user(const crow::request& req) {
    // TODO: 세션/토큰 무효화 로직 추가 필요
    return crow::response(200, "Logged out successfully");
}

void user_routes(crow::SimpleApp& app, pqxx::connection& conn) {
    CROW_ROUTE(app, "/users/<string>")
        .methods("GET"_method)
        ([&conn](const crow::request& req, const std::string& user_id) {
            return get_user(conn, req, user_id);
        });

    CROW_ROUTE(app, "/users/<string>")
        .methods("PUT"_method)
        ([&conn](const crow::request& req, const std::string& user_id) {
            return update_user(conn, req, user_id);
        });
}
