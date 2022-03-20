using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestFastaFormat
  {
    [Test]
    public void TestReadSequence()
    {
      FastaFormat ff = new FastaFormat();
      StreamReader sr = new StreamReader(TestContext.CurrentContext.TestDirectory + "/../../../data//test.fasta");
      Sequence test1 = ff.ReadSequence(sr);
      Assert.AreEqual("test1", test1.Name);
      Assert.AreEqual("AAAAA", test1.SeqString);
      Sequence test2 = ff.ReadSequence(sr);
      Assert.AreEqual("test2", test2.Name);
      Assert.AreEqual("BBBBB", test2.SeqString);
      Sequence test3 = ff.ReadSequence(sr);
      Assert.IsNull(test3);
    }

    [Test]
    public void TestReadFromFasta()
    {
      FastaFormat ff = new FastaFormat();
      List<Sequence> seqs = SequenceUtils.Read(ff, @TestContext.CurrentContext.TestDirectory + "/../../../data//Standard_Protein_FIT_060222.noredundant.fasta");
      Assert.AreEqual(43, seqs.Count);
    }
  }
}
