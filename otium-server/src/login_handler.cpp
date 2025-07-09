//
// Created by Jayce Lee on 25. 6. 19.
//

#include "../include/login_handler.h"
#include "../include/user_db.h"

void login_routes(crow::SimpleApp& app) {
    CROW_ROUTE(app, "/login").methods("POST"_method)
    ([&](const crow::request& req) {
        auto body = crow::json::load(req.body);
        if (!body || !body.has("username") || !body.has("password"))
            return crow::response(400, "Missing username or password");

        std::string username = body["username"].s();
        std::string password = body["password"].s();

        if (!verify_user(username, password))
            return crow::response(401, "Invalid credentials");

        crow::json::wvalue result;
        result["message"] = "Login successful for " + username;
        return crow::response(result);
    });

    CROW_ROUTE(app, "/logout")
        .methods("POST"_method)
        ([](const crow::request& req) {
            // 현재는 세션/토큰 관리가 없기 때문에 그냥 성공 메시지만 반환
            crow::json::wvalue result;
            result["message"] = "Successfully logged out";
            return crow::response(200, result);
        });
}
