cmake_minimum_required(VERSION 3.10)
project(GameServer)

set(CMAKE_CXX_STANDARD 17)

# Crow include 디렉터리 경로
include_directories(crow/include)

include_directories(/opt/homebrew/include)  # ← ASIO 헤더가 설치된 경로

include_directories(${CMAKE_SOURCE_DIR}/third_party)

# MySQL C API
include_directories(/opt/homebrew/include/mysql)
link_directories(/opt/homebrew/lib)

# OpenSSL 경로 (M1/M2 Mac 기준, Intel Mac은 다를 수 있음)
include_directories(/opt/homebrew/opt/openssl/include)
link_directories(/opt/homebrew/opt/openssl/lib)

add_executable(server
        src/main.cpp
        src/login_handler.cpp
        src/register.cpp
        src/user_db.cpp
        include/login_handler.h
        include/register.h
        include/user_db.h
        src/users_handler.cpp
        include/users_handler.hpp
        src/db_test.cpp)

target_link_libraries(server mysqlclient pthread ssl crypto)