using System.Collections.Generic;
using Amida;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAmida
{
    
    
    /// <summary>
    ///AmidaTest のテスト クラスです。すべての
    ///AmidaTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class AmidaTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///GenerateAmidaForward のテスト
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Amida.exe")]
        public void GenerateAmidaForwardTest()
        {
            Amida_Accessor target = new Amida_Accessor(); // TODO: 適切な値に初期化してください
            int expected = 5;
            List<int> inputs = new List<int> { 1, 2, 3, 4 };
            List<int> outputs = new List<int> { 3, 4, 2, 1 };
            target.SetInputOutput(inputs, outputs);
            int actual = target.GenerateAmidaForward();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///GenerateAmidaForward のテスト
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Amida.exe")]
        public void GenerateAmidaForwardTest2()
        {
            Amida_Accessor target = new Amida_Accessor(); // TODO: 適切な値に初期化してください
            int expected = 3;
            List<int> inputs = new List<int> { 1, 2, 3, 4 };
            List<int> outputs = new List<int> { 3, 2, 1, 4 };
            target.SetInputOutput(inputs, outputs);
            int actual = target.GenerateAmidaForward();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///GenerateAmidaBackward のテスト
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Amida.exe")]
        public void GenerateAmidaBackwardTest()
        {
            Amida_Accessor target = new Amida_Accessor(); // TODO: 適切な値に初期化してください
            int expected = 3;
            List<int> inputs = new List<int> { 1, 2, 3, 4 };
            List<int> outputs = new List<int> { 4, 1, 2, 3 };
            target.SetInputOutput(inputs, outputs);
            int actual = target.GenerateAmidaBackward();
            Assert.AreEqual(expected, actual);
        }
    }
}
