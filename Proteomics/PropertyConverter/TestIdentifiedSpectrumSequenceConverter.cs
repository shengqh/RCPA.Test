using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PropertyConverter
{
  [TestFixture]
  public class TestIdentifiedSpectrumSequenceConverter
  {
    [Test]
    public void Test()
    {
      IPropertyConverter<IdentifiedSpectrum> io = new IdentifiedSpectrumSequenceConverter<IdentifiedSpectrum>();
      var mph = new IdentifiedSpectrum();

      var mp1 = new IdentifiedPeptide(mph);
      mp1.Sequence = "AAAAA";

      var mp2 = new IdentifiedPeptide(mph);
      mp2.Sequence = "BBBBB";

      Assert.AreEqual("Sequence", io.Name);
      Assert.AreEqual("AAAAA ! BBBBB", io.GetProperty(mph));

      io.SetProperty(mph, "CCCCC ! DDDDD ! EEEEE");
      Assert.AreEqual(3, mph.Peptides.Count);
      Assert.AreEqual("CCCCC", mph.Peptides[0].Sequence);
      Assert.AreEqual("DDDDD", mph.Peptides[1].Sequence);
      Assert.AreEqual("EEEEE", mph.Peptides[2].Sequence);
    }
  }
}