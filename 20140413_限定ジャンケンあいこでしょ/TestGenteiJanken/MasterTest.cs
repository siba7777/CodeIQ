﻿using GenteiJanken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestGenteiJanken
{
    
    
    /// <summary>
    ///MasterTest のテスト クラスです。すべての
    ///MasterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class MasterTest
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
        ///IsWin のテスト
        ///</summary>
        [TestMethod()]
        public void IsWinTest()
        {
            Master_Accessor target = new Master_Accessor();
            List<List<string>> inputPatter = new List<List<string>>
            {
                new List<string> { "R", "S"},
                new List<string> { "S", "P"},
                new List<string> { "P", "R"},
                new List<string> { "S", "R"},
                new List<string> { "P", "S"},
                new List<string> { "R", "P"}
            };
            List<bool> expectedList = new List<bool> { true, true, true, false, false, false };
            bool actual;

            for (int i = 0; i < inputPatter.Count; ++i)
            {
                actual = target.IsWin(inputPatter[i][0], inputPatter[i][1]);
                Assert.AreEqual(expectedList[i], actual);
            }
        }

        /// <summary>
        ///IsAiko のテスト
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GenteiJanken.exe")]
        public void IsAikoTest1()
        {
            Master_Accessor target = new Master_Accessor();
            List<List<string>> inputPatter = new List<List<string>>
            {
                new List<string> { "R", "R"},
                new List<string> { "S", "S"},
                new List<string> { "P", "P"},
                new List<string> { "R", "S"},
                new List<string> { "S", "P"},
                new List<string> { "P", "R"}
            };
            List<bool> expectedList = new List<bool> { true, true, true, false, false, false };
            bool actual;

            for (int i = 0; i < inputPatter.Count; ++i)
            {
                actual = target.IsAiko(inputPatter[i][0], inputPatter[i][1]);
                Assert.AreEqual(expectedList[i], actual);
            }
        }
    }
}
