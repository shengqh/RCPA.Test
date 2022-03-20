using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.IO
{
  [TestFixture]
  public class TestDta2MgfProcessor
  {
    [Test]
    public void TestMergeSameScan()
    {
      List<PeakList<Peak>> pklList = new List<PeakList<Peak>>();
      pklList.Add(new PeakList<Peak>() { Experimental = "A1", PrecursorCharge = 2, FirstScan = 1 });
      pklList.Add(new PeakList<Peak>() { Experimental = "A1", PrecursorCharge = 3, FirstScan = 1 });

      Dta2MgfProcessor.MergeSameScan(pklList);
      Assert.AreEqual(1, pklList.Count);
      Assert.AreEqual(0, pklList[0].PrecursorCharge);

      pklList.Clear();
      pklList.Add(new PeakList<Peak>() { Experimental = "A1", PrecursorCharge = 2, FirstScan = 0 });
      pklList.Add(new PeakList<Peak>() { Experimental = "A1", PrecursorCharge = 3, FirstScan = 0 });

      Dta2MgfProcessor.MergeSameScan(pklList);
      Assert.AreEqual(2, pklList.Count);
      Assert.AreEqual(2, pklList[0].PrecursorCharge);
      Assert.AreEqual(3, pklList[1].PrecursorCharge);
    }
  }
}
