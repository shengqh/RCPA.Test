using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotDatParser : MascotDatSpectrumParser
  {
    [Test]
    public void TestGetSourceFile()
    {
      Assert.AreEqual("dyckall.asc", MascotDatSpectrumParser.GetSourceFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//F001264.dat"));
    }

    [Test]
    public void TestSectionClass()
    {
      SectionClass sc = new SectionClass("parameters");
      Assert.IsTrue(sc.IsStartLine("Content-Type: application/x-Mascot; name=\"parameters\""));
    }

    [Test]
    public void TestModification()
    {
      using (var sr = new StreamReader(@TestContext.CurrentContext.TestDirectory + "/../../../data//F001264.dat"))
      {
        Dictionary<string, string> parameters = base.ParseSection(sr, "masses");
        MascotModification mm = base.ParseModification(parameters);

        Assert.AreEqual(4, mm.DynamicModification.Count);
        Assert.AreEqual("42.010559 Acetyl (Protein N-term)", mm.DynamicModification[0].ToString());
        Assert.AreEqual("8.014206 Lysine-13C615N2 (K-full) (K)", mm.DynamicModification[1].ToString());
        Assert.AreEqual("15.994919 Oxidation (M)", mm.DynamicModification[2].ToString());
        Assert.AreEqual("79.966324 Phospho (STY)", mm.DynamicModification[3].ToString());

        Assert.AreEqual(3, mm.StaticModification.Count);
        Assert.AreEqual("127.063332 SMA (K)", mm.StaticModification[0].ToString());
        Assert.AreEqual("128.071154 SMA (N-term)", mm.StaticModification[1].ToString());
        Assert.AreEqual("15.994920 Oxidation (M)", mm.StaticModification[2].ToString());
      }
    }

    [Test]
    public void TestModifySequence()
    {
      String modification = "00100200300";
      string seq = ModifySequence("ABCDEFGHI", modification);
      Assert.AreEqual("AB*CDE#FGH@I", seq);
    }

    class TempTitleParser : ITitleParser
    {
      #region ITitleParser Members

      public string FormatName
      {
        get { return "TEMP"; }
      }

      public string Example
      {
        get { return "dp210198c                      21-Jan-98 DERIVED SPECTRUM    #9"; }
      }

      #endregion

      #region IParser<string,SequestFilename> Members

      public SequestFilename GetValue(string obj)
      {
        return new SequestFilename();
      }

      #endregion

      #region ITitleParser Members

      public bool IsMatch(string title)
      {
        return false;
      }

      #endregion
    }

    [Test]
    public void TestParsePeptide()
    {
      base.TitleParser = new TempTitleParser();
      List<IIdentifiedSpectrum> peptides = base.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/F001264.dat");
      Assert.AreEqual(4, peptides.Count);

      Assert.AreEqual(1, peptides[0].Query.QueryId);
      Assert.AreEqual(1, peptides[0].NumMissedCleavages);
      Assert.AreEqual(1341.736420, peptides[0].TheoreticalMass, 0.000001);
      Assert.AreEqual(18.48, peptides[0].Score);
      Assert.AreEqual(1, peptides[0].Proteins.Count);
      Assert.IsTrue(peptides[0].Proteins.Contains("IPI:IPI00002993.1"));
      Assert.AreEqual(1341.785448, peptides[0].ExperimentalMass, 0.000001);
      Assert.AreEqual(671.9000000, peptides[0].Query.ObservedMz, 0.000001);
      Assert.AreEqual(2, peptides[0].Query.Charge);
      Assert.AreEqual("dp210198c                      21-Jan-98 DERIVED SPECTRUM    #9", peptides[0].Query.Title);
      Assert.AreEqual(2, peptides[0].Peptides.Count);
      Assert.AreEqual("K.AS&TSAGRIT&VPR.L", peptides[0].Peptides[0].Sequence);
      Assert.AreEqual("K.AST&SAGRIT&VPR.L", peptides[0].Peptides[1].Sequence);
      Assert.AreEqual("79.966324 Phospho (STY)", peptides[0].Modifications);
      Assert.AreEqual(0.1, peptides[0].DeltaScore, 0.01);
      Assert.AreEqual(2, peptides[1].Query.QueryId);
      Assert.AreEqual(16, peptides[1].Proteins.Count);
      Assert.IsTrue(peptides[1].Proteins.Contains("IPI:IPI00003865.1"));
      Assert.IsTrue(peptides[1].Proteins.Contains("IPI:IPI00845339.1"));

      Assert.AreEqual(2, peptides[1].Peptides.Count);
      Assert.AreEqual("R.TTPSYVAFTDTER.Q", peptides[1].Peptides[0].Sequence);
      Assert.AreEqual("R.TTPSYVAFTDTER.L", peptides[1].Peptides[1].Sequence);
      Assert.AreEqual(2, peptides[1].Peptides[0].Proteins.Count);
      Assert.AreEqual(14, peptides[1].Peptides[1].Proteins.Count);
      Assert.AreEqual("IPI:IPI00003865.1", peptides[1].Peptides[0].Proteins[0]);
      Assert.AreEqual("IPI:IPI00845339.1", peptides[1].Peptides[0].Proteins[1]);
    }

    [Test]
    public void TestParseQuery()
    {
      using (var sr = new StreamReader(@TestContext.CurrentContext.TestDirectory + "/../../../data//F001264.dat"))
      {
        //parse header
        Dictionary<string, string> headers = base.ParseSection(sr, "header");
        int queryCount = int.Parse(headers["queries"]);

        //parse query
        Dictionary<int, MascotQueryItem> queries = base.ParseQueryItems(sr, queryCount);
        Assert.AreEqual(4, queries.Count);
        Assert.AreEqual(1, queries[1].QueryId);
        Assert.AreEqual(1341.785448, queries[1].ExperimentalMass, 0.000001);
        Assert.AreEqual(671.9000000, queries[1].Observed, 0.000001);
        Assert.AreEqual(2, queries[1].Charge);
        Assert.AreEqual(15095, queries[1].MatchCount);
      }
    }

    [Test]
    public void TestParseSection()
    {
      using (var sr = new StreamReader(@TestContext.CurrentContext.TestDirectory + "/../../../data//F001264.dat"))
      {
        //parse header
        Dictionary<string, string> headers = base.ParseSection(sr, "header");
        Assert.AreEqual(11, headers.Count);
        Assert.AreEqual("136322", headers["sequences"]);
        Assert.AreEqual("136322", headers["sequences_after_tax"]);
        Assert.AreEqual("57508264", headers["residues"]);
        Assert.AreEqual("24", headers["exec_time"]);
        Assert.AreEqual("1180126715", headers["date"]);
        Assert.AreEqual("16:58:35", headers["time"]);
        Assert.AreEqual("4", headers["queries"]);
        Assert.AreEqual("50", headers["max_hits"]);
        Assert.AreEqual("2.1.04", headers["version"]);
        Assert.AreEqual("ipi.HUMAN.v3.29.REVERSED.fasta", headers["release"]);
      }
    }

    [Test]
    public void TestParseSectionWithRegex()
    {
      using (var sr = new StreamReader(@TestContext.CurrentContext.TestDirectory + "/../../../data//F001264.dat"))
      {
        //parse header
        Dictionary<string, string> headers = base.ParseSection(sr, "header", "sequences");
        Assert.AreEqual(2, headers.Count);
        Assert.AreEqual("136322", headers["sequences"]);
        Assert.AreEqual("136322", headers["sequences_after_tax"]);

        //parse summary
        Dictionary<string, string> summary = base.ParseSection(sr, "summary", @"^(?:qmass|qexp|qmatch)");
        Assert.AreEqual(12, summary.Count);
      }
    }

    [Test]
    public void TestParseProtease()
    {
      using (var sr = new StreamReader(@TestContext.CurrentContext.TestDirectory + "/../../../data//F001264.dat"))
      {
        Protease p = ParseEnzyme(sr);
        Assert.AreEqual("semiTrypsin", p.Name);
        Assert.AreEqual("KR", p.CleaveageResidues);
        Assert.AreEqual("P", p.NotCleaveResidues);
        Assert.AreEqual(true, p.IsEndoProtease);
      }
    }
  }
}