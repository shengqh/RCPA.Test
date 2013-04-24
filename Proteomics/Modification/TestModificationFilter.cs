using NUnit.Framework;

namespace RCPA.Proteomics.Modification
{
  [TestFixture]
  public class TestModificationFilter
  {
    [Test]
    public void TestFilter()
    {
      string seq1 = "A.BCDE#FERG*J.R";
      string seq2 = "A.BCDEFERG*J.R";

      var f = new ModificationFilter("EJ");
      Assert.IsTrue(f.Accept(seq1));
      Assert.IsFalse(f.Accept(seq2));

      f = new ModificationFilter("EG");
      Assert.IsTrue(f.Accept(seq1));
      Assert.IsTrue(f.Accept(seq2));
    }
  }
}