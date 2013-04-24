using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumMissCleavageConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumMissCleavageConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("MissCleavage", io.Name);
      io.SetProperty(mph, "1");
      Assert.AreEqual("1", io.GetProperty(mph));

      Assert.AreEqual(1, mph.NumMissedCleavages);
    }
  }
}