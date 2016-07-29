import itertools
import time

# 実行時間を計測
def calc_time(n, sum):
    start = time.time()

    print(mawari_shogi(n, sum))

    elapsed_time = time.time() - start
    print (("elapsed_time:{0}".format(elapsed_time)) + "[sec]")

# n回振る時のパターン数を事前に準備しておく
def create_result(n):
    result = {}
    for hosu in range(0,32):
        result[hosu] = mawari_shogi(n - 1, hosu)
    return result

def mawari_shogi(n, sum):
    result = 0

    if (4 < n):
        for hosu in deme_pattern:
            result += mawari_shogi(n - 1, sum + hosu)
    elif (4 == n):
        for hosu in deme_pattern:
            result += result_table[(sum + hosu) % 32]
    elif (3 == n) or (2 == n):
        for hosu in deme_pattern:
            result += mawari_shogi(n - 1, sum + hosu)
    elif (1 == n):
        result += len([hosu for hosu in deme_pattern if (sum + hosu) % 8 == 0])

    return result

# 1回振った時の歩数パターンを生成
def create_deme_pattern():
    deme = (0, 1, 5, 10, 20)
    pattern = []

    for kin in itertools.combinations_with_replacement((0, 1, 2, 3, 4), 4):
        hosu = 0
        if kin == (0, 0, 0, 0):
            hosu += 8
        else:
            for i in kin:
                hosu += deme[i]
        pattern.append(hosu)
    pattern.append(0)

    return tuple(set(pattern))

deme_pattern = create_deme_pattern()
result_table = create_result(4)

if __name__ == "__main__":
    try:
        while True:
            print(mawari_shogi(int(input()), 0))
            #calc_time(int(input()), 0)
    except EOFError:
        pass