using NUnit.Framework;

namespace RCPA.Proteomics.MaxQuant
{
  [TestFixture]
  public class TestMaxQuantConverter
  {
    [Test]
    public void TestRead()
    {
      MaxQuantPeptideTextFormat reader = new MaxQuantPeptideTextFormat();
      var spectra = reader.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/All_Phospho (STY)Sites.txt");
      Assert.AreEqual(1, spectra.Count);
      var m = spectra[0].GetMaxQuantItemList();
      Assert.IsNotNull(m);
      Assert.AreEqual(6, m.Count);
      var dsNames = m.GetDatasetNames();
      Assert.IsTrue(dsNames.Contains("0min"));
      Assert.IsTrue(dsNames.Contains("5min"));
      Assert.IsTrue(dsNames.Contains("15min"));
      Assert.IsTrue(dsNames.Contains("30min"));
      Assert.IsTrue(dsNames.Contains("60min"));
      Assert.IsTrue(dsNames.Contains("120min"));

      reader.ResetBySpectra(spectra);
      reader.WriteToFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//All_Phospho (STY)Sites.txt.copy", spectra);
    }
  }
}
