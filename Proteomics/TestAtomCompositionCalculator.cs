using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestAtomCompositionCalculator
  {
    [Test]
    public void TestPeptideAtomCompositionCalculator()
    {
      AtomComposition nterm = new AtomComposition("H");
      AtomComposition cterm = new AtomComposition("OH");
      Aminoacids aas = new Aminoacids();
      PeptideAtomCompositionCalculator calc = new PeptideAtomCompositionCalculator(nterm, cterm, aas);

      //Terminal only
      IPeptideInfo terminalInfo = new IdentifiedPeptideInfo("",0.0,0);
      AtomComposition terminalActual = calc.GetAtomComposition(terminalInfo);
      Assert.AreEqual(2, terminalActual[Atom.H]);
      Assert.AreEqual(1, terminalActual[Atom.O]);

      //A:C3H5NO
      IPeptideInfo peptideInfo = new IdentifiedPeptideInfo("A", 0.0, 0);
      AtomComposition peptideActual = calc.GetAtomComposition(peptideInfo);
      Assert.AreEqual(3, peptideActual[Atom.C]);
      Assert.AreEqual(7, peptideActual[Atom.H]);
      Assert.AreEqual(1, peptideActual[Atom.N]);
      Assert.AreEqual(2, peptideActual[Atom.O]);
    }
  }
}
