using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Amida
{
    /// <summary>
    /// あみだを生成する
    /// </summary>
    class Amida
    {
        #region 変数
        /// <summary>
        /// 入力パターン
        /// </summary>
        private List<int> inputList;
        /// <summary>
        /// 出力（期待）パターン
        /// </summary>
        private List<int> outputList;
        #endregion

        #region プロパティ
        /// <summary>
        /// あみだの横線数
        /// </summary>
        public int HorizonLineCount { get; set; }
        #endregion

        #region コンストラクタ
        #endregion

        #region メソッド
        /// <summary>
        /// 入力パターンと出力パターンを設定する
        /// </summary>
        /// <param name="input">入力パターン</param>
        /// <param name="output">出力パターン</param>
        public void SetInputOutput(List<int> input, List<int> output)
        {
            if (input.Count != output.Count) throw new ArgumentException("入力と出力（期待値）のリストの数は同じである必要があります");
            this.inputList = new List<int>(input);
            this.outputList = new List<int>(output);
        }

        /// <summary>
        /// あみだを生成する
        /// はじめに目的軸までの距離を求めて、
        /// 横線を引きつつ、目的軸までの距離を更新して、
        /// 全軸の距離が0となるまで繰り返す。
        /// </summary>
        public void GenerateAmida()
        {
            this.HorizonLineCount = 0;

            List<int> list = new List<int>(this.inputList.Count);

            // 目的軸までの距離を求める
            // 右方向は＋、左方向は-とする
            for (int i = 0; i < this.inputList.Count; ++i)
            {
                list.Add(this.outputList.IndexOf(this.inputList[i]) - i);
            }

            // 左端から走査して、横線を引ける軸を探し、
            // 横線を引ける所でswapを行う
            int start = 0;
            int last = list.Count - 1;
            while (start < last)
            {
                // 横線を引ける場合
                // 左(index)が右に行きたがっており、
                // 右(index+1)が現状維持もしくは左に行きたがっている場合
                if (0 < list[start] && list[start + 1] <= 0)
                {
                    list = this.swap(list, start, start + 1);
                    ++this.HorizonLineCount;
                    start = 0;
                    last = list.Count - 1;
                }
                // 横線を引けない場合
                else ++start;

                // 横線を引ける場合
                // 左(last-1)が現状維持もしくは右に行きたがっており、
                // 右(last)が左に行きたがっている場合
                if (0 <= list[last-1] && list[last] < 0)
                {
                    list = this.swap(list, last-1, last);
                    ++this.HorizonLineCount;
                    start = 0;
                    last = list.Count - 1;
                }
                // 横線を引けない場合
                else --last;
            }
        }

        /// <summary>
        /// 出力パターンとあみだの横線数を返す
        /// </summary>
        /// <returns>出力パターンとあみだの横線数が記載されている文字列</returns>
        public string GetResultString()
        {
            return string.Format("{0}:{1}", string.Join(",", this.outputList.ToArray()), this.HorizonLineCount);
        }


        /// <summary>
        /// 目的軸までの距離を更新して左右の値を交換する
        /// </summary>
        /// <returns>値交換後のリスト</returns>
        private List<int> swap(List<int> list, int left, int right)
        {
            List<int> newList = new List<int>(list);
            newList[left] = list[right] + 1;
            newList[right] = list[left] - 1;
            //Debug.WriteLine(string.Format("before          :{0}", string.Join(",", list.ToArray())));
            //Debug.WriteLine(string.Format("after           :{0}", string.Join(",", newList.ToArray())));
            return newList;
        }
        #endregion
    }
}
