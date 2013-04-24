using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Seq;
using RCPA.Proteomics.Snp;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PFind
{
  [TestFixture]
  public class TestSnpDataValidator
  {
    private List<Sequence> seqs;
    private IdentifiedPeptide identical, singlemutation, misscleavageIdentical, misscleavageSingleMutation, nptIdentical, nptSingleMutation;

    [TestFixtureSetUp]
    public void SetUp()
    {

      seqs = new Sequence[] { new Sequence("Test1", "ADFADJLFASRDLFKJWONNCKAOIWJEFLNC"), new Sequence("Test2", "WEUOIRJKJNCJKLSDTORWELSDJF") }.ToList();
      identical = new IdentifiedPeptide(new IdentifiedSpectrum()) { Sequence = "JWONNCK" };
      singlemutation = new IdentifiedPeptide(new IdentifiedSpectrum()) { Sequence = "LSDAOR" };

      misscleavageIdentical = new IdentifiedPeptide(new IdentifiedSpectrum()) { Sequence = "LFKJWONNCK" };
      misscleavageSingleMutation = new IdentifiedPeptide(new IdentifiedSpectrum()) { Sequence = "JKLSDAOR" };

      nptIdentical = new IdentifiedPeptide(new IdentifiedSpectrum()) { Sequence = "JWONNC" };
      nptSingleMutation = new IdentifiedPeptide(new IdentifiedSpectrum()) { Sequence = "LSDAO" };

      //validator = new SnpDataValidator(null, null, null, new Protease("Trypsin", true, "KR", "P"));
    }

    //[Test]
    //public void TestFindIdentical()
    //{
    //  Assert.AreEqual(true, validator.FindIdentical(seqs, identical));
    //  Assert.AreEqual(false, validator.FindIdentical(seqs, singlemutation));
    //  Assert.AreEqual(false, validator.FindIdentical(seqs, misscleavageIdentical));
    //  Assert.AreEqual(false, validator.FindIdentical(seqs, nptIdentical));
    //}

    //[Test]
    //public void TestFindMutationOne()
    //{
    //  Assert.AreEqual(false, validator.FindMutationOne(seqs, identical));
    //  Assert.AreEqual(true, validator.FindMutationOne(seqs, singlemutation));
    //  Assert.AreEqual(false, validator.FindMutationOne(seqs, misscleavageSingleMutation));
    //  Assert.AreEqual(false, validator.FindMutationOne(seqs, nptSingleMutation));
    //}
  }
}
