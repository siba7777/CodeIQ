using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeIq
{
    class GetPrime
    {
        static void Main(string[] args)
        {
            int counter = 0; //乗除算の回数
            List<int> primes = new List<int>();
            primes.Add(2);
            primes.Add(3);
            for (int n = 5; n <= 100000; n += 2)
            {
                //対象は奇数のみ
                bool wflag = false; //割り切れたかどうか
                for (int i = 1; primes[i] * primes[i] <= n; i++)
                {
                    // 対象の数の平方根以下の全ての素数で除算する
                    counter += 2;
                    if (0 == n % primes[i])
                    { //割り切れたら素数ではない
                        wflag = true;
                        break;
                    }
                }
                if (!wflag)
                { //最後まで割り切れなかったら
                    primes.Add(n); //素数として新たに登録
                    counter++; //最後にループに入らなかった分
                }
            }

            String line;
            for (; (line = Console.ReadLine()) != null; )
            {
                Console.WriteLine(primes.Count(v => v < int.Parse(line)));
            }
        }
    }
}