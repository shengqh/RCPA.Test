using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumRankConverter
  {
    [Test]
    public void Test()
    {
      AbstractPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumRankConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      Assert.AreEqual("Rank", io.Name);
      io.SetProperty(mph, "2");
      Assert.AreEqual("2", io.GetProperty(mph));

      Assert.AreEqual(2, mph.Rank);
    }
  }
}