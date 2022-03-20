using NUnit.Framework;
using RCPA.Converter;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestAnnotationConverter
  {
    [Test]
    public void Test()
    {
      var mph = new IdentifiedSpectrum();

      var io = new AnnotationConverter<IdentifiedSpectrum>("TEST");

      Assert.AreEqual("TEST", io.Name);
      Assert.AreEqual("", io.GetProperty(mph));

      io = new AnnotationConverter<IdentifiedSpectrum>("TEST", "DEFAULT_VALUE");
      Assert.AreEqual("DEFAULT_VALUE", io.GetProperty(mph));

      io.SetProperty(mph, "TEST_VALUE");
      Assert.AreEqual("TEST_VALUE", io.GetProperty(mph));
      Assert.AreEqual("TEST_VALUE", mph.Annotations["TEST"]);
    }
  }
}