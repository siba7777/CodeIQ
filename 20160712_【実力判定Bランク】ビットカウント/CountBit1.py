# coding: UTF-8

from collections import Counter

if __name__ == "__main__":
    try:
        
        while True:
            inputs = input().split(" ")
            m = int(inputs[1])
            count = 0
            for n in range(0, int(inputs[0])+1):
                counter = Counter(bin(int(n)))
                #print(str(n) + " " + str(counter['1']))
                if counter['1'] == m:
                    count += 1
            print(count)
    except EOFError:
        pass