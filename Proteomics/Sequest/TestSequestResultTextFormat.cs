using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestResultTextFormat
  {
    [Test]
    public void TestReadFromFile()
    {
      IIdentifiedResult ir = new SequestResultTextFormat().ReadFromFile(@"..\..\data\Standard_Protein_FIT_060222.noredundant");

      Assert.AreEqual(19, ir.Count);

      List<IIdentifiedProtein> prohits = ir.GetProteins();
      Assert.AreEqual(43, prohits.Count);

      List<IIdentifiedSpectrum> pephits = ir.GetSpectra();
      Assert.AreEqual(287, pephits.Count);

      IIdentifiedProtein protein1_1 = ir[0][0];
      Assert.AreEqual(1, protein1_1.GroupIndex);
      Assert.AreEqual("sp|P00489|PHS2_RABIT Glycogen phosphorylase, muscle form (EC 2.4.1.1) (Myophosphorylase) - Oryctolagus cuniculus (Rabbit).", protein1_1.Reference.Trim());
      Assert.AreEqual(97, protein1_1.Peptides.Count);

      IIdentifiedSpectrum pephit = protein1_1.Peptides[0].Spectrum;
      Assert.AreEqual(1689.886, pephit.TheoreticalMH, 0.001);
      Assert.AreEqual(3.8133, pephit.Score, 0.01);
      Assert.AreEqual(0.65, pephit.DeltaScore, 0.01);
      Assert.AreEqual(1602.8, pephit.SpScore, 0.1);
      Assert.AreEqual(1, pephit.SpRank);
      Assert.AreEqual(27, pephit.MatchedIonCount);
      Assert.AreEqual(52, pephit.TheoreticalIonCount);
      Assert.AreEqual("K.ARPEFTLPVHFYGR.V", protein1_1.Peptides[0].Sequence);
      Assert.AreEqual("Standard_Protein_FIT_060222,7066", protein1_1.Peptides[0].Spectrum.Query.FileScan.ShortFileName);
    }
  }
}
