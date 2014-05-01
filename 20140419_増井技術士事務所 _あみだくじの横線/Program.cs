using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Amida
{
    class Program
    {
        /// <summary>
        /// 指定の入力パターンに対して、
        /// 順列で生成した全パターンのあみだを生成して、
        /// 求めた横線本数の個数を表示する
        /// </summary>
        static void Main()
        {
            const int ExpectedCount = 10;   // 求めたい横線の本数
            List<int> inputList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};    // 入力パターン
            //List<int> inputList = new List<int> { 1, 2, 3, 4, 5, 6, 7 };    // 入力パターン
            //List<int> inputList = new List<int> { 1, 2, 3, 4, 5, 6 };    // 入力パターン
            //List<int> inputList = new List<int> { 1, 2, 3, 4};    // 入力パターン
            List<Amida> amidaList = new List<Amida>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 入力パターンに対する順列を生成
            Permutation perm = new Permutation(inputList.ToArray());
            perm.GeneratePermutation(inputList.Count);

            // 順列で生成したパターンのあみだを生成する
            IEnumerator<List<int>> e = perm.Enumerate();
            while (e.MoveNext())
            {
                List<int> outputList = e.Current;
                Amida amida = new Amida();
                amida.SetInputOutput(inputList, outputList);
                amida.GenerateAmida();
                amidaList.Add(amida);
            }

            // あみだの横線が指定の本数の個数を表示
            Console.WriteLine("{0}", amidaList.Count(x => x.HorizonLineCount == ExpectedCount));

            // 全出力パターンとあみだの横線数を表示
            //amidaList.ForEach(x => Console.WriteLine(x.GetResultString()));

            sw.Stop();
            Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        }
    }
}
