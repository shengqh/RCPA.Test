using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedResult
  {
    [Test]
    public void TestFilter()
    {
      var pro1 = new IdentifiedProtein("P1");
      pro1.Peptides.Add(new IdentifiedPeptide(new IdentifiedSpectrum() { Charge = 1 }));
      pro1.Peptides.Add(new IdentifiedPeptide(new IdentifiedSpectrum() { Charge = 2 }));

      var pro2 = new IdentifiedProtein("P2");
      pro2.Peptides.Add(new IdentifiedPeptide(new IdentifiedSpectrum() { Charge = 3 }));

      var g1 = new IdentifiedProteinGroup();
      g1.Add(pro1);

      var g2 = new IdentifiedProteinGroup();
      g2.Add(pro2);

      IdentifiedResult ir = new IdentifiedResult();
      ir.Add(g1);
      ir.Add(g2);

      Assert.AreEqual(2, ir.Count);
      Assert.AreEqual(3, ir.GetSpectra().Count);

      ir.Filter(m => { return m.Spectrum.Query.Charge > 1; });

      Assert.AreEqual(2, ir.Count);
      Assert.AreEqual(2, ir.GetSpectra().Count);
      ir.GetSpectra().All(m => { return m.Charge > 1; });

      ir.Filter(m => { return m.Spectrum.Query.Charge > 2; });
      Assert.AreEqual(1, ir.Count);
      Assert.AreEqual(1, ir.GetSpectra().Count);
      ir.GetSpectra().All(m => { return m.Charge > 2; });

      Assert.AreEqual("P2", ir[0][0].Name);
    }

    [Test]
    public void TestFilter2()
    {
      var spectrum = new IdentifiedSpectrum();

      var pro1 = new IdentifiedProtein("P1");
      pro1.Peptides.Add(new IdentifiedPeptide(spectrum) { Sequence = "AAAAAAA" });

      var pro2 = new IdentifiedProtein("P2");
      pro2.Peptides.Add(new IdentifiedPeptide(spectrum) { Sequence = "BBBBBBB" });

      var g1 = new IdentifiedProteinGroup();
      g1.Add(pro1);
      g1.Add(pro2);

      IdentifiedResult ir = new IdentifiedResult();
      ir.Add(g1);

      Assert.AreEqual(1, ir.Count);
      Assert.AreEqual(2, ir[0].Count);
      Assert.AreEqual(1, ir.GetSpectra().Count);

      ir.Filter(m =>
      {
        return m.Sequence.Contains('A');
      });

      Assert.AreEqual(1, ir.Count);
      Assert.AreEqual(1, ir[0].Count);
      Assert.AreEqual(1, ir.GetSpectra().Count);
      Assert.AreSame(pro1, ir[0][0]);

      ir.Filter(m =>
      {
        return m.Sequence.Contains('C');
      });

      Assert.AreEqual(0, ir.Count);
    }
  }
}
