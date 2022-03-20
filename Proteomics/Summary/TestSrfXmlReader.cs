using NUnit.Framework;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestSrfXmlReader
  {
    [Test]
    public void TestRead()
    {
      SrfXmlReader reader = new SrfXmlReader();
      List<IIdentifiedProtein> proteins = reader.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/searchresult.xml");
      Assert.AreEqual(2, proteins.Count);
      Assert.AreEqual("rat_mine", proteins[0].Reference);
      Assert.AreEqual(1.00E-30, proteins[0].Score);
      Assert.AreEqual(0.00, proteins[0].Coverage);
      Assert.AreEqual(142901.9, proteins[0].MolecularWeight);
      Assert.AreEqual("rat_mine", proteins[0].Name);

      Assert.AreEqual(160, proteins[0].Peptides.Count);

      IIdentifiedSpectrum peptide = proteins[0].Peptides[0].Spectrum;
      Assert.AreEqual("2006091402,1115 - 1126", peptide.Query.FileScan.ShortFileName);
      Assert.AreEqual("R.THSGTYQVTVR.I", peptide.Peptides[0].Sequence);
      Assert.AreEqual("rat_mine", peptide.Peptides[0].Proteins[0]);
      Assert.AreEqual(1248.63, peptide.TheoreticalMH, 0.01);
      Assert.AreEqual(0.01, peptide.TheoreticalMinusExperimentalMass, 0.01);
      Assert.AreEqual(3, peptide.Query.Charge);
      Assert.AreEqual(2.634, peptide.Score, 0.001);
      Assert.AreEqual(1.0, peptide.DeltaScore);
      Assert.AreEqual(1694.8, peptide.SpScore, 0.1);
      Assert.AreEqual(1, peptide.SpRank);
      Assert.AreEqual("37/80", peptide.Ions);
    }
  }
}
