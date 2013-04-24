using NUnit.Framework;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestPeptideFormat
  {
    SequestPeptideTextFormat format = new SequestPeptideTextFormat();

    [Test]
    public void TestReadPeptideFromFile()
    {
      List<IIdentifiedSpectrum> spl = format.ReadFromFile(@"..\..\data\Standard_Protein_FIT_060222.peptides");
      Assert.AreEqual(287, spl.Count);

      List<IIdentifiedSpectrum> spl2 = format.ReadFromFile(@"..\..\data\Nmix_27_C13.peptides");
      Assert.AreEqual(1093, spl2.Count);
    }

    [Test]
    public void TestParse()
    {
      string spLine =
        "\tJWH_SAX_35_050906,10755\tK.GLEAEATY*PYEGKDGPCR.Y ! K.GLEAEATYPY*EGKDGPCR.Y\t2094.09819\t-1.27181\t2\t1\t2.71\t0.28\t127.4\t34\t15|51\tIPI:IPI00126770.2|SWISS-PROT:Q9R014|TREMBL:Q91XK6|REFSEQ_NP:NP_036137|ENSEMBL: ! IPI:IPI00126770.2|SWISS-PROT:Q9R014|TREMBL:Q91XK6|REFSEQ_NP:NP_036137|ENSEMBL:\tK.GLEAEAT*YPYEGKDGPCR.Y(2.6670,0.0154) ! K.GLEAEATYPY*EGKDGPCR.Y(2.5464,0.0599)\t4.41\t1\t100.01";
      IIdentifiedSpectrum peptide = format.PeptideFormat.ParseString(spLine);
      Assert.AreEqual("JWH_SAX_35_050906", peptide.Query.FileScan.Experimental);
      Assert.AreEqual(10755, peptide.Query.FileScan.FirstScan);
      Assert.AreEqual(10755, peptide.Query.FileScan.LastScan);
      Assert.AreEqual(2, peptide.Peptides.Count);
      Assert.AreEqual("K.GLEAEATY*PYEGKDGPCR.Y", peptide.Peptides[0].Sequence);
      Assert.AreEqual("K.GLEAEATYPY*EGKDGPCR.Y", peptide.Peptides[1].Sequence);
      Assert.AreEqual(2094.09819, peptide.TheoreticalMH);
      Assert.AreEqual(1, peptide.Rank);
      Assert.AreEqual(2.71, peptide.Score, 0.01);
      Assert.AreEqual(0.28, peptide.DeltaScore, 0.01);
      Assert.AreEqual(127.4, peptide.SpScore);
      Assert.AreEqual(34, peptide.SpRank);
      Assert.AreEqual(15, peptide.MatchedIonCount);
      Assert.AreEqual(51, peptide.TheoreticalIonCount);
      Assert.AreEqual(2, peptide.DiffModificationSiteCandidates.Count);
      Assert.AreEqual(1, peptide.NumMissedCleavages);

      Assert.AreEqual("K.GLEAEAT*YPYEGKDGPCR.Y", peptide.DiffModificationSiteCandidates[0].Sequence);
      Assert.AreEqual(2.6670, peptide.DiffModificationSiteCandidates[0].Score);
      Assert.AreEqual(0.0154, peptide.DiffModificationSiteCandidates[0].DeltaScore);

      Assert.AreEqual("K.GLEAEATYPY*EGKDGPCR.Y", peptide.DiffModificationSiteCandidates[1].Sequence);
      Assert.AreEqual(2.5464, peptide.DiffModificationSiteCandidates[1].Score);
      Assert.AreEqual(0.0599, peptide.DiffModificationSiteCandidates[1].DeltaScore);

      Assert.AreEqual(100.01, peptide.MatchedTIC, 0.01);
      Assert.AreEqual(spLine, format.PeptideFormat.GetString(peptide));
    }
  }
}