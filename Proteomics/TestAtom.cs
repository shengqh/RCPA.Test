using NUnit.Framework;
using System.Collections.Generic;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestAtom
  {
    [Test]
    public void TestGetMass()
    {
      Dictionary<Atom, int> atomCount = new Dictionary<Atom, int>();
      atomCount[Atom.H] = 2;
      atomCount[Atom.O] = 1;
      Assert.AreEqual(18.01, Atom.GetMonoMass(atomCount), 0.001);
      Assert.AreEqual(18.015, Atom.GetAverageMass(atomCount), 0.001);
    }

  }
}
