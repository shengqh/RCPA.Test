using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestSequenceUtils
  {
    [Test]
    public void TestRead()
    {
      List<Sequence> seqs = SequenceUtils.Read(new FastaFormat(), "../../../data/test.fasta");
      Assert.AreEqual(2, seqs.Count);
      Assert.AreEqual("test1 description of test1", seqs[0].Reference);
      Assert.AreEqual("AAAAA", seqs[0].SeqString);
      Assert.AreEqual("test2 description of test2", seqs[1].Reference);
      Assert.AreEqual("BBBBB", seqs[1].SeqString);
    }

    [Test]
    public void TestGetReversedSequence1()
    {
      Assert.AreEqual("EDCBA", SequenceUtils.GetReversedSequence("ABCDE"));
    }

    [Test]
    public void TestGetReversedSequence2()
    {
      Sequence seq = SequenceUtils.GetReversedSequence("ABCDE", 5);
      Assert.AreEqual("REVERSED_00000005", seq.Name);
      Assert.AreEqual("EDCBA", seq.SeqString);
    }

    [Test]
    public void TestGetReversedSequence3()
    {
      Sequence seq = SequenceUtils.GetReversedSequence("ABCDE", 2, 5);
      Assert.AreEqual("REVERSED_05", seq.Name);
      Assert.AreEqual("EDCBA", seq.SeqString);
    }

    [Test]
    public void TestGetReversedSequence4()
    {
      Sequence seq = SequenceUtils.GetReversedSequence("ABCDE", "REV_", 2, 5);
      Assert.AreEqual("REV_05", seq.Name);
      Assert.AreEqual("EDCBA", seq.SeqString);
    }

    [Test]
    public void TestGetDatabaseComposition()
    {
      Dictionary<char, double> actual = SequenceUtils.GetDatabaseComposition("../../../data/test.fasta");
      Assert.AreEqual(0.5, actual['A']);
      Assert.AreEqual(0.5, actual['B']);
    }
  }
}
