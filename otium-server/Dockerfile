FROM ubuntu:22.04

# 필수 패키지 설치
RUN apt-get update && apt-get install -y \
    g++ \
    cmake \
    make \
    libpq-dev \
    libasio-dev \
    libpqxx-dev

# 소스 코드 복사
COPY . /old-server
WORKDIR /old-server

# 빌드 디렉토리 생성 및 빌드
RUN apt-get update && apt-get install -y libpqxx-dev
RUN mkdir -p build && cd build && cmake .. && make

EXPOSE 18080

# 서버 실행 (실행파일 이름에 따라 수정 필요)
CMD ["./build/server"]