//
// Created by Jayce Lee on 25. 6. 19.
//

#ifndef REGISTER_H
#define REGISTER_H

#pragma once
#include <pqxx/pqxx>

#include "crow.h"

void register_routes(crow::SimpleApp& app, pqxx::connection& conn);

#endif //REGISTER_H