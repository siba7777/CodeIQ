using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeIq
{
    class GetPrime
    {
        static void Main(string[] args)
        {
            int counter = 0; //�揜�Z�̉�
            List<int> primes = new List<int>();
            primes.Add(2);
            primes.Add(3);
            for (int n = 5; n <= 100000; n += 2)
            {
                //�Ώۂ͊�̂�
                bool wflag = false; //����؂ꂽ���ǂ���
                for (int i = 1; primes[i] * primes[i] <= n; i++)
                {
                    // �Ώۂ̐��̕������ȉ��̑S�Ă̑f���ŏ��Z����
                    counter += 2;
                    if (0 == n % primes[i])
                    { //����؂ꂽ��f���ł͂Ȃ�
                        wflag = true;
                        break;
                    }
                }
                if (!wflag)
                { //�Ō�܂Ŋ���؂�Ȃ�������
                    primes.Add(n); //�f���Ƃ��ĐV���ɓo�^
                    counter++; //�Ō�Ƀ��[�v�ɓ���Ȃ�������
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