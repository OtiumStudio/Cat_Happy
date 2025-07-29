//
// Created by Jayce Lee on 25. 6. 19.
//

#ifndef LOGIN_H
#define LOGIN_H

#pragma once
#include "crow.h"
#include <pqxx/pqxx>

void login_routes(crow::SimpleApp& app, pqxx::connection& conn);

#endif //LOGIN_H