//
// Created by Jayce Lee on 25. 7. 25.
//

//기본 Base64 -> URL-sate Base64 변환 방식
// + -> -
// / -> _
// = -> 제거

//기본 base64 개념 정리
//바이너리 데이터를 아스키 문자로 표현하는 방식이다.
//3바이트=24비트 -> 6비트씩 나눠서 총 4개의 숫자로 변환
//이 숫자들을 인코딩 테이블에서 대응하는 문자로 치환

// [01000001] [01000010] [01000011] → A B C
// =>
// [010000] [010100] [001001] [000011]
// =>
// Q U J D (base64)

#include "base64_util.h"
#include <string>
#include <algorithm>
#include <vector>

using namespace std;

static const char* base64_chars =
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    "abcdefghijklmnopqrstuvwxyz"
    "01234567890+/";

string base64_encode(const string& input) {
    string encoded;
    unsigned char const* bytes = reinterpret_cast<unsigned char const*>(input.c_str());
    //reinterpret_cast 는 c++에서 사용하는 형변환 연산자 중 하나이고, 가장 위험한 축에 속한다.
    //메모리의 비트를 그대로 해석해서 다른 타입으로 바꾸는 것
    //-> 문자열 내부의 바이트 배열을 unsigned char* 포인터로 해석해서 사용한다.
    //-> string은 내부적으로 char 배열이고, base64 인코딩 로직은 바이트 단위로 연산을 해야하기 때문에,
    //-> unsigned char로 다루는 게 편하고 안전하다.
    //-> 바이트 연산에서는 보통 unsigned char를 쓴다.
    int len = input.length();
    int i = 0;
    unsigned char array3[3];
    unsigned char array4[4];

    while (len--) {
        array3[i++] = *(bytes++);
        if (i == 3) {
            array4[0] = (array3[0] & 0xfc) >> 2;
            array4[1] = ((array3[0] & 0x03) << 4) + ((array3[1] & 0xf0) >> 4);
            array4[2] = ((array3[1] & 0x0f) << 2) + ((array3[2] & 0xc0) >> 6);
            array4[3] = array3[2] & 0x3f;

            for (i = 0; i < 4; i++)
                encoded += base64_chars[array4[i]];
            i = 0;
        }
    }

    if (i) {
        for (int j = i; j < 3; j++)
            array3[j] = '\0';

        array4[0] = (array3[0] & 0xfc) >> 2;
        array4[1] = ((array3[0] & 0x03) << 4) + ((array3[1] & 0xf0) >> 4);
        array4[2] = ((array3[1] & 0x0f) << 2) + ((array3[2] & 0xc0) >> 6);
        array4[3] = array3[2] & 0x3f;

        for (int j = 0; j < i + 1; j++)
            encoded += base64_chars[array4[j]];

        while ((i++ < 3))
            encoded += '=';
    }

    return encoded;
}

std::string base64_encode_url(const std::string& input) {
    std::string encoded = base64_encode(input);

    // URL-safe 변환
    for (char& c : encoded) {
        if (c == '+') c = '-';
        else if (c == '/') c = '_';
    }

    // '=' 제거
    encoded.erase(std::remove(encoded.begin(), encoded.end(), '='), encoded.end());
    return encoded;
}