using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumChargeConverter
  {
    [Test]
    public void Test()
    {
      AbstractPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumChargeConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("Charge", io.Name);
      io.SetProperty(mph, "1");
      Assert.AreEqual("1", io.GetProperty(mph));

      Assert.AreEqual(1, mph.Query.Charge);
    }
  }
}