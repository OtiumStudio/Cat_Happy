//
// Created by Jayce Lee on 25. 7. 1.
//

#include "../include/users_handler.hpp"
#include <nlohmann/json.hpp>
#include <string>
#include <unordered_map>
#include <mutex>
#include <unordered_map>

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

crow::response get_user(const crow::request& req, const std::string& user_id) {
    auto it = user_db.find(user_id);
    if (it == user_db.end()) {
        return crow::response(404, "User not found");
    }
    nlohmann::json res;
    res["id"] = it->second.id;
    res["nickname"] = it->second.nickname;
    res["email"] = it->second.email;

    return crow::response(res.dump());
}

crow::response update_user(const crow::request& req, const std::string& user_id) {
    auto it = user_db.find(user_id);
    if (it == user_db.end()) {
        return crow::response(404, "User not found");
    }

    auto body = nlohmann::json::parse(req.body);
    if (body.contains("nickname")) {
        it->second.nickname = body["nickname"];
    }
    if (body.contains("email")) {
        it->second.email = body["email"];
    }

    return crow::response(200, "User updated");
}

crow::response logout_user(const crow::request& req) {
    // TODO: 세션/토큰 무효화 로직 추가 필요
    return crow::response(200, "Logged out successfully");
}

void user_routes(crow::SimpleApp& app) {
    CROW_ROUTE(app, "/users/<string>") //유저 정보 조회
        .methods("GET"_method)
        ([](const crow::request& req, const std::string& user_id) {
            std::lock_guard<std::mutex> lock(user_db_mutex);

            auto it = user_db.find(user_id);
            if (it == user_db.end()) {
                return crow::response(400, "User not found");
            }

            crow::json::wvalue result;
            result["username"] = it->first;

            return crow::response(result);
        });

    CROW_ROUTE(app, "/users/<string>") //유저 정보 수정
        .methods("PUT"_method)
        ([](const crow::request& req, const std::string& user_id) {
            auto body = crow::json::load(req.body);
            if (!body) {
                return crow::response(400, "Invalid JSON");
            }

            std::lock_guard<std::mutex> lock(user_db_mutex);
            auto it = user_db.find(user_id);
            if (it == user_db.end()) {
                return crow::response(400, "User not found");
            }

            auto& user = it->second;

            if (body.has("password")) user.password = std::string(body["password"].s());
            if (body.has("nickname")) user.nickname = std::string(body["nickname"].s());
            if (body.has("email")) user.email = std::string(body["email"].s());

            return crow::response(200, "User updated");
        });
}
