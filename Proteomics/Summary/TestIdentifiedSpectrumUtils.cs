using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedSpectrumUtils
  {
    private IIdentifiedSpectrum s1, s2, s3, s4, s5, s6;

    [TestFixtureSetUp]
    public void SetUp()
    {
      s1 = new IdentifiedSpectrum();
      s1.Query.FileScan.ShortFileName = "YEAST_0610_G1_SAX_080811_01,11110";
      s1.Query.Charge = 2;
      s1.Score = 3.0;
      s1.AddPeptide(new IdentifiedPeptide(s1) { Sequence = "R.#VK*PDR.T" });

      s2 = new IdentifiedSpectrum();
      s2.Query.FileScan.ShortFileName = "YEAST_0610_G1_SAX_080811_01,11110";
      s2.Query.Charge = 2;
      s2.Score = 2.0;
      s2.AddPeptide(new IdentifiedPeptide(s2) { Sequence = "R.#VK*PDR.T" });

      s3 = new IdentifiedSpectrum();
      s3.Score = 1.0;
      s3.Query.FileScan.ShortFileName = "YEAST_0610_G1_SAX_080811_01,11110";
      s3.Query.Charge = 3;
      s3.AddPeptide(new IdentifiedPeptide(s3) { Sequence = "R.#VK*PDDEFER.T" });

      s4 = new IdentifiedSpectrum();
      s4.Query.FileScan.ShortFileName = "YEAST_0610_G1_SAX_080811_01,11111";
      s4.Query.Charge = 3;
      s4.Score = 5.0;
      s4.AddPeptide(new IdentifiedPeptide(s4) { Sequence = "R.#VK*PDDSKEIEFER.T" });

      s5 = new IdentifiedSpectrum();
      s5.Query.FileScan.ShortFileName = "YEAST_0610_G1_SAX_080811_01,11111";
      s5.Query.Charge = 3;
      s5.Score = 4.0;
      s5.AddPeptide(new IdentifiedPeptide(s5) { Sequence = "R.#VK*PEESKEIEFER.T" });

      s6 = new IdentifiedSpectrum();
      s6.Query.FileScan.ShortFileName = "YEAST_0610_G1_SAX_080811_01,11111";
      s6.Query.Charge = 2;
      s6.Score = 3.0;
      s6.AddPeptide(new IdentifiedPeptide(s6) { Sequence = "R.#VK*PEESEFER.T" });
    }

    [Test]
    public void KeepTopPeptideFromSameEngineDifferentParameters()
    {
      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>(new IIdentifiedSpectrum[] { s1, s2, s3, s4, s5 });
      IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(spectra);
      Assert.AreEqual(3, spectra.Count);
      Assert.IsTrue(spectra.Contains(s1));
      Assert.IsTrue(spectra.Contains(s3));
      Assert.IsTrue(spectra.Contains(s4));
    }

    [Test]
    public void KeepUnconflictPeptidesFromSameEngineDifferentParameters()
    {
      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>(new IIdentifiedSpectrum[] { s1, s2, s3, s4, s5 });
      IdentifiedSpectrumUtils.KeepUnconflictPeptidesFromSameEngineDifferentParameters(spectra);
      Assert.AreEqual(4, spectra.Count);
      Assert.IsTrue(spectra.Contains(s1));
      Assert.IsTrue(spectra.Contains(s2));
      Assert.IsTrue(spectra.Contains(s3));
      Assert.IsTrue(spectra.Contains(s4));
    }

    [Test]
    public void TestSameEngineDifferentParameters()
    {
      ClassificationOptions co = new ClassificationOptions();
      co.ClassifyByCharge = true;
      co.ClassifyByMissCleavage = true;
      co.ClassifyByModification = true;
      co.ModifiedAminoacids = "STY";
      co.ClassifyByNumProteaseTermini = true;

      var s1 = new MascotPeptideTextFormat().ReadFromFile(@"../../data/deisotopic.peptides");
      IdentifiedSpectrumUtils.RemoveSpectrumWithAmbigiousAssignment(s1);

      s1.ForEach(m => m.Tag = "deisotopic");
      var s2 = new MascotPeptideTextFormat().ReadFromFile(@"../../data/deisotopic-top10.peptides");
      IdentifiedSpectrumUtils.RemoveSpectrumWithAmbigiousAssignment(s2);
      s2.ForEach(m => m.Tag = "deisotopic-top");

      var all = s1.Union(s2).ToList();

      var p1 = new List<IIdentifiedSpectrum>(all);
      IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(p1);

      p1.ForEach(m => m.ClassificationTag = "deisotopic/deisotopic-top");
      var bin1 = co.BuildSpectrumBin(p1);

      var p2 = new List<IIdentifiedSpectrum>(all);
      IdentifiedSpectrumUtils.KeepUnconflictPeptidesFromSameEngineDifferentParameters(p2);

      p2.ForEach(m => m.ClassificationTag = "deisotopic/deisotopic-top");
      var bin2 = co.BuildSpectrumBin(p2);
      bin2.ForEach(m =>
      {
        IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(m.Spectra);

        var n = bin1.Find(a => a.Condition.ToString().Equals(m.Condition.ToString()));
        Assert.AreEqual(m.Spectra.Count, n.Spectra.Count);
        //{
        //  if (m.Condition.ToString().Equals("deisotopic/deisotopic-top; Charge=2; MissCleavage=0; Modification=1; NumProteaseTermini=2"))
        //  {
        //    Assert.IsTrue(n.Spectra.Any(k => k.Query.FileScan.ShortFileName.Equals("20111128_CLi_v_4-2k_2mg_TiO2_iTRAQ,4992")));
        //  }

        //  var diff1 = m.Spectra.Except(n.Spectra).ToList();
        //  Console.WriteLine(m.Condition.ToString() + " : " + diff1.Count.ToString());
        //  diff1.ForEach(k =>
        //  {
        //    var lst = all.FindAll(l => l.Query.FileScan.LongFileName.Equals(k.Query.FileScan.LongFileName));
        //    lst.ForEach(q => Console.WriteLine(q.Query.FileScan.ShortFileName + "\t" + q.Tag + "\t" + q.Score.ToString() + "\t" + q.Sequence));
        //  });
        //}
      });
    }

    [Test]
    public void TestRemoveSameSpectrumWithDifferentCharge()
    {
      s1.Score = 3.0;
      s2.Score = 2.0;
      s3.Score = 2.0;
      s6.Score = 7.0;

      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>(new IIdentifiedSpectrum[] { s1, s2, s3, s4, s5, s6 });
      IdentifiedSpectrumUtils.FilterSameSpectrumWithDifferentCharge(spectra);
      Assert.AreEqual(3, spectra.Count);
      Assert.IsTrue(spectra.Contains(s1));
      Assert.IsTrue(spectra.Contains(s2));
      Assert.IsTrue(spectra.Contains(s6));

      s3.Score = 4.0;
      s6.Score = 3.0;
      spectra = new List<IIdentifiedSpectrum>(new IIdentifiedSpectrum[] { s1, s2, s3, s4, s5, s6 });
      IdentifiedSpectrumUtils.FilterSameSpectrumWithDifferentCharge(spectra);
      Assert.AreEqual(3, spectra.Count);
      Assert.IsTrue(spectra.Contains(s3));
      Assert.IsTrue(spectra.Contains(s4));
      Assert.IsTrue(spectra.Contains(s5));
    }

    [Test]
    public void TestFillProteinInformation()
    {
      var peptides = new MascotPeptideTextFormat().ReadFromFile("../../data/Test.output.xml.FDR0.01.peptides");
      Assert.IsTrue(peptides.All(m => m.Peptide.Proteins.Count == 0));

      IdentifiedSpectrumUtils.FillProteinInformation(peptides, "../../data/Test.output.xml.FDR0.01.peptides.proteins");
      Assert.IsTrue(peptides.All(m => m.Peptide.Proteins.Count > 0));
    }

    [Test]
    public void TestCalculateQValue()
    {
      var peptides = new MascotPeptideTextFormat().ReadFromFile("../../../data/QTOF_Ecoli.LowRes.t.xml.peptides");
      peptides.RemoveAll(m => m.ExpectValue > 0.05 || m.Peptide.PureSequence.Length < 6);
      peptides.ForEach(m => m.FromDecoy = m.Proteins.Any(l => l.Contains("REVERSE_")));

      IdentifiedSpectrumUtils.CalculateQValue(peptides, new ExpectValueFunction(), new TargetFalseDiscoveryRateCalculator());

      Assert.AreEqual(0.0267, peptides[0].QValue, 0.0001);
    }
  }
}
