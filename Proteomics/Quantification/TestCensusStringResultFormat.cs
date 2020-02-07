using NUnit.Framework;
using RCPA.Proteomics.Quantification.Census;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestCensusStringResultFormat
  {
    [Test]
    public void Test()
    {
      CensusStringResult cr;

      cr = new CensusStringResultFormat().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/census-g1-out.txt");
      Assert.AreEqual(3, cr.Proteins.Count);

      Assert.AreEqual(271, cr.Proteins[0].Peptides.Count);
    }
  }
}