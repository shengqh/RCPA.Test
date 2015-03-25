using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedPeptideUtils
  {
    [Test]
    public void TestUniqueCount()
    {
      var protein = new IdentifiedProtein();
      var spectrum = new IdentifiedSpectrum();

      var peptides = new List<IIdentifiedPeptide>();
      peptides.Add(new IdentifiedPeptide(spectrum){
        Sequence = "ILLLAR"
      });

      peptides.Add(new IdentifiedPeptide(spectrum){
        Sequence = "LILIAR"
      });

      Assert.AreEqual(1, IdentifiedPeptideUtils.GetUniquePeptideCount(peptides));

      peptides.Add(new IdentifiedPeptide(new IdentifiedSpectrum())
      {
        Sequence = "LIIIAR"
      });

      Assert.AreEqual(1, IdentifiedPeptideUtils.GetUniquePeptideCount(peptides));
    }
  }
}
