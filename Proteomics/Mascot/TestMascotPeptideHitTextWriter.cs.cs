using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotPeptideHitTextWriter
  {
    [Test]
    public void TestGetProteinString()
    {
      var mph = new IdentifiedSpectrum();

      var mp1 = new IdentifiedPeptide(mph);
      mp1.AddProtein("P1");

      var mp2 = new IdentifiedPeptide(mph);
      mp2.AddProtein("P2");
      mp2.AddProtein("P3");

      Assert.AreEqual("P1 ! P2/P3", MascotPeptideHitTextWriter.GetProteinString(mph));
    }

    [Test]
    public void TestGetSequenceString()
    {
      var mph = new IdentifiedSpectrum();

      var mp1 = new IdentifiedPeptide(mph);
      mp1.Sequence = "P1";

      var mp2 = new IdentifiedPeptide(mph);
      mp2.Sequence = "P2";

      Assert.AreEqual("P1 ! P2", MascotPeptideHitTextWriter.GetSequenceString(mph));
    }
  }
}