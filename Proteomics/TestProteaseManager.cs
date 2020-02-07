using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestProteaseManager
  {
    [Test]
    public void TestLoadFromFile()
    {
      ProteaseManager.LoadFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//proteases.xml");
      Assert.IsTrue(ProteaseManager.Registered("Trypsin"));
      Assert.IsTrue(ProteaseManager.Registered("Chymotrypsin"));
      Assert.IsTrue(ProteaseManager.Registered("LysC/P+AspC"));

      Assert.IsFalse(ProteaseManager.Registered("ProteaseNotExist"));

      Protease trypsin = ProteaseManager.GetProteaseByName("Trypsin");
      Assert.AreEqual("Trypsin",trypsin.Name);
      Assert.AreEqual(true, trypsin.IsEndoProtease);
      Assert.AreEqual("KR", trypsin.CleaveageResidues);
      Assert.AreEqual("P", trypsin.NotCleaveResidues);
    }
  }
}
