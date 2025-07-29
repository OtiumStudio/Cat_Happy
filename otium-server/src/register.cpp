//
// Created by Jayce Lee on 25. 6. 19.
//

//회원가입 API 구현

#include "../include/register.h"

#include <pqxx/pqxx>

#include "../include/user_db.h"
#include "crow/app.h"

void register_routes(crow::SimpleApp& app, pqxx::connection& conn) {
    CROW_ROUTE(app, "/register").methods("POST"_method)
    ([&conn](const crow::request& req) {
        std::cout << "[DEBUG] /register 요청 받음, body: " << req.body << std::endl;

        auto body = crow::json::load(req.body);
        if (!body) {
            std::cout << "[DEBUG] JSON 파싱 실패" << std::endl;
            return crow::response(400, "Invalid JSON");
        }
        if (!body || !body.has("username") || !body.has("password")) {
            std::cout << "[DEBUG] username 또는 password 누락" << std::endl;
            return crow::response(400, "Missing username/password");
        }

        std::string username = body["username"].s();
        std::string password = body["password"].s();

        std::cout << "[DEBUG] username: " << username << ", password: " << password << std::endl;

        if (username.empty() || password.empty())
            return crow::response(400, "Empty username or password");

        if (!add_user(conn, username, password)) {
            std::cout << "[DEBUG] add_user 실패" << std::endl;
            return crow::response(409, "User already exists");
        }

        std::cout << "[DEBUG] 회원가입 성공" << std::endl;
        return crow::response(200, "User registered");
    });
}

