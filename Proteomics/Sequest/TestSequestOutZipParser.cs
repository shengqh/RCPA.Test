using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestOutZipParser
  {

    [Test]
    public void Run()
    {
      var file = @TestContext.CurrentContext.TestDirectory + "/../../../data//FIT_HPPP_Bound_060622_04.zip";
      SequestOutZipParser parser = new SequestOutZipParser();
      var spectra = parser.ReadFromFile(file);
      Assert.AreEqual(2, spectra.Count);
    }
  }
}
