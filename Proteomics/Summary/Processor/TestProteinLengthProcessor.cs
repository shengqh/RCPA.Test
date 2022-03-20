using NUnit.Framework;

namespace RCPA.Proteomics.Summary.Processor
{
  [TestFixture]
  public class TestProteinLengthProcessor
  {
    string shortSeq = "AAAAA";
    string longSeq = "BBBBBBBBBB";
    IIdentifiedProteinGroup group;

    [SetUp]
    public void SetUp()
    {
      group = new IdentifiedProteinGroup();
      group.Add(new IdentifiedProtein("SHORT") { Sequence = shortSeq });
      group.Add(new IdentifiedProtein("LONG") { Sequence = longSeq });
    }

    [Test]
    public void TestLongerProcess()
    {
      IIdentifiedProteinGroup finalGroup = new ProteinLengthProcessor(true).Process(group);

      Assert.AreEqual(1, finalGroup.Count);
      Assert.AreEqual(longSeq, finalGroup[0].Sequence);
    }

    [Test]
    public void TestShorterProcess()
    {
      IIdentifiedProteinGroup finalGroup = new ProteinLengthProcessor(false).Process(group);

      Assert.AreEqual(1, finalGroup.Count);
      Assert.AreEqual(shortSeq, finalGroup[0].Sequence);
    }
  }
}
