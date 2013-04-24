using NUnit.Framework;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestExpectValueCalculator
  {
    [Test]
    public void Test()
    {
      Assert.AreEqual(0.046, ExpectValueCalculator.Calc(40.95, 11521, 0.05), 0.001);
      Assert.AreEqual(1.2e-7, ExpectValueCalculator.Calc(89.10, 1949, 0.05), 1e-8);
      Assert.AreEqual(19, ExpectValueCalculator.Calc(11.58, 5370, 0.05), 1);
    }
  }
}