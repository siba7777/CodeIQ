if __name__ == "__main__":

    try:
        values = []
        while True:
            # �W�����͎�t
            values.append(int(input()))
            # �\�[�g
            values.sort()

            if len(values) % 2 == 0 :
                print(int((values[len(values) // 2 -1] + values[len(values) // 2]) / 2))
            else :
                print(values[len(values) // 2])

    except EOFError:
        pass