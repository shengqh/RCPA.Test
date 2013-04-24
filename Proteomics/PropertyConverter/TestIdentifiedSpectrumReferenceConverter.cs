using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumReferenceConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumReferenceConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      var mp1 = new IdentifiedPeptide(mph);
      mp1.AddProtein("11111");

      var mp2 = new IdentifiedPeptide(mph);
      mp2.AddProtein("22222");
      mp2.AddProtein("33333");

      Assert.AreEqual("Reference", io.Name);
      Assert.AreEqual("11111 ! 22222/33333", io.GetProperty(mph));

      io.SetProperty(mph, "44444/55555 ! 66666");

      Assert.AreEqual(2, mph.Peptides[0].Proteins.Count);
      Assert.AreEqual("44444", mph.Peptides[0].Proteins[0]);
      Assert.AreEqual("55555", mph.Peptides[0].Proteins[1]);

      Assert.AreEqual(1, mph.Peptides[1].Proteins.Count);
      Assert.AreEqual("66666", mph.Peptides[1].Proteins[0]);
    }
  }
}