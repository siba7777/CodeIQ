#!/usr/bin/env python
# -*- coding: utf-8 -*-
import time
import decimal

# 等比数列か判定
def is_geometric(l):
    rate = decimal.Decimal(l[1]) / decimal.Decimal(l[0])
    for i in range(1, len(l) - 1):
        if decimal.Decimal(l[i+1]) / decimal.Decimal(l[i]) != rate:
             return False
    return True

# 等差数列か判定
def is_equaldifference(l):
    diff = l[1] - l[0]
    for i in range(1, len(l) - 1):
        if l[i+1] - l[i] != diff:
             return False
    return True

# フィボナッチ数列か判定
def is_fibonacci(l, fiblist):
    v = l[:]
    v.reverse()
    if v[0] not in fiblist:
        return False
    for i in range(1, len(l) - 2):
        if v[i] != v[i+1] + v[i+2]:
             return False
    return True

# n項のフィボナッチ数列値を計算
def fib(n):
    return (4 << n*(3+n)) // ((4 << 2*n) - (2 << n) - 1) & ((2 << n) - 1)

# max_value未満までのフィボナッチ数列生成
def generate_fiblist(max_value):
    fiblist = []
    n = 0
    while True:
        v = fib(n)
        if v < max_value:
            fiblist.append(v)
        else:
            break
        n += 1
    return fiblist

# どの数列か判定
def which_sequence(l, fiblist):
    if len(l) < 3:
        return "x"
    if is_geometric(l):
        return "G"
    elif is_equaldifference(l):
        return "A"
    elif is_fibonacci(l, fiblist):
        return "F"
    else:
        return "x"

if __name__ == "__main__":
    try:
        fiblist = generate_fiblist(10000000000)

        while True:
            #start = time.time()

            l = list(map(int, input().split()))
            print(which_sequence(l, fiblist))

            #elapsed_time = time.time() - start
            #print (("elapsed_time:{0}".format(elapsed_time)) + "[sec]")
    except EOFError:
        pass