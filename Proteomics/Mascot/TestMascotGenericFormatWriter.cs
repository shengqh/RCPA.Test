using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotGenericFormatWriter
  {
    [Test]
    public void TestGetTitle()
    {
      var pkl = new PeakList<Peak>();
      pkl.Experimental = "TEST";
      pkl.ScanTimes.Add(new ScanTime(4135, 58.89));
      pkl.PrecursorCharge = 2;

      string actual = new MascotGenericFormatWriter<Peak>().GetTitle(pkl);
      Assert.AreEqual("TEST.4135.4135.2.dta", actual);

      pkl.Annotations.Add("TITLE", "TEST_TITLE");
      string actualFromAnnotation = new MascotGenericFormatWriter<Peak>().GetTitle(pkl);
      Assert.AreEqual("TEST_TITLE", actualFromAnnotation);
    }
  }
}