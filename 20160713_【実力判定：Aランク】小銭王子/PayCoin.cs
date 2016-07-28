using System;
using System.Collections.Generic;

namespace CodeIq
{
    class PayCoin
    {
        static List<int> coins = new List<int> { 500, 100, 50, 10, 5, 1 };

        /// <summary>
        /// 再帰で残高を計算しながらパターン数を計算
        /// </summary>
        /// <param name="coin">コインのインデクス</param>
        /// <param name="remain">残高</param>
        /// <returns>パターン数</returns>
        static int CalcPattern(int coin, int remain)
        {
            int count = 0;
            if (remain == 0) return 1;
            if (coins[coin] == 1) return 1;
            for (int i = 0; i <= remain / coins[coin]; i++)
            {
                count += CalcPattern(coin + 1, remain - i * coins[coin]);
            }
            return count;
        }

        static void Main(string[] args)
        {
            int n = 0;
            String line;
            for (; (line = Console.ReadLine()) != null; )
            {
                if (int.TryParse(line, out n))
                {
                    Console.WriteLine(CalcPattern(0, n));
                }
            }
        }
    }
}
