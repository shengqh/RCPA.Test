using NUnit.Framework;

namespace RCPA.Proteomics.Modification
{
  [TestFixture]
  public class TestModificationCountCalculator
  {
    [Test]
    public void TestCalculate()
    {
      var calc = new ModificationCountCalculator("STY");
      Assert.AreEqual(0, calc.Calculate("K.GGGSTYR.A"));
      Assert.AreEqual(0, calc.Calculate("K.G*GGSTYR.A"));
      Assert.AreEqual(1, calc.Calculate("K.G*GGS@TYR.A"));
      Assert.AreEqual(1, calc.Calculate("K.G*GGST@YR.A"));
      Assert.AreEqual(1, calc.Calculate("K.G*GGSTY@R.A"));
      Assert.AreEqual(2, calc.Calculate("K.G*GGS@T#YR.A"));
      Assert.AreEqual(3, calc.Calculate("K.G*GGS@T#Y^R.A"));
    }
  }
}