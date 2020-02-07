using System.Collections.Generic;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.IO
{
  [TestFixture]
  public class TestOmicsMSFormatReader
  {
    [Test]
    public void TestRead()
    {
      var reader = new OmicsMSFormatReader<Peak>();
      List<PeakList<Peak>> pls = reader.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/AGP_100ng_062306_1.RAW.fullms");
      Assert.AreEqual(1511, pls.Count);

      Assert.AreEqual(1, pls[0].ScanTimes[0].Scan);
      Assert.AreEqual(10.0015, pls[0].ScanTimes[0].RetentionTime);
      Assert.AreEqual(1, pls[0].MsLevel);

      for (int i = 0; i < 7; i++)
      {
        Assert.AreEqual(0, pls[i].Count);
      }
      Assert.AreEqual(2, pls[7].Count);

      Assert.AreEqual(1182.14, pls[7][0].Mz);
      Assert.AreEqual(3585.14, pls[7][0].Intensity);
      Assert.AreEqual(3, pls[7][0].Charge);
      Assert.AreEqual(1863.99, pls[7][1].Mz);
      Assert.AreEqual(4304.96, pls[7][1].Intensity);
      Assert.AreEqual(3, pls[7][1].Charge);

      Assert.AreEqual(8756, pls[pls.Count - 1].ScanTimes[0].Scan);
      Assert.AreEqual(64.9718, pls[pls.Count - 1].ScanTimes[0].RetentionTime);
      Assert.AreEqual(1, pls[0].MsLevel);
    }
  }
}