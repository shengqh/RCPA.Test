using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestPeptide
  {
    [Test]
    public void TestAddProtein()
    {
      var pi = new IdentifiedPeptide(new IdentifiedSpectrum());
      pi.AddProtein("AAAAA\tBBBBB");
      Assert.AreEqual(1, pi.Proteins.Count);
      Assert.AreEqual("AAAAA BBBBB", pi.Proteins[0]);

      pi.SetProtein(0, "CCCCC\tDDDDD");
      Assert.AreEqual(1, pi.Proteins.Count);
      Assert.AreEqual("CCCCC DDDDD", pi.Proteins[0]);
    }

    [Test]
    public void TestSetSequence()
    {
      var pi = new IdentifiedPeptide(new IdentifiedSpectrum());
      pi.Sequence = "-.MAS*ESETLNPSAR.I";
      Assert.AreEqual("-.MAS*ESETLNPSAR.I", pi.Sequence);
      Assert.AreEqual("MASESETLNPSAR", pi.PureSequence);

      pi.Sequence = "MAS*ESETLNPSAR";
      Assert.AreEqual("MAS*ESETLNPSAR", pi.Sequence);
      Assert.AreEqual("MASESETLNPSAR", pi.PureSequence);
    }
  }
}