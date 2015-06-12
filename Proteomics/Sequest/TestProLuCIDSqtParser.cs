using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestProLuCIDSqtParser
  {
    [Test]
    public void TestReadFromFile()
    {
      var parser = new ProLuCIDSqtParser();
      var spectra = parser.ReadFromFile(@"..\..\..\data\sequest.sqt");
      Assert.AreEqual(2, spectra.Count);
    }
  }
}