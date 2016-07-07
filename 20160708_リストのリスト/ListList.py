# coding: UTF-8

if __name__ == "__main__":
    snakes = [[] for i in range(10)]
    print(snakes)
    snakes[1].append('蛇')
    print(snakes)

    snakes1 = [[]] * 10
    print(snakes1)
    snakes1[1].append('蛇')
    print(snakes1)