using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotGenericFormatSequestWriter
  {
    [Test]
    public void TestGetTitle()
    {
      var pkl = new PeakList<Peak>();
      pkl.Experimental = "TEST";
      pkl.ScanTimes.Add(new ScanTime(4135, 58.89));
      pkl.PrecursorCharge = 2;

      string actual = new MascotGenericFormatSequestWriter<Peak>().GetTitle(pkl);
      Assert.AreEqual("TEST.4135.4135.2.dta", actual);
    }
  }
}