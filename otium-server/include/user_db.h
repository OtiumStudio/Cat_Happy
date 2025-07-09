//
// Created by Jayce Lee on 25. 6. 19.
//

//회원가입 API 함수 선언

#ifndef USER_DB_H
#define USER_DB_H

#pragma once
#include <mysql.h>
#include <string>

bool add_user(MYSQL* conn, const std::string& username, const std::string& password);
bool verify_user(const std::string& username, const std::string& password);

#endif //USER_DB_H

