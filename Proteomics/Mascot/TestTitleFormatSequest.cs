using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestTitleFormatSequest
  {
    [Test]
    public void TestGetTitle()
    {
      var format = new TitleFormatSequest();
      var pkl = new PeakList<Peak>();
      pkl.Experimental = "TEST";
      pkl.ScanTimes.Add(new ScanTime(4135, 58.89));
      pkl.PrecursorCharge = 2;

      Assert.AreEqual("TEST.4135.4135.2.dta", format.Build(pkl));

      pkl.Annotations.Add("TITLE", "TEST_TITLE");
      Assert.AreEqual("TEST_TITLE", format.Build(pkl));
    }
  }
}