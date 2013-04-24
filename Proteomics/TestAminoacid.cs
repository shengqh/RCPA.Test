using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestAminoacid
  {
    [Test]
    public void TestInitialize()
    {
      Aminoacid aa = new Aminoacid();

      Assert.AreEqual(false, aa.Visible);
      aa.Initialize('G', "Glydddddd", 57.02147, 57.05, "Glycine", "C2H3NO", true, "");
      Assert.AreEqual("Gly", aa.ThreeName);
      Assert.AreEqual(57, aa.NominalMass);
      Assert.AreEqual("G Gly 57.0215 57.05 Glycine C2H3NO", aa.ToString());
    }

    [Test]
    public void TestGetAtomMassPercent()
    {
      Aminoacid aa = new Aminoacid();
      aa.Initialize('G', "Glydddddd", 57.02147, 57.05, "Glycine", "C2H3NO", true, "");
      double atomCPercent = aa.GetAtomMassPercent(Atom.C);
      Assert.AreEqual(Atom.C.MonoMass * 2 / aa.MonoMass, atomCPercent, 0.0001);
    }
  }
}
