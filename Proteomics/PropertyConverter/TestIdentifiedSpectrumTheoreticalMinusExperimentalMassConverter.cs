using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumTheoreticalMinusExperimentalMassConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io =
        new IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();
      mph.TheoreticalMass = 1000.1234;

      Assert.AreEqual("Diff(MH+)", io.Name);

      io.SetProperty(mph, "0.12340");
      Assert.AreEqual(1000.0000, mph.ExperimentalMass, 0.0001);

      Assert.AreEqual("0.12340", io.GetProperty(mph));
    }
  }
}