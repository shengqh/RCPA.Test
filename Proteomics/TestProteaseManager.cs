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
      ProteaseManager.LoadFromFile(@"..\..\data\protease.list");
      Assert.IsTrue(ProteaseManager.Registered("Arg-C"));
      Assert.IsTrue(ProteaseManager.Registered("Glu-C-bicarbonate"));
      Assert.IsTrue(ProteaseManager.Registered("Chymotrypsin"));
      Assert.IsTrue(ProteaseManager.Registered("Trypsin"));
      Assert.IsTrue(ProteaseManager.Registered("Trypsin_2"));
      Assert.IsTrue(ProteaseManager.Registered("Glu-C-phosphate"));
      Assert.IsTrue(ProteaseManager.Registered("Asp-N"));
      Assert.IsTrue(ProteaseManager.Registered("CNBr"));
      Assert.IsTrue(ProteaseManager.Registered("Lys-C"));
      Assert.IsTrue(ProteaseManager.Registered("Trypsin_3"));

      Assert.IsFalse(ProteaseManager.Registered("ProteaseNotExist"));

      Protease trypsin = ProteaseManager.GetProteaseByName("Trypsin");
      Assert.AreEqual("Trypsin",trypsin.Name);
      Assert.AreEqual(true, trypsin.IsEndoProtease);
      Assert.AreEqual("RK", trypsin.CleaveageResidues);
      Assert.AreEqual("P", trypsin.NotCleaveResidues);
    }
  }
}
