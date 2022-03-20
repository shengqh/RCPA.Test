using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.IO;

namespace RCPA.Proteomics.IO
{
  [TestFixture]
  public class TestDtaFormat
  {
    private string testDtaFile = @TestContext.CurrentContext.TestDirectory + "/../../../data//20040922_HPPP_Flow_01.0007.0007.2.dta";

    private readonly DtaFormat<Peak> dtaFormat = new DtaFormat<Peak>();

    [Test]
    public void TestReadFromFile()
    {
      PeakList<Peak> spectrum = this.dtaFormat.ReadFromFile(this.testDtaFile);
      Assert.AreEqual(1736.6228, spectrum.PrecursorMZ, 0.001);
      Assert.AreEqual(2, spectrum.PrecursorCharge);
      Assert.AreEqual(23, spectrum.Count);
    }

    [Test]
    public void TestWriteToDta()
    {
      PeakList<Peak> spectrum = new DtaFormat<Peak>().ReadFromFile(this.testDtaFile);
      var fi = new FileInfo(this.testDtaFile);
      string tmpFile = fi.DirectoryName + "\\a.1.1." + spectrum.PrecursorCharge + ".dta";
      try
      {
        this.dtaFormat.WriteToFile(tmpFile, spectrum);

        PeakList<Peak> tmpSpectrum = this.dtaFormat.ReadFromFile(tmpFile);
        Assert.AreEqual(spectrum.PrecursorMZ, tmpSpectrum.PrecursorMZ, 0.001);
        Assert.AreEqual(spectrum.PrecursorCharge, tmpSpectrum.PrecursorCharge);
        Assert.AreEqual(spectrum.Count, tmpSpectrum.Count);
      }
      finally
      {
        new FileInfo(tmpFile).Delete();
      }
    }
  }
}