using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestMsfDatabaseToNoredundantProcessor
  {
    private string msfFile = "../../data/SK158d.msf";
    private MsfDatabaseToNoredundantProcessor processor = new MsfDatabaseToNoredundantProcessor();

    [Test]
    public void TestParseProteins()
    {
      var proteins = processor.ParseProteins(msfFile);
      Assert.AreEqual(1, proteins.Count);
      Assert.AreEqual(82, proteins[0].PeptideCount);
    }

    [Test]
    public void TestParseProteinMap()
    {
      var proteins = processor.ParseProteinMap(msfFile);
      Assert.AreEqual(1, proteins.Count);
      Assert.IsTrue(proteins.ContainsKey(550974));
      Assert.AreEqual(165.1025, proteins[550974].Score, 0.0001);
      Assert.AreEqual(55, proteins[550974].Coverage, 0.1);
      Assert.AreEqual(607, proteins[550974].Sequence.Length);
      Assert.AreEqual("sp|P02769|ALBU_BOVIN Serum albumin OS=Bos taurus GN=ALB PE=1 SV=4", proteins[550974].Reference);
    }

    [Test]
    public void TestParseModifications()
    {
      var mods = processor.ParseModifications(msfFile);
      Assert.AreEqual(2, mods.Count);
      Assert.IsTrue(mods.ContainsKey(43));
      Assert.IsTrue(mods.ContainsKey(194));

      Assert.AreEqual("15.994915 Oxidation", mods[43].SignStr);
      Assert.AreEqual('*', mods[43].SignChar);
      Assert.AreEqual("4.008491 Label:18O(2) (C-term)", mods[194].SignStr);
      Assert.AreEqual('#', mods[194].SignChar);
    }

    [Test]
    public void TestParseFileMap()
    {
      var fileMap = processor.ParseFileMap(msfFile);
      Assert.AreEqual(1, fileMap.Count);
      Assert.IsTrue(fileMap.ContainsKey(753));
      Assert.AreEqual("SK158d", fileMap[753]);
    }

    [Test]
    public void TestParsePeptideMap()
    {
      var spectra = processor.ParsePeptideMap(msfFile);
      Assert.AreEqual(82, spectra.Count);
      var pep = spectra[6];
      Assert.AreEqual(2128, pep.Spectrum.Query.FileScan.FirstScan);
      Assert.AreEqual(2186, pep.Spectrum.Query.FileScan.LastScan);
      Assert.AreEqual(2, pep.Spectrum.Query.FileScan.Charge);
      Assert.AreEqual(1494.5183, pep.Spectrum.ExperimentalMH, 0.0001);
      Assert.AreEqual(22, pep.Spectrum.TheoreticalIonCount);
      Assert.AreEqual(12, pep.Spectrum.MatchedIonCount);
      Assert.AreEqual("ETYGDM*ADCCEK", pep.Sequence);
      Assert.AreEqual("15.994915 Oxidation (M)", pep.Spectrum.Modifications);
      Assert.AreEqual(3, pep.ConfidenceLevel);
      Assert.AreEqual("SK158d", pep.Spectrum.Query.FileScan.Experimental);

      Assert.AreEqual(1, spectra[12].ConfidenceLevel);

      Assert.AreEqual("ETYGDM*ADCCEK", spectra[9].Sequence);
      Assert.AreEqual("ETYGDM*ADCCEKQEPER", spectra[10].Sequence);
      Assert.AreEqual("ETYGDM*ADCCEK", spectra[11].Sequence);
      Assert.AreEqual("ETYGDM*ADCCEK", spectra[18].Sequence);
      Assert.AreEqual("TVM*ENFVAFVDK#", spectra[82].Sequence);
      Assert.AreEqual("15.994915 Oxidation (M); 4.008491 Label:18O(2) (C-term)", spectra[82].Spectrum.Modifications);
    }

    [Test]
    public void TestParseAminoacids()
    {
      var aas = processor.ParseAminoacids(msfFile);
      Assert.AreEqual(71.03712, aas['A'].MonoMass, 0.00001);
      Assert.AreEqual(71.0787, aas['A'].AverageMass, 0.0001);
      Assert.AreEqual(160.030654, aas['C'].MonoMass, 0.000001);
    }
  }
}
