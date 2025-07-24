#include "crow.h"
#include <fstream>
#include <mysql/mysql.h>
#include <sstream>
#include "../include/register.h"
#include "../include/login_handler.h"
#include "../include/users_handler.hpp"
#include "../include/user_db.h"

int main() {
    MYSQL* conn = mysql_init(nullptr);
    if (!conn) {
        std::cerr << "MySQL initialization failed" << std::endl;
        return 1;
    }

    const char* host = getenv("DB_HOST");
    const char* user = getenv("DB_USER");
    const char* password = getenv("DB_PASS");
    const char* dbname = getenv("DB_NAME");

    if (!mysql_real_connect(conn, host, user, password, dbname, 3306, nullptr, 0)) {
        std::cerr << "MySQL connection failed: " << mysql_error(conn) << std::endl;
        return 1;
    }

    std::cout << "MySQL connected successfully!" << std::endl;

    crow::SimpleApp app;

    register_routes(app, conn);
    login_routes(app);
    user_routes(app);

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
        if (!in) return crow::response(404);
        std::ostringstream contents;
        contents << in.rdbuf();
        return crow::response(contents.str());
    });


    app.port(18080).multithreaded().run();
    mysql_close(conn);
    return 0;

}
