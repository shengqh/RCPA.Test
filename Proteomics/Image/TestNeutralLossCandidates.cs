using NUnit.Framework;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestNeutralLossCandidates
  {
    [Test]
    public void TestConstruction()
    {
      NeutralLossCandidates candidates = new NeutralLossCandidates("R.FAS*K.F");

      Assert.IsTrue(candidates.CanLossWater);
      Assert.IsTrue(candidates.CanLossAmmonia);

      Assert.IsFalse(candidates.BLossWater[0]);
      Assert.IsFalse(candidates.BLossWater[1]);
      Assert.IsTrue(candidates.BLossWater[2]);
      Assert.IsTrue(candidates.BLossWater[3]);

      Assert.IsFalse(candidates.YLossWater[0]);
      Assert.IsTrue(candidates.YLossWater[1]);
      Assert.IsTrue(candidates.YLossWater[2]);
      Assert.IsTrue(candidates.YLossWater[3]);

      Assert.IsFalse(candidates.BLossAmmonia[0]);
      Assert.IsFalse(candidates.BLossAmmonia[1]);
      Assert.IsFalse(candidates.BLossAmmonia[2]);
      Assert.IsTrue(candidates.BLossAmmonia[3]);

      Assert.IsTrue(candidates.YLossAmmonia[0]);
      Assert.IsTrue(candidates.YLossAmmonia[1]);
      Assert.IsTrue(candidates.YLossAmmonia[2]);
      Assert.IsTrue(candidates.YLossAmmonia[3]);
    }
  }
}
