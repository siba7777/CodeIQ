using System.Collections.Generic;
using System.Linq;

namespace GenteiJanken
{
    /// <summary>
    /// プレイヤーの情報を保持する
    /// 手持ちのカードを操作する
    /// 対戦結果を保存する
    /// </summary>
    public class Player
    {
        #region 変数
        /// <summary>
        /// 手持ちのジャンケンカード
        /// </summary>
        private List<string> cardList;

        /// <summary>
        /// 対戦時に出したジャンケンカードと結果
        /// </summary>
        private List<JankenResult> resultList;
        #endregion

        #region プロパティ
        /// <summary>
        /// プレイヤーID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// プレイヤー名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 星カードの数
        /// </summary>
        public int Star { get; set; }

        /// <summary>
        /// 手持ちのジャンケンカード
        /// </summary>
        public string[] CardList
        {
            get { return this.cardList.ToArray(); }
        }

        /// <summary>
        /// 対戦時に出したジャンケンカードと結果
        /// </summary>
        public JankenResult[] ResultList
        {
            get { return this.resultList.ToArray(); }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Player(string id, string name)
        {
            this.Id = id;
            this.Name = name;

            this.cardList = new List<string>();
            this.resultList = new List<JankenResult>();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// カードを設定
        /// </summary>
        /// <param name="r">ジャンケンカード（グー）の数</param>
        /// <param name="s">ジャンケンカード（チョキ）の数</param>
        /// <param name="p">ジャンケンカード（パー）の数</param>
        /// <param name="star">星カードの数</param>
        public void SetCard(int r, int s, int p, int star)
        {
            this.SetCardR(r);
            this.SetCardS(s);
            this.SetCardP(p);
            this.SetCardStar(star);
        }

        /// <summary>
        /// ジャンケンカードを削除
        /// </summary>
        /// <param name="card">出したジャンケンカードの種類</param>
        public void PopCard(string card)
        {
            this.cardList.Remove(card);
        }

        /// <summary>
        /// 星カードを貰う
        /// </summary>
        public void PushStar()
        {
            ++this.Star;
        }

        /// <summary>
        /// 星カードを渡す（星カードの数を減らす）
        /// </summary>
        public void PopStar()
        {
            if (this.Star <= 0) return;
            --this.Star;
        }

        /// <summary>
        /// ジャンケンカードの数を取得する
        /// </summary>
        /// <param name="card">ジャンケンカードの種類</param>
        /// <returns></returns>
        public int GetCardNum(string card)
        {
            return this.cardList.FindAll(x => x == card).Count;
        }

        /// <summary>
        /// 対戦時に出したジャンケンカードと結果を保存
        /// </summary>
        /// <param name="result">ジャンケン結果</param>
        public void AddJankenResult(JankenResult result)
        {
            this.resultList.Add(result);
        }

        /// <summary>
        /// 対戦可能か判定
        /// </summary>
        /// <returns>true:対戦可, false:対戦不可</returns>
        public bool IsBattlePossible()
        {
            if (this.Star == 0) return false;
            if (this.cardList.Count == 0) return false;
            return true;
        }

        /// <summary>
        /// 手持ちのカードの枚数が、指定のプレイヤーの状態と同じか判定
        /// </summary>
        /// <param name="player">プレイヤーの状態</param>
        /// <returns>true:同じ, false:違う</returns>
        public bool EqualsCard(Player player)
        {
            return this.GetCardNum(Master.CardStringGu) == player.GetCardNum(Master.CardStringGu) &&
                    this.GetCardNum(Master.CardStringTyoki) == player.GetCardNum(Master.CardStringTyoki) &&
                    this.GetCardNum(Master.CardStringPa) == player.GetCardNum(Master.CardStringPa) &&
                    this.Star == player.Star;
        }

        /// <summary>
        /// プレイヤーの情報をディープコピー
        /// 但し、対戦結果はコピーしない
        /// </summary>
        /// <returns>プレイヤー</returns>
        public Player DeepCopyTo()
        {
            Player copy = new Player(this.Id, this.Name);
            copy.SetCard(this.cardList.Count(x => x == Master.CardStringGu),
                        this.cardList.Count(x => x == Master.CardStringTyoki),
                        this.cardList.Count(x => x == Master.CardStringPa),
                        this.Star);
            return copy;
        }

        /// <summary>
        /// グーのカードを設定
        /// </summary>
        /// <param name="r">グーのカードの枚数</param>
        private void SetCardR(int r)
        {
            for (int i = 0; i < r; ++i)
            {
                this.cardList.Add(Master.CardStringGu);
            }
        }

        /// <summary>
        /// チョキのカードを設定
        /// </summary>
        /// <param name="s">チョキのカードの枚数</param>
        private void SetCardS(int s)
        {
            for (int i = 0; i < s; ++i)
            {
                this.cardList.Add(Master.CardStringTyoki);
            }
        }

        /// <summary>
        /// パーのカードを設定
        /// </summary>
        /// <param name="p">パーのカードの枚数</param>
        private void SetCardP(int p)
        {
            for (int i = 0; i < p; ++i)
            {
                this.cardList.Add(Master.CardStringPa);
            }
        }

        /// <summary>
        /// 星のカードを設定
        /// </summary>
        /// <param name="star">星のカードの枚数</param>
        private void SetCardStar(int star)
        {
            this.Star = star;
        }
        #endregion
    }
}
