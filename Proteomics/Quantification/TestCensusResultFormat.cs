using NUnit.Framework;
using RCPA.Proteomics.Quantification.Census;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestCensusResultFormat
  {
    [Test]
    public void Test()
    {
      CensusResult cr;

      cr = new CensusResultFormat(true).ReadFromFile(@"..\..\data\census-g1-out.txt");
      Assert.AreEqual(3, cr.Proteins.Count);

      Assert.AreEqual(271, cr.Proteins[0].Peptides.Count);

      cr = new CensusResultFormat(false).ReadFromFile(@"..\..\data\census-g1-out.txt");
      Assert.AreEqual(2, cr.Proteins.Count);
    }
  }
}