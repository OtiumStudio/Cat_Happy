//
// Created by Jayce Lee on 25. 6. 19.
//

#ifndef REGISTER_H
#define REGISTER_H

#pragma once
#include <mysql.h>

#include "crow.h"

void register_routes(crow::SimpleApp& app, MYSQL* conn);

#endif //REGISTER_H