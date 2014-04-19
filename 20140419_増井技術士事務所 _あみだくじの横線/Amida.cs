using System;
using System.Collections.Generic;

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
        /// 左端から右端、右端から左端からそれぞれあみだを生成して、
        /// 横線数は少ない方を採用する
        /// </summary>
        public void GenerateAmida()
        {
            int forward = this.GenerateAmidaForward();
            int backward = this.GenerateAmidaBackward();
            this.HorizonLineCount = forward < backward ? forward : backward;
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
        /// 左端から右端に向かってあみだを生成する
        /// </summary>
        private int GenerateAmidaForward()
        {
            int lineCount = 0;

            List<int> list = new List<int>(this.inputList);

            // 左端から右端に向かって、目的の縦軸までのあみだの横線を引いてゆく
            int start = 0;
            while (start < list.Count - 1)
            {
                // 目的の縦軸を探索
                int goal = this.outputList.IndexOf(list[start]);
                // 目的の縦軸が見つからない場合（outputListの入力ミスがなければありえない）
                if (goal == -1) break;
                // 既に目的地の場合
                if (start == goal)
                {
                    ++start;
                    continue;
                }
                // 目的の縦軸から現在の縦軸まであみだの横線を引いてゆく
                for (int now = start; now < goal; ++now)
                {
                    list = swap(list, now, now + 1);
                    lineCount++;
                }
            }
            return lineCount;
        }

        /// <summary>
        /// 右端から左端に向かってあみだを生成する
        /// </summary>
        private int GenerateAmidaBackward()
        {
            int lineCount = 0;

            List<int> list = new List<int>(this.inputList);

            // 左端から右端に向かって、目的の縦軸までのあみだの横線を引いてゆく
            int start = list.Count - 1;
            while (0 < start)
            {
                // 目的の縦軸を探索
                int goal = this.outputList.IndexOf(list[start]);
                // 目的の縦軸が見つからない場合（outputListの入力ミスがなければありえない）
                if (goal == -1) break;
                // 既に目的地の場合
                if (start == goal)
                {
                    --start;
                    continue;
                }
                // 目的の縦軸から現在の縦軸まであみだの横線を引いてゆく
                for (int now = start; goal < now; --now)
                {
                    list = swap(list, now - 1, now);
                    lineCount++;
                }
            }
            return lineCount;
        }

        /// <summary>
        /// 指定インデックスの値を交換
        /// </summary>
        /// <returns>値交換後のリスト</returns>
        private List<int> swap(List<int> list, int index1, int index2)
        {
            List<int> newList = new List<int>(list);
            newList[index1] = list[index2];
            newList[index2] = list[index1];
            //Debug.WriteLine(string.Format("before          :{0}", string.Join(",", list.ToArray())));
            //Debug.WriteLine(string.Format("after           :{0}", string.Join(",", newList.ToArray())));
            return newList;
        }
        #endregion
    }
}
