# coding: UTF-8

import math

# 素数判定
def is_prime(q):
    q = abs(q)
    if q == 2: return True
    if q < 2 or q&1 == 0: return False
    return pow(2, q-1, q) == 1

# マップ内X座標取得
def get_MapX(v) :
    x1 = math.ceil(math.sqrt(v))
    if x1 - 1 <= (x1*x1) - v :
        return x1
    else :
        return int(math.fabs((x1*x1) - v + 1))

# マップ内Y座標取得
def get_MapY(v) :
    y1 = math.ceil(math.sqrt(v))
    if (y1*y1) - v <= y1 - 1 :
        return y1
    else :
        return int(math.fabs((y1-1)*(y1-1) - v))

if __name__ == "__main__":

    try:
        while True:
            # 標準入力受付
            inputValue = int(input())

            # 入力値の座標取得
            x = get_MapX(inputValue)
            y = get_MapY(inputValue)

            # 入力値と、全素数との、マップ内距離を計算し、
            # 最小値を更新しながら、距離が最小となる素数を求める
            min_distance = 1111
            results = []

            # 入力値 ± 100000の範囲を走査
            rengeStart = 0
            if 0 < inputValue - 100000 :
                rengeStart = inputValue - 100000
            else :
                rengeStart = 0
            for num in range(rengeStart, inputValue + 100000) :
                if is_prime(num) :
                    x_prime = get_MapX(num)
                    y_prime = get_MapY(num)
                    distance = (x_prime-x)*(x_prime-x) + (y_prime-y)*(y_prime-y)
                    if distance < min_distance :
                        results = []
                        results.append(str(num))
                        min_distance = distance
                    elif distance == min_distance :
                        results.append(str(num))

            # 結果を標準出力
            print(",".join(results))

    except EOFError:
        pass