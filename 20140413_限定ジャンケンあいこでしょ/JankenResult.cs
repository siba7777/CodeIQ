
namespace GenteiJanken
{
    /// <summary>
    /// ジャンケン対戦結果の情報
    /// </summary>
    public class JankenResult
    {
        #region プロパティ
        /// <summary>
        /// 出したジャンケンカード
        /// </summary>
        public string Card { get; set; }

        /// <summary>
        /// 勝敗
        /// </summary>
        public Master.Result Result { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JankenResult()
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="card">出したジャンケンカード</param>
        public JankenResult(string card)
        {
            this.Card = card;
        }
        #endregion
    }
}
