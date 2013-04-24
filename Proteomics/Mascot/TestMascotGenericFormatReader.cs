using System.Collections.Generic;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotGenericFormatReader
  {
    [Test]
    public void TestReadFromITMS()
    {
      var reader = new MascotGenericFormatReader<Peak>();
      List<PeakList<Peak>> pls = reader.ReadFromFile(@"..\..\data\DHBS0001_4FR_R01.mgf");
      Assert.AreEqual(2439, pls.Count);
      Assert.AreEqual(1242.05, pls[pls.Count - 1].PrecursorMZ);
      Assert.AreEqual(4629, pls[pls.Count - 1].PrecursorIntensity);
      Assert.AreEqual(0, pls[pls.Count - 1].PrecursorCharge);
    }

    [Test]
    public void TestReadFromMaldiTof()
    {
      var reader = new MascotGenericFormatReader<Peak>();
      List<PeakList<Peak>> pls = reader.ReadFromFile(@"..\..\data\pps_192 LC_1141485233.txt");
      Assert.AreEqual(81, pls.Count);
      Assert.AreEqual(2869.9788, pls[pls.Count - 1].PrecursorMZ);
      Assert.AreEqual(0.0, pls[pls.Count - 1].PrecursorIntensity);
      Assert.AreEqual(1, pls[pls.Count - 1].PrecursorCharge);
    }
  }
}