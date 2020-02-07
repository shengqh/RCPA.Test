using System.Collections.Generic;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  [TestFixture]
  public class TestPeptideProphetXmlReader
  {
    [Test]
    public void TestRead()
    {
      List<IIdentifiedSpectrum> items = new PeptideProphetXmlParser()
      {
        TitleParser = new DefaultTitleParser()
      }.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/pepxml.xml");
      Assert.AreEqual(2, items.Count);

      Assert.AreEqual(0.0017, items[0].Probability);
      Assert.AreEqual(2, items[0].Proteins.Count);
      Assert.AreEqual("K.EEALVIQTEMEK.K", items[0].Peptide.Sequence);
      Assert.AreEqual("Standard_Protein_FIT_060222,10", items[0].Query.FileScan.ShortFileName);

      Assert.AreEqual(0.0000, items[1].Probability);
      Assert.AreEqual(1, items[1].Proteins.Count);
      Assert.AreEqual("K.NLAPLGR.A", items[1].Peptide.Sequence);
      Assert.AreEqual("Standard_Protein_FIT_060222,100", items[1].Query.FileScan.ShortFileName);
    }
  }
}