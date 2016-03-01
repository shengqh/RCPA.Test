using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Snp
{
  [TestFixture]
  public class TestSapPtmTable
  {
    public void TestConstruction()
    {
      var table = new SapPtmTable(@"../../../data/SAP_PTM.txt");
      Assert.AreEqual(13, table.SapPtmMap.Count);
      Assert.AreEqual("Formyl", table.GetModification('T', 'E'));
      Assert.AreEqual("Formyl", table.GetModification('S', 'D'));
      Assert.AreEqual("Acetyl", table.GetModification('S', 'E'));
      Assert.AreEqual("Deamidated", table.GetModification('Q', 'E'));
      Assert.AreEqual("Deamidated", table.GetModification('N', 'D'));
      Assert.AreEqual("Methyl", table.GetModification('D', 'E'));
      Assert.AreEqual("Methyl+Deamidated", table.GetModification('N', 'E'));
      Assert.AreEqual("ethylamino", table.GetModification('T', 'K'));
      Assert.AreEqual("Methyl", table.GetModification('N', 'Q'));
      Assert.AreEqual("Dioxidation", table.GetModification('P', 'E'));
      Assert.AreEqual("Methyl", table.GetModification('S', 'T'));
      Assert.AreEqual("Oxidation", table.GetModification('F', 'Y'));
      Assert.AreEqual("Deoxy", table.GetModification('S', 'A'));
    }
  }
}
