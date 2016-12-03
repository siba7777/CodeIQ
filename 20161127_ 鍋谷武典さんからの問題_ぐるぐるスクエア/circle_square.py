# coding: utf-8

def generate_square(n):
    """
    n×nのマスを生成します
    マスとともに、値がどの位置にあるかわかる辞書も生成します
    """

    # マップ初期化
    map = [[0 for i in range(n)] for j in range(n)]
    pos = {}

    # 位置初期化
    row = n // 2
    col = n // 2

    num = 1
    map[row][col] = num
    pos[num] = [row, col]
    num += 1

    odd = 3
    while odd <= n :

        # 上辺へ
        row = row - 1
        map[row][col] = num
        pos[num] = [row, col]
        num += 1

        #上辺走査
        for i in range(1, odd - 1):
            col = col - 1
            map[row][col] = num
            pos[num] = [row, col]
            num += 1

        #左辺走査
        for i in range(1, odd):
            row = row + 1
            map[row][col] = num
            pos[num] = [row, col]
            num += 1

        #下辺走査
        for i in range(1, odd):
            col = col + 1
            map[row][col] = num
            pos[num] = [row, col]
            num += 1

        #上辺走査
        for i in range(1, odd):
            row = row - 1
            map[row][col] = num
            pos[num] = [row, col]
            num += 1

        odd = odd + 2

    return map, pos

def get_near(num, map, pos):
    """
    マスに隣接するマスの値を昇順で取得します
    """
    ans = []
    num_pos = pos[num]
    ans.append(map[num_pos[0]-1][num_pos[1]])
    ans.append(map[num_pos[0]+1][num_pos[1]])
    ans.append(map[num_pos[0]][num_pos[1]-1])
    ans.append(map[num_pos[0]][num_pos[1]+1])
    ans.sort()
    ans = [str(i) for i in ans]
    return ','.join(ans)

def main():
    map, pos = generate_square(35)
    while True:
        try:
            print(get_near(int(input()), map, pos))
        except:
            break

if __name__ == '__main__':
    main()
