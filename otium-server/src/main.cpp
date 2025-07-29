#include "crow.h"
#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include "../include/register.h"
#include "../include/login_handler.h"
#include "../include/users_handler.hpp"
#include "base64_util.h"
#include "../include/user_db.h"
#include <pqxx/pqxx>

int main() {
    std::string jwt_header = R"({\"alg\":\"HS256\",\"typ\":\"JWT\"})";
    std::string encoded_header = base64_encode_url(jwt_header);

    std::cout << "[JWT 헤더 Base64 URL] " << encoded_header << std::endl;

    // PostgreSQL 접속 정보 환경변수에서 읽기
    const char* host = getenv("DB_HOST");
    const char* user = getenv("DB_USER");
    const char* password = getenv("DB_PASS");
    const char* dbname = getenv("DB_NAME");
    std::stringstream conn_str;
    conn_str << "host=" << (host ? host : "localhost")
             << " user=" << (user ? user : "postgres")
             << " password=" << (password ? password : "")
             << " dbname=" << (dbname ? dbname : "postgres");
    pqxx::connection conn(conn_str.str());
    if (!conn.is_open()) {
        std::cerr << "PostgreSQL connection failed" << std::endl;
        return 1;
    }
    std::cout << "PostgreSQL connected successfully!" << std::endl;

    crow::SimpleApp app;

    register_routes(app, conn);
    login_routes(app, conn);
    user_routes(app, conn);

    // 기본 라우트 - GET
    CROW_ROUTE(app, "/")([](){
        return "Hello, Game Server!";
    });

    // 서버 상태 확인용 - GET / ping
    CROW_ROUTE(app, "/ping")([]() {
        return "pong";
    });

#include <fstream>
#include <sstream>

    // Swagger UI 파일 서빙: /swagger/<파일이름>
    CROW_ROUTE(app, "/swagger/<string>")
    ([](const std::string& file) {
        std::ifstream in("public/swagger/" + file, std::ios::binary);
        if (!in) return crow::response(404);
        std::ostringstream contents;
        contents << in.rdbuf();
        return crow::response(contents.str());
    });

    // Swagger UI 접속용 redirect
    CROW_ROUTE(app, "/docs")([] {
        crow::response res;
        res.code = 301; // 301 Moved Permanently
        res.add_header("Location", "/swagger/index.html");
        return res;
    });

    // openapi.yaml 제공
    CROW_ROUTE(app, "/openapi.yaml")([] {
        std::ifstream in("openapi.yaml");
        if (!in) {
        std::cerr << "[ERROR] openapi.yaml 파일 열기 실패!" << std::endl;
        return crow::response(404);
        }
        if (!in) return crow::response(404);
        std::ostringstream contents;
        contents << in.rdbuf();
        return crow::response(contents.str());
    });


    app.port(18080).multithreaded().run();
    return 0;
}
