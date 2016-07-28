#!/usr/bin/env python
# -*- coding: utf-8 -*-

import itertools
import time

def senkyo(m, n):
        rest = []
        count = 0
        #最下位候補の得票数設定
        for v in range(1, n // m + 1) :
            rest.append((m - 1, n - v, v, v))

        #最下位候補以外の得票数設定しながらパターン数カウント
        while 0 < len(rest):
            item = rest.pop()

            if 0 < item[0] :
                for v in range(item[2], item[1] // item[0] + 1) :
                    if v % item[3] == 0 :
                        if item[1] - v == 0 :
                            count += 1
                            #print((item[0] - 1, item[1] - v, v, item[3]))
                            #print("↑")
                        elif v <= item[1] - v :
                            rest.append((item[0] - 1, item[1] - v, v, item[3]))
                            #print((item[0] - 1, item[1] - v, v))

        return count

if __name__ == "__main__":
    try:
        while True:
            #start = time.time()

            m, n = map(int, input().split())
            print(senkyo(m, n))

            #elapsed_time = time.time() - start
            #print (("elapsed_time:{0}".format(elapsed_time)) + "[sec]")
    except EOFError:
        pass
