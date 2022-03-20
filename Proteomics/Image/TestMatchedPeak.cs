using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestMatchedPeak
  {
    [Test]
    public void TestInformation()
    {
      MatchedPeak p = new MatchedPeak(1000, 0.0, 2);

      p.DisplayName = "TEST";
      Assert.AreEqual("TEST++   1000.0000", p.Information);

      p.Charge = 1;
      Assert.AreEqual("TEST   1000.0000", p.Information);

      p.DisplayName = null;
      p.PeakType = IonType.PRECURSOR;
      Assert.AreEqual("[MH]   1000.0000", p.Information);

      p.Charge = 2;
      Assert.AreEqual("[MH2]++   1000.0000", p.Information);

      p.PeakType = IonType.B2;
      p.PeakIndex = 5;
      Assert.AreEqual("b5++   1000.0000", p.Information);

      p.Charge = 1;
      Assert.AreEqual("b5   1000.0000", p.Information);
    }
  }
}
