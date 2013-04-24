using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumExpectValueConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumExpectValueConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("ExpectValue", io.Name);
      io.SetProperty(mph, "0.1234");
      Assert.AreEqual("1.23E-001", io.GetProperty(mph));

      Assert.AreEqual(0.1234, mph.ExpectValue, 0.0001);
    }
  }
}