using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumDeltaScoreConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumDeltaScoreConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("DeltaScore", io.Name);
      io.SetProperty(mph, "0.12");
      Assert.AreEqual("0.12", io.GetProperty(mph));

      Assert.AreEqual(0.12, mph.DeltaScore, 0.01);
    }
  }
}