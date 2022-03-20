using NUnit.Framework;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestScoreDistributionXmlFormat
  {
    [Test]
    public void TestRead()
    {
      ScoreDistribution sd = new ScoreDistributionXmlFormat().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/test.scoreDistribution");
      Assert.AreEqual(2, sd.Count);
      Assert.AreEqual(2, sd.Keys.First().PrecursorCharge);
      Assert.AreEqual(0, sd.Keys.First().MissCleavageSiteCount);
      Assert.AreEqual(0, sd.Keys.First().ModificationCount);

      Assert.AreEqual(2, sd.Values.First().Count);
      Assert.AreEqual(25, sd.Values.First()[0].Score);
      Assert.AreEqual(2, sd.Values.First()[0].PeptideCountFromTargetDB);
      Assert.AreEqual(1, sd.Values.First()[0].PeptideCountFromDecoyDB);
      Assert.AreEqual(26, sd.Values.First()[1].Score);
      Assert.AreEqual(8, sd.Values.First()[1].PeptideCountFromTargetDB);
      Assert.AreEqual(1, sd.Values.First()[1].PeptideCountFromDecoyDB);
    }
  }
}
