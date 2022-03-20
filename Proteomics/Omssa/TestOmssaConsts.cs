using NUnit.Framework;

namespace RCPA.Proteomics.Omssa
{
  [TestFixture]
  public class TestOmssaConsts
  {
    [Test]
    public void TestEnzymeMap()
    {
      Assert.AreEqual(27, OmssaConsts.EnzymeMap.Count);
      Assert.AreEqual("trypsin", OmssaConsts.EnzymeMap["0"]);
      Assert.AreEqual("none", OmssaConsts.EnzymeMap["255"]);
    }
  }
}
