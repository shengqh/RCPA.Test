using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumModificationConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumModificationConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("Modification", io.Name);
      io.SetProperty(mph, "O18(2)");
      Assert.AreEqual("O18(2)", io.GetProperty(mph));

      Assert.AreEqual("O18(2)", mph.Modifications);
    }
  }
}