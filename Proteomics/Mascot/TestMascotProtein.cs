using System.Collections.Generic;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotProtein
  {
    [Test]
    public void TestSort()
    {
      var mph1 = new IdentifiedSpectrum();
      var peptide1 = new IdentifiedPeptide(mph1);

      var mph2 = new IdentifiedSpectrum();
      var peptide2 = new IdentifiedPeptide(mph2);

      var mpro1 = new IdentifiedProtein("Protein1");
      mpro1.Peptides.Add(peptide1);
      mpro1.Peptides.Add(peptide2);

      var mpro2 = new IdentifiedProtein("Protein2");
      mpro2.Peptides.Add(peptide1);

      var mpro3 = new IdentifiedProtein("Protein3");
      mpro3.Peptides.Add(peptide2);

      var mpros = new List<IdentifiedProtein>();
      mpros.Add(mpro3);
      mpros.Add(mpro2);
      mpros.Add(mpro1);

      mpros.Sort();

      Assert.AreEqual(mpro1, mpros[0]);
      Assert.AreEqual(mpro2, mpros[1]);
      Assert.AreEqual(mpro3, mpros[2]);
    }
  }
}