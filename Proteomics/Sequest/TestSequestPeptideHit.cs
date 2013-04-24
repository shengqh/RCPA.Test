using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestPeptideHit
  {
    [Test]
    public void TestDiffOfMassPPM()
    {
      var sp = new IdentifiedSpectrum();
      sp.TheoreticalMH = 1000;
      sp.ExperimentalMH = 999.95;
      sp.Query.Charge = 1;
      Assert.AreEqual(0.05, sp.TheoreticalMinusExperimentalMass, 0.01);
    }

    [Test]
    public void TestSetIons()
    {
      var sp = new IdentifiedSpectrum();
      sp.Ions = "10/20";
      Assert.AreEqual(10, sp.MatchedIonCount);
      Assert.AreEqual(20, sp.TheoreticalIonCount);

      sp.Ions = "10|20";
      Assert.AreEqual(10, sp.MatchedIonCount);
      Assert.AreEqual(20, sp.TheoreticalIonCount);

      Assert.AreEqual("10/20", sp.Ions);
    }
  }
}