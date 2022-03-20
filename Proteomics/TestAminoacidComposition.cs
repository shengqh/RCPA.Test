using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestAminoacidComposition
  {
    [Test]
    public void TestConstructionAminoacid()
    {
      AtomComposition ac = new AtomComposition("C5H9NO");
      Assert.AreEqual(5, ac[Atom.C]);
      Assert.AreEqual(9, ac[Atom.H]);
      Assert.AreEqual(1, ac[Atom.N]);
      Assert.AreEqual(1, ac[Atom.O]);
    }

    [Test]
    public void TestIsotopic()
    {
      AtomComposition ac = new AtomComposition("C5(C13)6");
      Assert.AreEqual(5, ac[Atom.C]);
      Assert.AreEqual(6, ac[Atom.C13]);
    }

    [Test]
    public void TestConstructionCompound()
    {
      AtomComposition ac = new AtomComposition("CaCl2");
      Assert.AreEqual(1, ac[Atom.Ca]);
      Assert.AreEqual(2, ac[Atom.Cl]);
    }

    [Test]
    public void TestCompositionToString()
    {
      AtomComposition ac = new AtomComposition("C5H9NO");
      Assert.AreEqual("C5H9NO", ac.ToString());
    }
  }
}
