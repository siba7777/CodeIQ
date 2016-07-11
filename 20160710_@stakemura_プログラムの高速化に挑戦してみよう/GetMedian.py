if __name__ == "__main__":

    try:
        values = []
        while True:
            # 標準入力受付
            values.append(int(input()))
            # ソート
            values.sort()

            if len(values) % 2 == 0 :
                print(int((values[len(values) // 2 -1] + values[len(values) // 2]) / 2))
            else :
                print(values[len(values) // 2])

    except EOFError:
        pass