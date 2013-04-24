using NUnit.Framework;

namespace RCPA.Proteomics.Raw
{
  [TestFixture]
  public class TestRawScanFilter
  {
    [Test]
    public void TestSetFilter()
    {
      var rsf = new RawScanFilter();

      rsf.Filter = "+ c ESI Full ms [ 400.00-1800.00]";
      Assert.AreEqual(1, rsf.MsLevel);
      Assert.AreEqual("+", rsf.Polarity);
      Assert.AreEqual("Centroided", rsf.SpectrumType);
      Assert.AreEqual("Full", rsf.ScanType);

      rsf.Filter = "- c ESI d Full ms2 1026.70@35.00 [ 270.00-2000.00]";
      Assert.AreEqual(2, rsf.MsLevel);
      Assert.AreEqual("-", rsf.Polarity);
      Assert.AreEqual("Centroided", rsf.SpectrumType);
      Assert.AreEqual("Full", rsf.ScanType);
      Assert.AreEqual(1026.70, rsf.PrecursorMZ, 0.01);
      Assert.AreEqual(35.00, rsf.CollisionEnergy, 0.01);

      rsf.Filter = "+ p NSI d Z ms [ 270.00-2000.00]";
      Assert.AreEqual(1, rsf.MsLevel);
      Assert.AreEqual("+", rsf.Polarity);
      Assert.AreEqual("Profile", rsf.SpectrumType);
      Assert.AreEqual("zoom", rsf.ScanType);

      rsf.Filter = "N + c ESI d Full ms3 999.70@28.00 1026.70@35.00 [ 270.00-2000.00]";
      Assert.AreEqual(3, rsf.MsLevel);
      Assert.AreEqual("+", rsf.Polarity);
      Assert.AreEqual("Centroided", rsf.SpectrumType);
      Assert.AreEqual("Full", rsf.ScanType);
      Assert.AreEqual(1026.70, rsf.PrecursorMZ, 0.01);
      Assert.AreEqual(35.00, rsf.CollisionEnergy, 0.01);

      rsf.Filter = "- p Full ms2 271.00@0.00 [ 80.00-275.00]";
      Assert.AreEqual(2, rsf.MsLevel);
      Assert.AreEqual("-", rsf.Polarity);
      Assert.AreEqual("Profile", rsf.SpectrumType);
      Assert.AreEqual("Full", rsf.ScanType);

      rsf.Filter = "+ p NSI !det Full ms2 842.00@0.00 ecd@5.00 [ 230.00-2000.00]";
      Assert.AreEqual(2, rsf.MsLevel);
      Assert.AreEqual("+", rsf.Polarity);
      Assert.AreEqual("Profile", rsf.SpectrumType);
      Assert.AreEqual("Full", rsf.ScanType);
      Assert.AreEqual(842.00, rsf.PrecursorMZ, 0.01);

      rsf.Filter = "ITMS + c NSI d Full ms2 1167.57@cid35.00 [310.00-2000.00]";
      Assert.AreEqual(2, rsf.MsLevel);
      Assert.AreEqual("+", rsf.Polarity);
      Assert.AreEqual("Centroided", rsf.SpectrumType);
      Assert.AreEqual("Full", rsf.ScanType);
      Assert.AreEqual(1167.57, rsf.PrecursorMZ, 0.01);

      rsf.Filter = "+ c NSI SRM ms2 447.786 [244.164-244.166, 456.317-456.319, 571.343-571.345, 684.428-684.430, 781.480-781.482]";
      Assert.AreEqual(2, rsf.MsLevel);
      Assert.AreEqual("+", rsf.Polarity);
      Assert.AreEqual("Centroided", rsf.SpectrumType);
      Assert.AreEqual("SRM", rsf.ScanType);
      Assert.AreEqual(447.786, rsf.PrecursorMZ, 0.001);
    }
  }
}