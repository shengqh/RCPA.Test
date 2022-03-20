using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Tools.Distiller
{
  [TestFixture]
  public class TestExtractDistinctResultProcessor
  {
    [Test]
    public void TestKeepDistinctPeptideOnly()
    {
      var spectrum1 = new IdentifiedSpectrum();
      var pep1 = spectrum1.NewPeptide();

      var spectrum2 = new IdentifiedSpectrum();
      var pep2 = spectrum2.NewPeptide();

      var spectrum3 = new IdentifiedSpectrum();
      var pep3 = spectrum3.NewPeptide();

      var spectrum4 = new IdentifiedSpectrum();
      var pep4 = spectrum4.NewPeptide();

      var protein1 = new IdentifiedProtein();
      protein1.Peptides.Add(pep1);
      protein1.Peptides.Add(pep2);

      var protein2 = new IdentifiedProtein();
      protein2.Peptides.Add(pep1);
      protein2.Peptides.Add(pep3);

      var protein3 = new IdentifiedProtein();
      protein3.Peptides.Add(pep2);
      protein3.Peptides.Add(pep4);

      var g1 = new IdentifiedProteinGroup();
      g1.Add(protein1);

      var g2 = new IdentifiedProteinGroup();
      g2.Add(protein2);

      var g3 = new IdentifiedProteinGroup();
      g3.Add(protein3);

      IIdentifiedResult ir = new IdentifiedResult();
      ir.Add(g1);
      ir.Add(g2);
      ir.Add(g3);

      new DistinctResultDistiller().KeepDistinctPeptideOnly(ir);

      Assert.AreEqual(2, ir.Count);
      Assert.AreEqual(g2, ir[0]);
      Assert.AreEqual(g3, ir[1]);

      Assert.AreEqual(1, ir[0].GetPeptides().Count);
      Assert.AreEqual(spectrum3, ir[0].GetPeptides()[0]);

      Assert.AreEqual(1, ir[1].GetPeptides().Count);
      Assert.AreEqual(spectrum4, ir[1].GetPeptides()[0]);
    }
  }
}
