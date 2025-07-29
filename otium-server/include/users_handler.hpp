//
// Created by Jayce Lee on 25. 7. 1.
//

#ifndef USERS_HANDLER_H
#define USERS_HANDLER_H

#pragma once
#include <crow.h>
#include <pqxx/pqxx>

void user_routes(crow::SimpleApp& app, pqxx::connection& conn);


#endif //USERS_HANDLER_H
