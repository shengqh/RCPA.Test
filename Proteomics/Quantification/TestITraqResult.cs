using NUnit.Framework;
using RCPA.Proteomics.Quantification.ITraq;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestITraqResult
  {
    [Test]
    public void TestToExperimentalScanDictionary()
    {
      IsobaricResult items = new IsobaricResult();
      items.Add(new IsobaricItem() { Experimental = "A1", Scan = new ScanTime(1, 0.0) });
      items.Add(new IsobaricItem() { Experimental = "A1", Scan = new ScanTime(2, 0.0) });
      items.Add(new IsobaricItem() { Experimental = "A2", Scan = new ScanTime(1, 0.0) });
      items.Add(new IsobaricItem() { Experimental = "A2", Scan = new ScanTime(2, 0.0) });
      items.Add(new IsobaricItem() { Experimental = "A2", Scan = new ScanTime(3, 0.0) });

      var dic = items.ToExperimentalScanDictionary();
      Assert.AreEqual(2, dic.Count);
      Assert.AreEqual(2, dic.Values.First().Count);
      Assert.AreEqual(3, dic.Values.Last().Count);
    }
  }
}
