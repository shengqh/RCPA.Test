using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestTitleFormatProteomeDiscoverer
  {
    [Test]
    public void TestGetTitle()
    {
      var pkl = new PeakList<Peak>();
      pkl.Experimental = "TEST";
      pkl.ScanTimes.Add(new ScanTime(4135, 58.89));
      pkl.PrecursorCharge = 2;

      string actual = new TitleFormatProteomeDiscoverer().Build(pkl);
      Assert.AreEqual("TEST Spectrum4135 scans: 4135", actual);
    }
  }
}