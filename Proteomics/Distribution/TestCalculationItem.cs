using NUnit.Framework;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Distribution
{
  [TestFixture]
  public class TestCalculationItem
  {
    [Test]
    public void TestClassifyPeptideHit()
    {
      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();
      spectra.Add(new IdentifiedSpectrum() { Rank = 1 });
      spectra.Add(new IdentifiedSpectrum() { Rank = 1 });
      spectra.Add(new IdentifiedSpectrum() { Rank = 2 });
      spectra.Add(new IdentifiedSpectrum() { Rank = 2 });

      new IdentifiedPeptide(spectra[0]) { Sequence = "SE*Q" };
      new IdentifiedPeptide(spectra[1]) { Sequence = "SEQ" };
      new IdentifiedPeptide(spectra[2]) { Sequence = "SEQSEQ" };
      new IdentifiedPeptide(spectra[3]) { Sequence = "SEQSEQSEQ" };

      CalculationItem item = new CalculationItem()
      {
        Peptides = spectra.ConvertAll(m => m.Peptide).ToList()
      };


      item.ClassifyPeptideHit(m => m.Spectrum.Rank.ToString(), new string[] { "1", "2" });

      Assert.AreEqual(2, item.Classifications.Count);
      Assert.AreEqual(2, item.Classifications["1"].PeptideCount);
      Assert.AreEqual(1, item.Classifications["1"].UniquePeptideCount);
      Assert.AreEqual(2, item.Classifications["2"].PeptideCount);
      Assert.AreEqual(2, item.Classifications["2"].UniquePeptideCount);
    }

    [Test]
    public void TestClassifyPeptideHit2()
    {
      var ir = new SequestResultTextFormat().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/TestDistributionOption.noredundant");

      CalculationItem item = new CalculationItem()
      {
        Peptides = ir[0][0].GetDistinctPeptides()
      };
      item.ClassifyPeptideHit(m => "G1");
      Assert.AreEqual(1360, item.Classifications["G1"].PeptideCount);
      Assert.AreEqual(24, item.Classifications["G1"].UniquePeptideCount);

      item.Peptides = ir[1][0].GetDistinctPeptides();
      item.ClassifyPeptideHit(m => "G1");
      Assert.AreEqual(5, item.Classifications["G1"].PeptideCount);
      Assert.AreEqual(1, item.Classifications["G1"].UniquePeptideCount);
    }

  }
}
