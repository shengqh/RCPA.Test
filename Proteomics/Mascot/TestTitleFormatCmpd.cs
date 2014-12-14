using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestTitleFormatCmpd
  {
    [Test]
    public void TestGetTitle()
    {
      var pkl = new PeakList<Peak>();
      pkl.ScanTimes.Add(new ScanTime(4135, 58.89));
      pkl.PrecursorMZ = 717.87;
      pkl.PrecursorIntensity = 10000;

      string actual = new TitleFormatCmpd().Build(pkl);
      Assert.AreEqual("Cmpd 4135, +MSn(717.87), 58.89 min", actual);
    }
  }
}