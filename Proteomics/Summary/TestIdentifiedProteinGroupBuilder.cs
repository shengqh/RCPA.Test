using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedProteinGroupBuilder
  {
    [Test]
    public void TestBuild()
    {
      var pep1 = new IdentifiedPeptide(new IdentifiedSpectrum(new SequestFilename("A", 1, 1, 1, ".dta"))) { Sequence = "A" };
      var pep2 = new IdentifiedPeptide(new IdentifiedSpectrum(new SequestFilename("B", 1, 1, 1, ".dta"))) { Sequence = "B" };
      var pep3 = new IdentifiedPeptide(new IdentifiedSpectrum(new SequestFilename("C", 1, 1, 1, ".dta"))) { Sequence = "C" };
      var pep4 = new IdentifiedPeptide(new IdentifiedSpectrum(new SequestFilename("D", 1, 1, 1, ".dta"))) { Sequence = "D" };
      var pep5 = new IdentifiedPeptide(new IdentifiedSpectrum(new SequestFilename("E", 1, 1, 1, ".dta"))) { Sequence = "E" };
      var pep6 = new IdentifiedPeptide(new IdentifiedSpectrum(new SequestFilename("F", 1, 1, 1, ".dta"))) { Sequence = "F" };

      var protein1 = new IdentifiedProtein()
      {
        Peptides = new IIdentifiedPeptide[] { pep1, pep3, pep5, pep6 }.ToList()
      };

      var protein2 = new IdentifiedProtein()
      {
        Peptides = new IIdentifiedPeptide[] { pep2, pep3, pep4 }.ToList()
      };

      //should be removed from final result since all peptides has been included in protein1 and protein2, even one protein contains both peptides
      var protein3 = new IdentifiedProtein()
      {
        Peptides = new IIdentifiedPeptide[] { pep1, pep2 }.ToList()
      };

      //should be removed from final result since all peptides has been included in protein1
      var protein4 = new IdentifiedProtein()
      {
        Peptides = new IIdentifiedPeptide[] { pep1, pep5 }.ToList()
      };

      var actual = new IdentifiedProteinGroupBuilder2().Build(new IIdentifiedProtein[] { protein1, protein2, protein3 }.ToList());
      Assert.AreEqual(2, actual.Count);
      Assert.AreSame(protein1, actual[0][0]);
      Assert.AreSame(protein2, actual[1][0]);
    }
  }
}
