using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestFollowCandidate
  {
    [Test]
    public void TestConstruction()
    {
      string info = "-.MAS*ESETLNPSAR.I(2.6617,0.0182)";
      var fc = new FollowCandidate(info);
      Assert.AreEqual("-.MAS*ESETLNPSAR.I", fc.Sequence);
      Assert.AreEqual(2.6617, fc.Score, 0.0001);
      Assert.AreEqual(0.0182, fc.DeltaScore, 0.0001);
      Assert.AreEqual(info, fc.ToString());
    }
  }
}