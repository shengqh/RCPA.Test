using System.Collections.Generic;
using NUnit.Framework;

namespace RCPA.Proteomics.PeptideProphet
{
  [TestFixture]
  public class TestPepXmlReader
  {
    [Test]
    public void TestRead()
    {
      SequestPepXmlInfo info = new SequestPepXmlReader().Read(@TestContext.CurrentContext.TestDirectory + "/../../../data//pepxml.xml");
      Assert.AreEqual(@"D:\database\FDR\Standard19_ipi.ARATH.v3.17.nr.0.7_Original_Reversed.fasta", info.SearchDatabase);

      List<SequestPeptideProphetItem> items = info.PeptideProphetItems;
      Assert.AreEqual(2, items.Count);

      Assert.AreEqual(0.0017, items[0].PeptideProphetProbability);
      Assert.AreEqual(2, items[0].NumTotalProteins);
      Assert.AreEqual("K.EEALVIQTEMEK.K", items[0].SearchResult.Sequence);
      Assert.AreEqual("Standard_Protein_FIT_060222,10", items[0].SearchResult.Query.FileScan.ShortFileName);

      Assert.AreEqual(0.0000, items[1].PeptideProphetProbability);
      Assert.AreEqual(1, items[1].NumTotalProteins);
      Assert.AreEqual("K.NLAPLGR.A", items[1].SearchResult.Sequence);
      Assert.AreEqual("Standard_Protein_FIT_060222,100", items[1].SearchResult.Query.FileScan.ShortFileName);
    }
  }
}