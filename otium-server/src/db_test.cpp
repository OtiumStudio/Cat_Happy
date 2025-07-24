//
// Created by Jayce Lee on 25. 7. 2.
//

#include <mysql/mysql.h>
#include <iostream>

int main_test() {
    MYSQL* conn;
    conn = mysql_init(nullptr);

    if (!conn) {
        std::cerr << "MySQL init failed\n";
        return 1;
    }

    const char* host = getenv("DB_HOST");
    const char* user = getenv("DB_USER");
    const char* password = getenv("DB_PASS");
    const char* dbname = getenv("DB_NAME");

    // 연결: 호스트, 유저, 패스워드, DB명, 포트
    if (!mysql_real_connect(conn, host, user, password, dbname, 3306, NULL, 0)) {
        std::cerr << "MySQL connection failed: " << mysql_error(conn) << "\n";
        return 1;
    }

    std::cout << "MySQL 연결 성공!\n";

    mysql_close(conn);
}