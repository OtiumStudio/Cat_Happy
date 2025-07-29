//
// Created by Jayce Lee on 25. 6. 19.
//

//사용자 데이터 저장 및 검증 구현

#include "../include/user_db.h"

#include <iostream>
#include <pqxx/pqxx>

// PostgreSQL 기반 사용자 추가
bool add_user(pqxx::connection& conn, const std::string& username, const std::string& password) {
    try {
        pqxx::work txn(conn);
        txn.exec_params(
            "INSERT INTO users (username, password) VALUES ($1, $2)",
            username, password
        );
        txn.commit();
        return true;
    } catch (const pqxx::unique_violation& e) {
        return false;
    } catch (const std::exception& e) {
        std::cerr << "Error inserting user: " << e.what() << std::endl;
        return false;
    }
}

// PostgreSQL 기반 사용자 검증
bool verify_user(pqxx::connection& conn, const std::string& username, const std::string& password) {
    try {
        pqxx::work txn(conn);
        pqxx::result r = txn.exec_params(
            "SELECT password FROM users WHERE username = $1",
            username
        );
        if (r.empty()) return false;
        return r[0][0].as<std::string>() == password;
    } catch (const std::exception& e) {
        std::cerr << "Error verifying user: " << e.what() << std::endl;
        return false;
    }
}