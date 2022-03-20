using NUnit.Framework;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedProtein
  {
    [Test]
    public void TestCalculateCoverage()
    {
      IdentifiedProtein protein = new IdentifiedProtein();
      //total 30 amino acids
      protein.Sequence = "ABCDEDFDEFDSESLKJFDJLSLGFGDDGD";

      IdentifiedSpectrum s1 = new IdentifiedSpectrum();
      IdentifiedPeptide p1 = new IdentifiedPeptide(s1);
      p1.Sequence = "B.CDEDF.D";
      protein.Peptides.Add(p1);

      protein.CalculateCoverage();
      Assert.AreEqual(16.67, protein.Coverage, 0.01);

      IdentifiedSpectrum s2 = new IdentifiedSpectrum();
      IdentifiedPeptide p2 = new IdentifiedPeptide(s2);
      p2.Sequence = "F.DSESL.K";
      protein.Peptides.Add(p2);

      protein.CalculateCoverage();
      Assert.AreEqual(33.33, protein.Coverage, 0.01);

      IdentifiedSpectrum s3 = new IdentifiedSpectrum();
      IdentifiedPeptide p3 = new IdentifiedPeptide(s3);
      p3.Sequence = "L.SLGF.G";
      protein.Peptides.Add(p3);

      protein.CalculateCoverage();
      Assert.AreEqual(46.67, protein.Coverage, 0.01);

      IdentifiedSpectrum s4 = new IdentifiedSpectrum();
      IdentifiedPeptide p4 = new IdentifiedPeptide(s4);
      p4.Sequence = "L.SLGFG.D";
      protein.Peptides.Add(p4);

      protein.CalculateCoverage();
      Assert.AreEqual(50.00, protein.Coverage, 0.01);
    }

    [Test]
    public void TestDistinctPeptides()
    {
      IdentifiedProtein protein = new IdentifiedProtein();

      IdentifiedSpectrum sp1 = new IdentifiedSpectrum();
      IdentifiedSpectrum sp2 = new IdentifiedSpectrum();

      protein.Peptides.Add(new IdentifiedPeptide(sp1));
      protein.Peptides.Add(new IdentifiedPeptide(sp1));
      protein.Peptides.Add(new IdentifiedPeptide(sp2));

      Assert.AreEqual(3, protein.Peptides.Count);
      Assert.AreEqual(2, protein.GetSpectra().Count);
      Assert.AreEqual(2, protein.GetDistinctPeptides().Count());
    }
  }
}
