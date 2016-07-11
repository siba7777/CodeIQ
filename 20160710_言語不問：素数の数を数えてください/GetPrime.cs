using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeIq
{
    class GetPrime
    {
        static void Main(string[] args)
        {
            int counter = 0; //æœZ‚Ì‰ñ”
            List<int> primes = new List<int>();
            primes.Add(2);
            primes.Add(3);
            for (int n = 5; n <= 100000; n += 2)
            {
                //‘ÎÛ‚ÍŠï”‚Ì‚İ
                bool wflag = false; //Š„‚èØ‚ê‚½‚©‚Ç‚¤‚©
                for (int i = 1; primes[i] * primes[i] <= n; i++)
                {
                    // ‘ÎÛ‚Ì”‚Ì•½•ûªˆÈ‰º‚Ì‘S‚Ä‚Ì‘f”‚ÅœZ‚·‚é
                    counter += 2;
                    if (0 == n % primes[i])
                    { //Š„‚èØ‚ê‚½‚ç‘f”‚Å‚Í‚È‚¢
                        wflag = true;
                        break;
                    }
                }
                if (!wflag)
                { //ÅŒã‚Ü‚ÅŠ„‚èØ‚ê‚È‚©‚Á‚½‚ç
                    primes.Add(n); //‘f”‚Æ‚µ‚ÄV‚½‚É“o˜^
                    counter++; //ÅŒã‚Éƒ‹[ƒv‚É“ü‚ç‚È‚©‚Á‚½•ª
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