# coding: UTF-8
import math
from decimal import *

if __name__ == '__main__':
    getcontext().prec = 400
    
    m_Max = 300
    # n!(順列数)を求める
    perms = [Decimal(1.0)] * (m_Max+1)
    for i in range (2, m_Max+1):
        perms[i] = Decimal(perms[i-1] * i)
        #print(perms[i])
    
    # 期待値の合計を求める
    expecteds = [Decimal(0.0)] * (m_Max+1)
    expecteds[1] = 1.0
    for i in range (2, m_Max+1):
        expecteds[i] = Decimal(expecteds[i-1]) * Decimal(i) + Decimal(perms[i-1])
        #print(expecteds[i])
    
    # 期待値合計をパターン数n!で割って人数毎の期待値を求める
    for i in range (2, m_Max+1):
        expecteds[i] = expecteds[i] / perms[i]
        #print(expecteds[i])
    
    try:
        while True:
            n = int(input())
            for i in range (m_Max, -1, -1):
                if (expecteds[i] <= n):
                    print(i+1)
                    break
    except EOFError:
        pass
