using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenteiJanken
{
    /// <summary>
    /// 限定ジャンケンを統括する
    /// </summary>
    public class Master
    {
        #region 列挙値
        /// <summary>
        /// 勝敗
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// 勝ち
            /// </summary>
            Win,
            /// <summary>
            /// 負け
            /// </summary>
            Lose,
            /// <summary>
            /// あいこ
            /// </summary>
            Even
        }
        #endregion

        #region 定数
        /// <summary>
        /// グー
        /// </summary>
        public const string CardStringGu = "R";

        /// <summary>
        /// チョキ
        /// </summary>
        public const string CardStringTyoki = "S";

        /// <summary>
        /// パー
        /// </summary>
        public const string CardStringPa = "P";
        #endregion

        #region 変数
        /// <summary>
        /// プレイヤーの情報（対戦前の状態）
        /// </summary>
        private List<Player> playerList;

        /// <summary>
        /// 全対戦終了時の状態
        /// </summary>
        private List<BattleScore> battleScoreList;

        /// <summary>
        /// 期待結果
        /// </summary>
        private List<Player> expectedList;
        #endregion

        #region プロパティ
        /// <summary>
        /// 対戦回数
        /// </summary>
        public int BattleNum { get; set; }
        #endregion

        #region コンストラクタ
        #endregion

        #region メソッド
        /// <summary>
        /// プレイヤー毎の手持ちのカード数の初期値と、
        /// 期待結果をCSVファイルから読み込み
        /// </summary>
        /// <param name="path">CSVファイルのパス</param>
        public void ReadInput(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(path);

            this.playerList = new List<Player>();
            this.expectedList = new List<Player>();

            const string SituationPlayer = "default";
            const string SituationExpected = "result";

            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; ++i)
            {
                string[] data = lines[i].Split(',');

                if (data.Length < 6) continue;

                // プレイヤー情報を読み込み
                if (data[5] == SituationPlayer)
                {
                    Player player = new Player(data[4], data[4].Replace("user_", ""));
                    player.SetCard(int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[0]));
                    this.playerList.Add(player);
                }

                // 期待結果を読み込み
                if (data[5] == SituationExpected)
                {
                    Player player = new Player(data[4], data[4].Replace("user_", ""));
                    player.SetCard(int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[0]));
                    this.expectedList.Add(player);
                }
            }
        }

        /// <summary>
        /// 対戦スタート
        /// プレイヤーが２人であることを前提とする
        /// </summary>
        public void BattleStart()
        {
            if (this.playerList.Count < 2) return;

            this.BattleAllPattern(this.playerList[0], this.playerList[1]);
        }

        /// <summary>
        /// プレイヤー２人について、カードを出すパターンを順列で全て列挙して、
        /// 全組み合わせで限定ジャンケンを行い、結果を保存する。
        /// </summary>
        /// <param name="defPlayerA">プレイヤーAの対戦前の情報</param>
        /// <param name="defPlayerB">プレイヤーBの対戦前の情報</param>
        public void BattleAllPattern(Player defPlayerA, Player defPlayerB)
        {
            if (this.playerList.Count < 2) return;

            this.battleScoreList = new List<BattleScore>();

            // 対戦数がプレイヤーのジャンケンカード数より大きい場合は、
            // 対戦数をプレイヤーのジャンケンカード数へ合わせる
            if (defPlayerA.CardList.Length < this.BattleNum ||
                defPlayerB.CardList.Length < this.BattleNum)
            {
                this.BattleNum = defPlayerA.CardList.Length < defPlayerB.CardList.Length
                    ? defPlayerA.CardList.Length : defPlayerB.CardList.Length;
            }

            // カードを出すパターンを順列で生成
            Permutation permA = new Permutation(defPlayerA.CardList);
            permA.GeneratePermutation(this.BattleNum);
            Permutation permB = new Permutation(defPlayerB.CardList);
            permB.GeneratePermutation(this.BattleNum);

            // 順列で生成したカードを出すパターンを全て標準出力
            //permA.PrintPermList();
            //permB.PrintPermList();

            // 生成したパターンについて、全組み合わせで限定ジャンケンを実行
            IEnumerator<List<string>> cardListA = permA.Enumerate();
            while (cardListA.MoveNext())
            {
                List<string> patternA = cardListA.Current;
                IEnumerator<List<string>> cardListB = permB.Enumerate();
                while (cardListB.MoveNext())
                {
                    List<string> patternB = cardListB.Current;

                    BattleScore battleScore = new BattleScore();
                    Player playerA = defPlayerA.DeepCopyTo();
                    Player playerB = defPlayerB.DeepCopyTo();
                    for (int i = 0; i < patternA.Count || i < patternB.Count; ++i)
                    {
                        JankenResult resultA = new JankenResult(patternA[i]);
                        JankenResult resultB = new JankenResult(patternB[i]);
                        resultA.Result = this.Judge(resultA.Card, resultB.Card);    // 勝敗を判定
                        playerA.PopCard(resultA.Card);  // 出したジャンケンカードを削除
                        playerB.PopCard(resultB.Card);  // 出したジャンケンカードを削除
                        // プレイヤーAが勝った時
                        if (resultA.Result == Result.Win)
                        {
                            resultB.Result = Result.Lose;
                            playerA.PushStar();
                            playerB.PopStar();
                        }
                        // プレイヤーAが負けた時（プレイヤーBが勝ち）
                        else if (resultA.Result == Result.Lose)
                        {
                            resultB.Result = Result.Win;
                            playerA.PopStar();
                            playerB.PushStar();
                        }
                        // あいこの時
                        else if (resultA.Result == Result.Even)
                        {
                            resultB.Result = Result.Even;
                        }
                        // 対戦結果を保存
                        playerA.AddJankenResult(resultA);
                        playerB.AddJankenResult(resultB);
                        
                    }
                    // 全対戦結果を保存
                    battleScore.AddPlayerSituation(playerA);
                    battleScore.AddPlayerSituation(playerB);
                    this.battleScoreList.Add(battleScore);
                }
            }
        }

        /// <summary>
        /// あいこの最少回数を取得する
        /// </summary>
        /// <returns>あいこの最少回数</returns>
        public int GetMinAikoCount()
        {
            int min = int.MaxValue;
            foreach (BattleScore battleScore in this.battleScoreList.Where(x => x.EqualsLastSituation(this.expectedList.ToArray())))
            {
                int aikoCount = battleScore.GetAikoCount();
                if (aikoCount < min) min = aikoCount;
            }
            if (min == int.MaxValue) min = 0;   // 期待結果となる対戦結果がひとつも無い場合
            return min;
        }

        /// <summary>
        /// あいこの最少回数となる対戦パターンを全て標準出力
        /// </summary>
        public void PrintBattleScoreMinAiko()
        {
            Console.WriteLine("対戦パターン：");

            int minAikoCount = this.GetMinAikoCount();
            BattleScore[] printList = this.battleScoreList.Where(x => x.EqualsLastSituation(this.expectedList.ToArray()) &&
                                                                        x.GetAikoCount() == minAikoCount).ToArray();
            for (int i = 0; i < printList.Length; ++i)
            {
                Console.WriteLine("[pattern{0}]", i + 1);
                foreach (string line in printList[i].GetPrintList())
                {
                    Console.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// 全対戦パターンを標準出力
        /// </summary>
        public void PrintBattleScoreAll()
        {
            for (int i = 0; i < this.battleScoreList.Count; ++i)
            {
                Console.WriteLine("[pattern{0}]", i + 1);
                foreach (string line in this.battleScoreList[i].GetPrintList())
                {
                    Console.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// ジャンケンの勝敗判定
        /// </summary>
        /// <param name="cardA">Aが出したカード</param>
        /// <param name="cardB">Bが出したカード</param>
        /// <returns>勝敗（A基準、勝はAの勝ち、負はAの負け）</returns>
        private Result Judge(string cardA, string cardB)
        {
            if (IsAiko(cardA, cardB)) return Result.Even;   //あいこ
            if (IsWin(cardA, cardB)) return Result.Win; //勝ち
            return Result.Lose; //負け
        }

        /// <summary>
        /// あいこ判定
        /// </summary>
        /// <param name="cardA">Aが出したカード</param>
        /// <param name="cardB">Bが出したカード</param>
        /// <returns>true:あいこ, false:あいこじゃない</returns>
        private bool IsAiko(string cardA, string cardB)
        {
            if (cardA == cardB) return true;

            return false;
        }

        /// <summary>
        /// 勝敗判定
        /// </summary>
        /// <param name="cardA">Aが出したカード</param>
        /// <param name="cardB">Bが出したカード</param>
        /// <returns>true:Aの勝ち, false:Aの負け</returns>
        private bool IsWin(string cardA, string cardB)
        {
            if (cardA == CardStringGu && cardB == CardStringTyoki) return true;
            if (cardA == CardStringTyoki && cardB == CardStringPa) return true;
            if (cardA == CardStringPa && cardB == CardStringGu) return true;
            return false;
        }
        #endregion

    }
}
