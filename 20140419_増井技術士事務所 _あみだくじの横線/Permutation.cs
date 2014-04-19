using System.Collections.Generic;

namespace Amida
{
    /// <summary>
    /// 順列を生成する
    /// </summary>
    class Permutation
    {
        #region 変数
        /// <summary>
        /// 順列を求めるパターン
        /// </summary>
        private List<int> patternList;
        /// <summary>
        /// 順列生成後のパターン
        /// </summary>
        List<List<int>> permList;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pattern">順列を求めるパターン</param>
        public Permutation(int[] pattern)
        {
            this.patternList = new List<int>(pattern);
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 順列を生成する
        /// </summary>
        /// <param name="n">いくつで順列を生成するか</param>
        public void GeneratePermutation(int n)
        {
            this.permList = new List<List<int>>();
            this._GeneratePermutation(new List<int>(), this.patternList, n);
        }

        /// <summary>
        /// 順列を生成する
        /// </summary>
        /// <param name="pre">取り出されたパターン</param>
        /// <param name="post">取り出されるパターン</param>
        /// <param name="n">取り出す数</param>
        private void _GeneratePermutation(List<int> pre, List<int> post, int n)
        {
            //Debug.WriteLine(string.Format("----- pre:{0} post:{1} n:{2} -----", string.Join(",", pre), string.Join(",", post), n));

            List<int> rest;
            List<int> now = new List<int>(pre);

            if (0 < n)
            {
                for (int i = 0; i < post.Count; ++i)
                {
                    rest = new List<int>(post);
                    now = new List<int>(pre);
                    now.Add(rest[i]);
                    rest.RemoveAt(i);

                    //Debug.WriteLine(string.Format("1 :{0}", string.Join(",", rest)));
                    //Debug.WriteLine(string.Format("2 :{0}", string.Join(",", now)));

                    _GeneratePermutation(now, rest, n - 1);
                }
            }
            else
            {
                this.permList.Add(now);
            }
        }

        /// <summary>
        /// 順列生成後のパターンを取得
        /// </summary>
        public IEnumerator<List<int>> Enumerate()
        {
            return this.permList.GetEnumerator();
        }
        #endregion
    }
}
