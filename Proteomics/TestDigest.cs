using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Seq;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestDigest
  {
    [Test]
    public void TestAddDigestFeatures()
    {
      Sequence seq = new Sequence("Test", "AAANESAARBBBNPSBBKFFFNITFFRGGGNDTGGR");

      Digest digest = new Digest();
      digest.DigestProtease = ProteaseManager.CreateProtease("Trypsin_TestAddDigestFeatures", true, "RK", "P");
      digest.ProteinSequence = seq;

      digest.MaxMissedCleavages = 0;
      digest.AddDigestFeatures();
      List<DigestPeptideInfo> peptides = (List<DigestPeptideInfo>)seq.Annotation[Digest.PEPTIDE_FEATURE_TYPE];
      Assert.AreEqual(4, peptides.Count);

      digest.MaxMissedCleavages = 1;
      digest.AddDigestFeatures();
      List<DigestPeptideInfo> missedPeptides = (List<DigestPeptideInfo>)seq.Annotation[Digest.PEPTIDE_FEATURE_TYPE];
      Assert.AreEqual(7, missedPeptides.Count);

      IRangeLocationFilter nglycanFilter = new NGlycanFilter();
      digest.MaxMissedCleavages = 0;
      digest.Filter = nglycanFilter;
      digest.AddDigestFeatures();
      List<DigestPeptideInfo> nglycanPeptides = (List<DigestPeptideInfo>)seq.Annotation[Digest.PEPTIDE_FEATURE_TYPE];
      Assert.AreEqual(2, nglycanPeptides.Count);
      Assert.AreEqual("AAANESAAR", nglycanPeptides[0].PeptideSeq);
      Assert.AreEqual("FFFNITFFR", nglycanPeptides[1].PeptideSeq);
    }
  }
}
