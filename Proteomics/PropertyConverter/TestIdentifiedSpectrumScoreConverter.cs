using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumScoreConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumScoreConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("Score", io.Name);
      io.SetProperty(mph, "32.1");
      Assert.AreEqual("32.1", io.GetProperty(mph));

      Assert.AreEqual(32.1, mph.Score, 0.01);
    }
  }
}