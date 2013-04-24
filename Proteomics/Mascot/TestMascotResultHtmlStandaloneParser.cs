using System.IO;
using NUnit.Framework;
using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotResultHtmlStandaloneParser
  {
    [Test]
    public void testConstruction()
    {
      var testFile = new FileInfo(@"..\..\data\mascot_result_protein_newversion.html");
      // filter by rank 1 only
      MascotResult mr = new MascotResultHtmlStandaloneParser(false)
        .ParseFile(testFile);
      Assert.AreEqual(2, mr.Count);
      Assert.AreEqual(3, mr[0][0].Peptides.Count);
      Assert.AreEqual(3, mr[1][0].Peptides.Count);

      // filter by rank 1 and score
      mr = new MascotResultHtmlStandaloneParser(new IdentifiedSpectrumScoreFilter(40),
                                                false).ParseFile(testFile);
      Assert.AreEqual(2, mr.Count);
      Assert.AreEqual(2, mr[0][0].Peptides.Count);
      Assert.AreEqual(1, mr[1][0].Peptides.Count);

      mr = new MascotResultHtmlStandaloneParser(new IdentifiedSpectrumExpectValueFilter(
                                                  0.01), false).ParseFile(testFile);
      Assert.AreEqual(2, mr.Count);
      Assert.AreEqual(2, mr[0][0].Peptides.Count);
      Assert.AreEqual(1, mr[1][0].Peptides.Count);

      mr = new MascotResultHtmlStandaloneParser(new IdentifiedSpectrumExpectValueFilter(
                                                  0.0016), false).ParseFile(testFile);
      Assert.AreEqual(1, mr.Count);
      Assert.AreEqual(1, mr[0][0].Peptides.Count);

      mr = new MascotResultHtmlStandaloneParser(new IdentifiedSpectrumExpectValueFilter(
                                                  1.0E-5), false).ParseFile(testFile);
      Assert.AreEqual(0, mr.Count);
    }

    [Test]
    public void testO18Result()
    {
      MascotResult mr = new MascotResultHtmlStandaloneParser(true)
        .ParseFile(new FileInfo(@"..\..\data\mascot_result_protein_o18.html"));
      Assert.AreEqual(1, mr.Count);
      Assert.AreEqual(6, mr[0].Count);
      Assert.AreEqual(10, mr[0][0].Peptides.Count);

      IIdentifiedSpectrum mp = mr[0][0].FindPeptideByQuery(5492).Spectrum;
      Assert.IsNotNull(mp);
      Assert.AreEqual("R.ISLPLPNFSSLNLR.E", mp.Sequence);
      Assert.AreEqual("Label:18O(2) (C-term)", mp.Modifications);
    }

    [Test]
    public void testParseFile1()
    {
      MascotResult mr = new MascotResultHtmlStandaloneParser(true)
        .ParseFile(new FileInfo(@"..\..\data\mascot_result_protein_newversion.html"));

      Assert.AreEqual(33, mr.PValueScore);
      Assert.AreEqual(0.05, mr.PValue);

      Assert.AreEqual(IsotopicType.Monoisotopic, mr.PeakIsotopicType);
      Assert.AreEqual(0.8, mr.PeakTolerance, 0.01);

      Assert.AreEqual(2, mr.Count);

      IIdentifiedProtein protein1 = mr[0][0];
      Assert.AreEqual("sp|P00489|PHS2_RABIT", protein1.Name);
      Assert.AreEqual(97610, protein1.MolecularWeight, 1.0);
      Assert.AreEqual(
        "Glycogen phosphorylase, muscle form (EC 2.4.1.1) (Myophosphorylase) - Oryctolagus cuniculus (Rabbit",
        protein1.Description);

      Assert.AreEqual(3, protein1.Peptides.Count);

      IIdentifiedSpectrum pep11 = protein1.Peptides[0].Spectrum;
      Assert.AreEqual(1053.5720, pep11.Query.ObservedMz, 0.0001);
      Assert.AreEqual("Cmpd.6612.6612.1.dta", pep11.Query.FileScan.LongFileName);
      Assert.AreEqual(2232, pep11.Query.QueryId);
      Assert.AreEqual(1052.5647, pep11.ExperimentalMass, 0.0001);
      Assert.AreEqual(1052.5654, pep11.TheoreticalMass, 0.0001);
      Assert.AreEqual(0.0007, pep11.TheoreticalMinusExperimentalMass, 0.0001);
      Assert.AreEqual(0, pep11.NumMissedCleavages);
      Assert.AreEqual(34, pep11.Score);
      Assert.AreEqual(0.039, pep11.ExpectValue);
      Assert.AreEqual(1, pep11.Rank);
      Assert.AreEqual("R.VIFLENYR.V", pep11.Sequence);

      IIdentifiedSpectrum pep12 = protein1.Peptides[1].Spectrum;
      Assert.AreEqual(527.7459, pep12.Query.ObservedMz, 0.0001);
      Assert.AreEqual("Cmpd.5747.5747.2.dta", pep12.Query.FileScan.LongFileName);
      Assert.AreEqual(2239, pep12.Query.QueryId);
      Assert.AreEqual(1053.4772, pep12.ExperimentalMass, 0.0001);
      Assert.AreEqual(1053.4767, pep12.TheoreticalMass, 0.0001);
      Assert.AreEqual(-0.0005, pep12.TheoreticalMinusExperimentalMass, 0.0001);
      Assert.AreEqual(0, pep12.NumMissedCleavages);
      Assert.AreEqual(48, pep12.Score);
      Assert.AreEqual(5.4E-5, pep12.ExpectValue, 0.001);
      Assert.AreEqual(1, pep12.Rank);
      Assert.AreEqual("R.TNFDAFPDK.V", pep12.Sequence);

      IIdentifiedSpectrum pep13 = protein1.Peptides[2].Spectrum;
      Assert.AreEqual(527.7464, pep13.Query.ObservedMz, 0.0001);
      Assert.AreEqual("Cmpd.5743.5743.2.dta", pep13.Query.FileScan.LongFileName);
      Assert.AreEqual(2240, pep13.Query.QueryId);
      Assert.AreEqual(1053.4782, pep13.ExperimentalMass, 0.0001);
      Assert.AreEqual(1053.4767, pep13.TheoreticalMass, 0.0001);
      Assert.AreEqual(-0.0015, pep13.TheoreticalMinusExperimentalMass, 0.0001);
      Assert.AreEqual(0, pep13.NumMissedCleavages);
      Assert.AreEqual(47, pep13.Score);
      Assert.AreEqual(0.0018, pep13.ExpectValue, 0.001);
      Assert.AreEqual(1, pep13.Rank);
      Assert.AreEqual("R.TNFDAFPDK.V", pep13.Sequence);

      IIdentifiedProtein protein2 = mr[1][0];
      Assert.AreEqual("REVERSED_0008631", protein2.Name);
      Assert.AreEqual("", protein2.Description);

      Assert.AreEqual(2, protein2.Peptides.Count);

      IIdentifiedSpectrum pep21 = protein2.Peptides[0].Spectrum;
      Assert.AreEqual(699.4770, pep21.Query.ObservedMz, 0.0001);
      Assert.AreEqual("Standard_Protein_FIT_060222.6065.6065.1.dta", pep21.
                                                                       Query.FileScan.LongFileName);
      Assert.AreEqual(932, pep21.Query.QueryId);
      Assert.AreEqual(698.4697, pep21.ExperimentalMass, 0.0001);
      Assert.AreEqual(698.4115, pep21.TheoreticalMass, 0.0001);
      Assert.AreEqual(-0.0582, pep21.TheoreticalMinusExperimentalMass, 0.0001);
      Assert.AreEqual(0, pep21.NumMissedCleavages);
      Assert.AreEqual(33, pep21.Score);
      Assert.AreEqual(0.049, pep21.ExpectValue);
      Assert.AreEqual(1, pep21.Rank);
      Assert.AreEqual("R.WPAVVK.L", pep21.Sequence);

      Assert.AreSame(protein1.Peptides[2], protein2.Peptides[1]);
    }

    [Test]
    public void testParseFile2()
    {
      MascotResult mr = new MascotResultHtmlStandaloneParser(true)
        .ParseFile(new FileInfo(@"..\..\data\mascot_result_Caseins00001.htm"));

      Assert.AreEqual(44, mr.PValueScore);
      Assert.AreEqual(0.05, mr.PValue);

      Assert.AreEqual(IsotopicType.Monoisotopic, mr.PeakIsotopicType);
      Assert.AreEqual(0.8, mr.PeakTolerance, 0.01);

      Assert.AreEqual(1, mr.Count);

      IIdentifiedProtein protein1 = mr[0][0];
      Assert.AreEqual("ALBU_HUMAN", protein1.Name);
      Assert.AreEqual("Serum albumin precursor - Homo sapiens (Human)", protein1
                                                                          .Description);

      Assert.AreEqual(2, protein1.Peptides.Count);

      IIdentifiedSpectrum pep11 = protein1.Peptides[0].Spectrum;
      Assert.AreEqual(547.22, pep11.Query.ObservedMz, 0.01);
      Assert.AreEqual("Cmpd.51.51.3.dta", pep11.Query.FileScan.LongFileName);
      Assert.AreEqual(118, pep11.Query.QueryId);
      Assert.AreEqual(1638.6382, pep11.ExperimentalMass, 0.0001);
      Assert.AreEqual(1638.9304, pep11.TheoreticalMass, 0.0001);
      Assert.AreEqual(0.2922, pep11.TheoreticalMinusExperimentalMass, 0.0001);
      Assert.AreEqual(1, pep11.NumMissedCleavages);
      Assert.AreEqual(50, pep11.Score);
      Assert.AreEqual(0.0091, pep11.ExpectValue, 0.0001);
      Assert.AreEqual(1, pep11.Rank);
      Assert.AreEqual("K.KVPQVSTPTLVEVSR.N", pep11.Sequence);

      IIdentifiedSpectrum pep12 = protein1.Peptides[1].Spectrum;
      Assert.AreEqual(547.23, pep12.Query.ObservedMz, 0.01);
      Assert.AreEqual("Cmpd.47.47.3.dta", pep12.Query.FileScan.LongFileName);
      Assert.AreEqual(119, pep12.Query.QueryId);
      Assert.AreEqual(1638.6682, pep12.ExperimentalMass, 0.0001);
      Assert.AreEqual(1638.9304, pep12.TheoreticalMass, 0.0001);
      Assert.AreEqual(0.2622, pep12.TheoreticalMinusExperimentalMass, 0.0001);
      Assert.AreEqual(1, pep12.NumMissedCleavages);
      Assert.AreEqual(52, pep12.Score);
      Assert.AreEqual(0.0062, pep12.ExpectValue, 0.0001);
      Assert.AreEqual(1, pep12.Rank);
      Assert.AreEqual("K.KVPQVSTPTLVEVSR.N", pep12.Sequence);
    }

    [Test]
    public void testParseFileWithSameMatchProteins()
    {
      MascotResult mr = new MascotResultHtmlStandaloneParser(false)
        .ParseFile(new FileInfo(@"..\..\data\mascot_result_Caseins00001.htm"));

      Assert.AreEqual(44, mr.PValueScore);
      Assert.AreEqual(0.05, mr.PValue);

      Assert.AreEqual(IsotopicType.Monoisotopic, mr.PeakIsotopicType);
      Assert.AreEqual(0.8, mr.PeakTolerance, 0.01);

      Assert.AreEqual(8, mr.Count);

      Assert.AreEqual("TAU_HUMAN", mr[6][0].Name);
      Assert.AreEqual(2, mr[6].Count);
      Assert.AreEqual("TAU_PANTR", mr[6][1].Name);
    }
  }
}