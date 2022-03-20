using NUnit.Framework;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotUtils
  {
    private static List<IIdentifiedProtein> InitProteins()
    {
      var mph1 = new IdentifiedSpectrum();
      mph1.Query.FileScan.Experimental = "EXP1";
      var mp1 = new IdentifiedPeptide(mph1);
      mp1.AddProtein("Protein1");
      mp1.AddProtein("Protein2");
      mp1.Sequence = "SEQ1";

      var mph2 = new IdentifiedSpectrum();
      mph2.Query.FileScan.Experimental = "EXP2";
      var mp2 = new IdentifiedPeptide(mph2);
      mp2.AddProtein("Protein1");
      mp2.AddProtein("Protein3");
      mp2.Sequence = "SEQ2";

      var mpro1 = new IdentifiedProtein("Protein1");
      mpro1.Peptides.Add(mp1);
      mpro1.Peptides.Add(mp2);

      var mpro2 = new IdentifiedProtein("Protein2");
      mpro2.Peptides.Add(mp1);

      var mpro3 = new IdentifiedProtein("Protein3");
      mpro3.Peptides.Add(mp2);

      var result = new List<IIdentifiedProtein>();

      result.Add(mpro3);
      result.Add(mpro2);
      result.Add(mpro1);

      return result;
    }

    [Test]
    public void TestBuildNonredundantProteinGroup()
    {
      List<IIdentifiedProtein> proteins = InitProteins();

      List<IIdentifiedProteinGroup> mpgs = MascotUtils.BuildNonredundantProteinGroups(proteins);

      Assert.AreEqual(1, mpgs.Count);
      Assert.AreEqual("Protein1", mpgs[0][0].Name);
    }

    [Test]
    public void TestBuildProteins()
    {
      var mph1 = new IdentifiedSpectrum();
      mph1.Query.FileScan.Experimental = "EXP1";
      var mp1 = new IdentifiedPeptide(mph1);
      mp1.AddProtein("Protein1");
      mp1.AddProtein("Protein2");

      var mph2 = new IdentifiedSpectrum();
      mph2.Query.FileScan.Experimental = "EXP2";
      var mp2 = new IdentifiedPeptide(mph2);
      mp2.AddProtein("Protein1");
      mp2.AddProtein("Protein3");

      var mphs = new List<IIdentifiedSpectrum>();
      mphs.Add(mph1);
      mphs.Add(mph2);

      List<IIdentifiedProtein> proteins = MascotUtils.BuildProteins(mphs);

      Assert.AreEqual(3, proteins.Count);

      foreach (IdentifiedProtein mp in proteins)
      {
        if (mp.Name.Equals("Protein1"))
        {
          Assert.AreEqual(2, mp.Peptides.Count);
          continue;
        }

        if (mp.Name.Equals("Protein2"))
        {
          Assert.AreEqual(1, mp.Peptides.Count);
          Assert.AreEqual(mp1, mp.Peptides[0]);
          continue;
        }

        if (mp.Name.Equals("Protein3"))
        {
          Assert.AreEqual(1, mp.Peptides.Count);
          Assert.AreEqual(mp2, mp.Peptides[0]);
          continue;
        }
      }
    }

    [Test]
    public void TestBuildRedundantProteinGroup()
    {
      List<IIdentifiedProtein> proteins = InitProteins();

      List<IIdentifiedProteinGroup> mpgs = MascotUtils.BuildRedundantProteinGroups(proteins);

      Assert.AreEqual(3, mpgs.Count);
      Assert.AreEqual("Protein1", mpgs[0][0].Name);
      Assert.AreEqual("Protein2", mpgs[1][0].Name);
      Assert.AreEqual("Protein3", mpgs[2][0].Name);
    }

    [Test]
    public void TestGetAnnotationKeys()
    {
      string key1 = "TEST1";
      string key2 = "TEST2";

      var mph1 = new IdentifiedSpectrum();
      mph1.Annotations.Add(key1, null);
      mph1.Query.FileScan.Experimental = "EXP1";
      new IdentifiedPeptide(mph1);

      var mph2 = new IdentifiedSpectrum();
      mph2.Annotations.Add(key2, null);
      mph2.Query.FileScan.Experimental = "EXP2";
      new IdentifiedPeptide(mph2);

      mph1.Peptide.Sequence = "SEQ1";
      mph2.Peptide.Sequence = "SEQ2";

      var protein = new IdentifiedProtein();
      protein.Peptides.Add(mph1.Peptide);
      protein.Peptides.Add(mph2.Peptide);

      var mpg = new IdentifiedProteinGroup();
      mpg.Add(protein);

      var mr = new MascotResult();
      mr.Add(mpg);

      List<string> annotationKeys = AnnotationUtils.GetAnnotationKeys(mr.GetSpectra());
      Assert.AreEqual(2, annotationKeys.Count);
      Assert.IsTrue(annotationKeys.Contains(key1));
      Assert.IsTrue(annotationKeys.Contains(key2));
    }

    [Test]
    public void TestParseTitle()
    {
      SequestFilename sf = MascotUtils.ParseTitle(" Cmpd. 6612. 6612. 1. dta", 1);
      Assert.AreEqual("Cmpd.6612.6612.1.dta", sf.LongFileName);

      sf = MascotUtils.ParseTitle(" FIT_PPN_SAX_Online_1mg_060909_01, Cmpd 7736, +MSn(600.4082), 51.88 min", 1);
      Assert.AreEqual("FIT_PPN_SAX_Online_1mg_060909_01.7736.7736.1.dta", sf.LongFileName);

      sf =
        MascotUtils.ParseTitle(
          "spectrumId=6085 Polarity=Positive ScanMode=ProductIon TimeInMinutes=61.875 acqNumber=3712494", 1);
      Assert.AreEqual(".6085.6085.1.dta", sf.LongFileName);

      sf =
        MascotUtils.ParseTitle(
          "Elution from: 40.798 to 40.798 period: 0 experiment: 1 cycles: 1 precIntensity: 355220.0 FinneganScanNumber: 6070 MStype: enumIsNormalMS rawFile: KR_mix_lc_090115.raw",
          1);
      Assert.AreEqual("KR_mix_lc_090115.6070.6070.1.dta", sf.LongFileName);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestParseTitleException()
    {
      MascotUtils.ParseTitle("dfajdskfjalsdjfa", 1);
    }
  }
}