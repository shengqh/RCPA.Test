using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumTheoreticalMHConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumTheoreticalMHConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();
      mph.IsPrecursorMonoisotopic = true;

      Assert.AreEqual("MH+", io.Name);
      io.SetProperty(mph, "2000.0000");
      Assert.AreEqual("2000.00000", io.GetProperty(mph));

      Assert.AreEqual(2000.0000 - Atom.H.MonoMass + Atom.ElectronMass, mph.TheoreticalMass, 0.0001);
    }
  }
}