using System;
using System.IO;

namespace GenteiJanken
{
    /// <summary>
    /// 限定ジャンケンを行い
    /// 期待結果と合致する、あいこの最少回数と、その対戦パターンを標準出力する
    /// </summary>
    public class Program
    {
        /// <summary>
        /// メイン処理
        /// </summary>
        public static void Main()
        {
            if (Environment.GetCommandLineArgs().Length < 3)
            {
                Console.WriteLine("引数が足りません");
                return;
            }

            try
            {
                // 引数チェック
                int num;
                if (!int.TryParse(Environment.GetCommandLineArgs()[1], out num))
                {
                    Console.WriteLine("１番目の引数には、対戦回数を数字で設定して下さい");
                    return;
                }
                if (!File.Exists(Environment.GetCommandLineArgs()[2]))
                {
                    Console.WriteLine("２番目の引数で指定されたファイルが存在しません");
                    return;
                }

                Master master = new Master();
                master.BattleNum = num;  // 対戦回数を設定
                master.ReadInput(Environment.GetCommandLineArgs()[2]);  // 持ちカードの初期値と、期待結果をCSVファイルから読み込み
                master.BattleStart();   // 限定じゃんけんスタート
                Console.WriteLine("あいこの最小回数：{0}", master.GetMinAikoCount());    // あいこの最少回数を表示
                //master.PrintBattleScoreAll(); // 対戦パターンを全て表示（あいこの最少回数以外も表示）
                master.PrintBattleScoreMinAiko();   // あいこの最少回数となる対戦パターンを表示 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
