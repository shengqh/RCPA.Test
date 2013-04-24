using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumFileScanConverter
  {
    [Test]
    public void Test()
    {
      AbstractPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumFileScanConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();
      mph.Query.FileScan.ShortFileName = "ABC,1-2";

      Assert.AreEqual("\"File, Scan(s)\"", io.Name);
      Assert.AreEqual("ABC,1 - 2", io.GetProperty(mph));

      io.SetProperty(mph, "DEF,3");
      Assert.AreEqual("DEF,3", mph.Query.FileScan.ShortFileName);
    }
  }
}