using System.Collections.Generic;
using System.Linq;

namespace GenteiJanken
{
    /// <summary>
    /// 一つの対戦が終了した（ジャンケンカードを出し切る or 星カードが0となる）際の、
    /// 全対戦結果の情報を保持
    /// </summary>
    public class BattleScore
    {
        #region 変数
        /// <summary>
        /// 対戦終了時のプレイヤーの状態
        /// </summary>
        private List<Player> playerSituationList;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BattleScore()
        {
            this.playerSituationList = new List<Player>();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// プレイヤーの状態を追加
        /// </summary>
        /// <param name="player">プレイヤーの状態</param>
        public void AddPlayerSituation(Player player)
        {
            this.playerSituationList.Add(player);
        }

        /// <summary>
        /// 全対戦結果を出力
        /// プレイヤーが２人であることを前提とする
        /// </summary>
        /// <returns>対戦パターンの一つ</returns>
        public IEnumerable<string> GetPrintList()
        {
            if (this.playerSituationList.Count < 2) yield break;

            for (int i = 0; i < this.playerSituationList[0].ResultList.Length &&
                            i < this.playerSituationList[1].ResultList.Length; ++i)
            {
                yield return string.Format("{0}{1}",
                    this.playerSituationList[0].ResultList[i].Card,
                    this.playerSituationList[1].ResultList[i].Card);
            }
        }

        /// <summary>
        /// 期待結果と同じか判定
        /// </summary>
        /// <param name="expectedList">期待結果</param>
        /// <returns>true:同じ, false:違う</returns>
        public bool EqualsLastSituation(Player[] expectedList)
        {
            foreach (Player expected in expectedList)
            {
                if (!this.playerSituationList.Exists(x => x.EqualsCard(expected))) return false;
            }
            return true;
        }

        /// <summary>
        /// あいこの数を取得
        /// </summary>
        /// <returns>あいこの数</returns>
        public int GetAikoCount()
        {
            if (this.playerSituationList.Count < 2) return -1;
            int ret = this.playerSituationList[0].ResultList.Where(x => x.Result == Master.Result.Even).Count();
            return ret;
        }
        #endregion
    }
}
