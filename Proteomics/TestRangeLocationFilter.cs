using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestRangeLocationFilter
  {
    [Test]
    public void TestNGlycanFilter()
    {
      NGlycanFilter filter = new NGlycanFilter();
      filter.SetSequence(new Seq.Sequence("Test", "AAANESAAR"));
      Assert.IsTrue(filter.Accept(new RangeLocation(1, 9)));
      filter.SetSequence(new Seq.Sequence("Test", "BBBNPSBBK"));
      Assert.IsFalse(filter.Accept(new RangeLocation(1, 9)));
      filter.SetSequence(new Seq.Sequence("Test", "FFFNITFFR"));
      Assert.IsTrue(filter.Accept(new RangeLocation(1, 9)));
      filter.SetSequence(new Seq.Sequence("Test", "GGGNDTGGR"));
      Assert.IsFalse(filter.Accept(new RangeLocation(1, 9)));
    }
  }
}
