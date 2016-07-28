# coding: UTF-8

if __name__ == "__main__":
    try:
        while True:
            n = int(input())
            values = input().split(" ")
            
            count256 = 0
            for i in range(0, n):
                for j in range(0, n):
                    if i != j and int(values[i]) + int(values[j]) == 256:
                        count256 += 1
            print("yes" if 0 < count256 else "no")
    except EOFError:
        pass
