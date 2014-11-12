using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;
using System.IO;

namespace RCPA.Proteomics.PFind
{
  [TestFixture]
  public class TestPFindSpectrumParser
  {
    string fileName = @"..\..\data\pFind_qry.proteins.txt";

    [Test]
    public void TestParseModification()
    {
      PFindSpectrumParser parser = new PFindSpectrumParser();

      var sec = new Dictionary<string, string>();
      sec.Add("Fixed_modifications", "1,Carbamidomethyl_C");
      sec.Add("Variable_modifications", "2,Oxidation_M,Phosphylation_STY");

      var mods = parser.ParseModification(sec);
      Assert.AreEqual(1, mods.StaticModification.Count);
      Assert.AreEqual("C", mods.StaticModification[0].Aminoacid);
      Assert.AreEqual("Carbamidomethyl", mods.StaticModification[0].Modification);
      Assert.AreEqual(2, mods.DynamicModification.Count);
      Assert.AreEqual("M", mods.DynamicModification[0].Aminoacid);
      Assert.AreEqual("Oxidation", mods.DynamicModification[0].Modification);
      Assert.AreEqual("STY", mods.DynamicModification[1].Aminoacid);
      Assert.AreEqual("Phosphylation", mods.DynamicModification[1].Modification);
    }

    [Test]
    public void Test()
    {
      PFindSpectrumParser parser = new PFindSpectrumParser();

      var peptides = parser.ReadFromFile(fileName);

      Assert.AreEqual(2970, peptides.Count);

      Assert.AreEqual(1, peptides[0].Query.QueryId);
      Assert.AreEqual("B06-11073.1000.1000.2.dta", peptides[0].Query.FileScan.LongFileName);
      Assert.AreEqual(2, peptides[0].Query.Charge);
      //Assert.AreEqual(1, peptides[0].NumMissedCleavages);
      Assert.AreEqual(839.49962, peptides[0].ExperimentalMH, 0.00001);
      Assert.AreEqual(420.25345, peptides[0].ObservedMz, 0.00001);
      Assert.AreEqual(54.08608, peptides[0].Score, 0.00001);
      Assert.AreEqual(1.537127E-001, peptides[0].ExpectValue, 0.0000001);
      Assert.AreEqual(839.46214, peptides[0].TheoreticalMH, 0.00001);
      Assert.AreEqual(1, peptides[0].Proteins.Count);
      Assert.AreEqual("YFR023W", peptides[0].Proteins[0]);
      Assert.AreEqual("YSISNKK", peptides[0].Sequence);
      Assert.AreEqual("", peptides[0].Modifications);
      Assert.AreEqual(1.0, peptides[0].DeltaScore, 0.01);

      var pep = peptides.Find(m => m.Query.QueryId == 32);
      Assert.AreEqual("B06-11073.1061.1061.2.dta", pep.Query.FileScan.LongFileName);
      Assert.AreEqual("Oxidation_M", pep.Modifications);
      Assert.AreEqual("IM*AGFKDDTK", pep.Sequence);
      Assert.AreEqual(0.044, pep.DeltaScore, 0.001);
    }
  }
}
