//
// Created by Jayce Lee on 25. 6. 19.
//

//사용자 데이터 저장 및 검증 구현

#include "../include/user_db.h"

#include <iostream>
#include <unordered_map>
#include <mutex>
#include <mysql.h>
#include <ostream>


//임시로 서버에 데이터 저장해주는 코드
std::unordered_map<std::string, std::string> user_db;
std::mutex user_db_mutex;

bool add_user(MYSQL* conn, const std::string& username, const std::string& password) {
    char escaped_username[256];
    char escaped_password[256];

    mysql_real_escape_string(conn, escaped_username, username.c_str(), username.length());
    mysql_real_escape_string(conn, escaped_password, password.c_str(), password.length());

    std::string query = "INSERT INTO users (username, password) VALUES ('" + std::string(escaped_username) + "', '" + std::string(escaped_password) + "')";

    if (mysql_query(conn, query.c_str())) {
        if (std::string(mysql_error(conn)).find("Duplicate") != std::string::npos) {
            return false;
        }
        std::cerr << "Error inserting user " << mysql_error(conn) << std::endl;
        return false;
    }
    return true;
}

bool verify_user(const std::string& username, const std::string& password) {
    std::lock_guard<std::mutex> lock(user_db_mutex);
    auto it = user_db.find(username);
    if (it == user_db.end()) return false;
    return it->second == password;
}