using System;
using System.Collections.Generic;

namespace GenteiJanken
{
    /// <summary>
    /// 順列を生成する
    /// </summary>
    public class Permutation
    {
        #region 変数
        /// <summary>
        /// 順列を求めるパターン
        /// </summary>
        private List<string> patternList;
        /// <summary>
        /// 順列生成後のパターン
        /// </summary>
        List<List<string>> permList;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pattern">順列を求めるパターン</param>
        public Permutation(string[] pattern)
        {
            this.patternList = new List<string>(pattern);
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 順列を生成する
        /// </summary>
        /// <param name="n">いくつで順列を生成するか</param>
        public void GeneratePermutation(int n)
        {
            this.permList = new List<List<string>>();
            this._GeneratePermutation(new List<string>(), this.patternList, n);
        }

        /// <summary>
        /// 順列を生成する
        /// </summary>
        /// <param name="pre">取り出されたパターン</param>
        /// <param name="post">取り出されるパターン</param>
        /// <param name="n">取り出す数</param>
        private void _GeneratePermutation(List<string> pre, List<string> post, int n)
        {
            List<string> rest;
            List<string> now = new List<string>(pre);

            if (0 < n)
            {
                for (int i = 0; i < post.Count; ++i)
                {
                    rest = new List<string>(post);
                    now = new List<string>(pre);
                    now.Add(rest[i]);
                    rest.RemoveAt(i);
                    _GeneratePermutation(now, rest, n - 1);
                }
            }
            else
            {
                if (!this.permList.Exists(perm =>
                    {
                        if (perm.Count != now.Count) return false;
                        for (int i = 0; i < perm.Count; ++i)
                        {
                            if (perm[i] != now[i]) return false;
                        }
                        return true;
                    }))
                this.permList.Add(now);
            }
        }

        /// <summary>
        /// 順列生成後のパターンを取得
        /// </summary>
        public IEnumerator<List<string>> Enumerate()
        {
            return this.permList.GetEnumerator();
        }

        /// <summary>
        /// 順列生成後のパターンを標準出力
        /// </summary>
        public void PrintPermList()
        {
            this.permList.ForEach(x => Console.WriteLine(string.Join(",", x.ToArray())));
        }
        #endregion
    }
}
