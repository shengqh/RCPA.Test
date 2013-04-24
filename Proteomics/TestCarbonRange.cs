using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestCarbonRange
  {
    [Test]
    public void TestGetCarbonRange()
    {
      string peptide = "MAAHRRWLLMSFLFLEVILLEAAK";
      Aminoacids aas = new Aminoacids();
      double residueMass = aas.MonoResiduesMass(peptide);
      int expect = aas.AtomCount(peptide, Atom.C);
      CarbonRange carbonRange = new ProteinCarbonRangePredictor().GetCarbonRange(residueMass);
      Assert.Greater(expect, carbonRange.Min);
      Assert.Less(expect, carbonRange.Max);
    }
  }
}
