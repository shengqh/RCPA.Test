using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestOutParser : OutParser
  {
    [Test]
    public void TestParseFrom_V27_Rev11()
    {
      IIdentifiedSpectrum spectrum = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//sequest_v27_rev11.out");
      Assert.IsNotNull(spectrum);
      Assert.AreEqual("HLPP2004112411CLC-412414.743.745.2.out", spectrum.Query.FileScan.LongFileName);
      Assert.AreEqual(1701.0000, spectrum.ExperimentalMH, 0.0001);

      Assert.AreEqual(1702.0120, spectrum.TheoreticalMH, 0.0001);
      Assert.AreEqual(0.0609, spectrum.DeltaScore, 0.0001);
      Assert.AreEqual(1.4213, spectrum.Score, 0.0001);
      Assert.AreEqual(152.7, spectrum.SpScore, 0.0001);
      Assert.AreEqual(10, spectrum.MatchedIonCount);
      Assert.AreEqual(26, spectrum.TheoreticalIonCount);

      Assert.AreEqual("-.IVLNPNKPVVEWHR.-", spectrum.Peptide.Sequence);
      Assert.AreEqual("IPI:IPI00329593.1|RE", spectrum.Peptide.Proteins[0]);
      Assert.AreEqual("IPI:IPI00396457.1|TR", spectrum.Peptide.Proteins[1]);
      Assert.AreEqual(2, spectrum.Peptide.Proteins.Count);
    }

    [Test]
    public void TestParseFrom_V27_Rev12()
    {
      IIdentifiedSpectrum spectrum = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//sequest_v27_rev12.out");
      Assert.IsNotNull(spectrum);
      Assert.AreEqual("Standard_Protein_FIT_060222.5079.5079.1.out", spectrum.Query.FileScan.LongFileName);
      Assert.AreEqual(627.38200, spectrum.ExperimentalMH, 0.0001);

      Assert.AreEqual(627.45521, spectrum.TheoreticalMH, 0.00001);
      Assert.AreEqual(1.0, spectrum.DeltaScore, 0.0001);
      Assert.AreEqual(1.4857, spectrum.Score, 0.0001);
      Assert.AreEqual(210.0, spectrum.SpScore, 0.0001);
      Assert.AreEqual(6, spectrum.MatchedIonCount);
      Assert.AreEqual(8, spectrum.TheoreticalIonCount);

      Assert.AreEqual(10, spectrum.Peptides.Count);

      Assert.AreEqual("K.LLLIR.S", spectrum.Peptide.Sequence);
      Assert.AreEqual("IPI00529841", spectrum.Peptide.Proteins[0]);
      Assert.AreEqual("REVERSED_00005138", spectrum.Peptide.Proteins[1]);
      Assert.AreEqual(10, spectrum.Peptide.Proteins.Count);
    }

    [Test]
    public void TestParseFrom_V28_Rev12()
    {
      IIdentifiedSpectrum spectrum = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//sequest_v28_rev12.out");
      Assert.IsNotNull(spectrum);
      Assert.AreEqual("20110509z.3768.3768.1.out", spectrum.Query.FileScan.LongFileName);
      Assert.AreEqual(905.86700, spectrum.ExperimentalMH, 0.00001);

      Assert.AreEqual(904.07163, spectrum.TheoreticalMH, 0.00001);
      Assert.AreEqual(0.2716, spectrum.DeltaScore, 0.0001);
      Assert.AreEqual(0.4313, spectrum.Score, 0.0001);
      Assert.AreEqual(11.4, spectrum.SpScore, 0.0001);
      Assert.AreEqual(2, spectrum.MatchedIonCount);
      Assert.AreEqual(14, spectrum.TheoreticalIonCount);


      Assert.AreEqual("R.SIPMGWGR.T", spectrum.Peptide.Sequence);
      Assert.AreEqual("gi|4204443|gb|AAD10723.1|", spectrum.Peptide.Proteins[0]);
      Assert.AreEqual("gi|111219186|gb|ABH08921.1|", spectrum.Peptide.Proteins[1]);
      Assert.AreEqual(2, spectrum.Peptide.Proteins.Count);
    }

    [Test]
    public void TestParseFrom_V28_rev13()
    {
      IIdentifiedSpectrum spectrum = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//sequest_v28_rev13.out");
      Assert.IsNotNull(spectrum);
      Assert.AreEqual("TUCJ_022009_1D_RKO_1.3052.3052.2.out", spectrum.Query.FileScan.LongFileName);
      Assert.AreEqual(1345.07800, spectrum.ExperimentalMH, 0.00001);

      Assert.AreEqual(1344.73694, spectrum.TheoreticalMH, 0.00001);
      Assert.AreEqual(0.1193, spectrum.DeltaScore, 0.0001);
      Assert.AreEqual(2.6095, spectrum.Score, 0.0001);
      Assert.AreEqual(215.7, spectrum.SpScore, 0.0001);
      Assert.AreEqual(11, spectrum.MatchedIonCount);
      Assert.AreEqual(24, spectrum.TheoreticalIonCount);


      Assert.AreEqual("R.VPETVSAATQTIK.N", spectrum.Peptide.Sequence);
      Assert.AreEqual("IPI:IPI00029778.3|SWISS-PROT:Q12888-1|ENSEMBL:ENSP00000263801;ENSP00000384078|REFSEQ:NP_005648|H-INV:HIT000066277|VEGA:OTTHUMP00000066116;OTTHUMP00000176233", spectrum.Peptide.Proteins[0]);
      Assert.AreEqual("IPI:IPI00657704.3|TREMBL:C9JXV0|EN", spectrum.Peptide.Proteins[1]);
      Assert.AreEqual(4, spectrum.Peptide.Proteins.Count);
    }

    /**
     * Some time the first 10 candidates are all rank 1. So parser will try to parse
     * the next line which starts with "1." as next candidate. It may throw exception 
     * of format conversion. 
     *  10.   1 /  1          0  627.45521  0.0000  1.4857   210.0   6/ 8  REVERSED_00018687 +2  R.IILIR.R
     *                        0  SHUFFLET_00000523
     *                        0  SHUFFLET_00003343
     *   1.          0  IPI00529841  Tax_Id=3702 Arabidopsis thaliana genomic DNA, chromosome 3, P1 cl
     **/

    [Test]
    public void TestModifiedParseFromFile()
    {
      var parser = new ModificationOutParser(0.1);

      IIdentifiedSpectrum peptide = parser.ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//JWH_SAX_35_050906.19303.19303.2.out");
      Assert.IsNotNull(peptide);
      Assert.AreEqual(0, peptide.DiffModificationSiteCandidates.Count);

      peptide = parser.ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//P_kit_elution.5407.5407.3.out");
      Assert.IsNotNull(peptide);
      Assert.AreEqual(2, peptide.DiffModificationSiteCandidates.Count);
    }

    [Test]
    public void TestParse2()
    {
      Assert.IsNotNull(ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//Standard_Protein_FIT_060222.5079.5079.1.out"));
    }

    [Test]
    public void TestParseEmpty()
    {
      Assert.AreEqual(null, ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//RP62A_15N_1_03.10801.10801.3.out"));
      Assert.AreEqual(null, ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//Standard_Protein_FIT_060222.8034.8034.1.out"));
    }

    [Test]
    [ExpectedException(typeof(System.IO.FileNotFoundException))]
    public void TestParseFromFileException()
    {
      ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//unexistsfile.1.1.2.out");
    }

    [Test]
    public void TestParseFromMultipleCandidate()
    {
      IIdentifiedSpectrum peptide = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//ZhouH_ML_050720_Nano_pH_02.29478.29478.2.out");
      Assert.IsNotNull(peptide);
      Assert.AreEqual("ZhouH_ML_050720_Nano_pH_02,29478", peptide.Query.FileScan.ShortFileName);
      Assert.AreEqual(2, peptide.Peptides.Count);
    }

    [Test]
    [ExpectedException(typeof(RCPA.Proteomics.Sequest.DuplicatedReferenceAbsentException))]
    public void TestParseFromVersion_27_12_WithDuplicatedReferencesCheck()
    {
      var parser = new OutParser(true);
      parser.ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//mouseliver.4587.4587.2.out");
    }

    [Test]
    public void TestParseFromVersion_27_12_WithoutDuplicatedReferencesCheck()
    {
      var parser = new OutParser(false);

      IIdentifiedSpectrum peptide = parser.ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//mouseliver.4587.4587.2.out");
      Assert.IsNotNull(peptide);
      Assert.AreEqual("20030714_MouseLiver_2,4587", peptide.Query.FileScan.ShortFileName);
      Assert.AreEqual("-.S*AILS*NTPS*LLALR.-", peptide.Peptides[0].Sequence);
      Assert.AreEqual("Q8VI59", peptide.Peptides[0].Proteins[0]);
      Assert.AreEqual(1696.6531, peptide.TheoreticalMH, 0.0001);
      Assert.AreEqual(1695.9512, peptide.ExperimentalMH, 0.0001);
    }

    /**
      Sometime, SEQUEST will ingore something like follow (in HLPP20050041811CLC-17.2074.2074.2.out):
      +28 means there are 28 other protein contains current peptide sequence, but
      actually SEQUEST only record 20 protein names, so I have to ignore such mistake
      to ensure that summary-building process can be continued.
      1.   1 / 65  1963.2010  0.0000  1.1017   101.4   7/30  IPI:IPI00029168.1|SW +28  -.TPEYYPNAGLIMNYCR.-
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
          IPI:IPI00029168.1|SW
      2.   2 / 69  1962.3430  0.1021  0.9893    99.8   8/34  IPI:IPI00016282.1|SW      -.LVLGLHTLDSPGLTFHIK.-
    **/

    [Test]
    public void TestParseWithDuplicatedReferencesCheckIgnoreSEQUESTMistake()
    {
      IIdentifiedSpectrum peptide = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//HLPP20050041811CLC-17.2074.2074.2.out");
      Assert.IsNotNull(peptide);
    }

    [Test]
    public void TestReadFromFileMissingTerminalAminoacid()
    {
      IIdentifiedSpectrum peptide = ParseFromFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//12228_15N_1_01.6027.6027.2.out");
      Assert.IsNotNull(peptide);
      Assert.AreEqual("12228_15N_1_01,6027", peptide.Query.FileScan.ShortFileName);
      Assert.AreEqual("K.EINEVDLQLK.X", peptide.Peptides[0].Sequence);
      Assert.AreEqual("SER0844", peptide.Peptides[0].Proteins[0]);
    }
  }
}