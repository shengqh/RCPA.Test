using NUnit.Framework;

namespace RCPA.Numerics
{
  [TestFixture]
  public class TestMADOutlierDetector
  {
    [Test]
    public void TestDetect()
    {
      var values = new double[] { 3.2, 3.3, 8.1, 3.2, 2.9, 3.7, 3.1, 3.5, 3.3, 9.2 };
      var outliers = new MADOutlierDetector().Detect(values);
      Assert.AreEqual(2, outliers.Count);
      Assert.AreEqual(2, outliers[0]);
      Assert.AreEqual(9, outliers[1]);
    }
  }
}
