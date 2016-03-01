using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestDtaselectResultReader
  {
    [Test]
    public void TestRead()
    {
      DtaselectResult dr = new DtaselectResultReader().ReadFromFile("../../../data/dtaselect.txt");
      Assert.AreEqual(9, dr.Proteins.Count);
      Assert.AreEqual(2, dr.Proteins[0].Peptides.Count);
      Assert.AreEqual(2, dr.Proteins[1].Peptides.Count);
      Assert.AreEqual(1, dr.Proteins[2].Peptides.Count);
      Assert.AreEqual(1, dr.Proteins[3].Peptides.Count);
      Assert.AreEqual(5, dr.Proteins[4].Peptides.Count);
      Assert.AreEqual(2, dr.Proteins[5].Peptides.Count);
      Assert.AreEqual(1, dr.Proteins[6].Peptides.Count);
      Assert.AreEqual(1, dr.Proteins[7].Peptides.Count);
      Assert.AreEqual(1, dr.Proteins[8].Peptides.Count);
    }
  }
}
