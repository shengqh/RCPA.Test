using NUnit.Framework;

namespace RCPA.Numerics
{
  [TestFixture]
  public class TestGrubbsTestOutlierDetector
  {
    double[] values1 = new double[] { 3.2, 3.3, 8.1, 3.2, 2.9, 3.7, 3.1, 3.5, 3.3, 9.2 };
    double[] values2 = new double[] { 3.2, 3.3, 3.2, 2.9, 3.7, 3.1, 3.5, 3.3, 9.2 };
    GrubbsTestOutlierDetector detector = new GrubbsTestOutlierDetector(0.05);

    [Test]
    public void TestGetProbability()
    {
      var ps = GrubbsTestOutlierDetector.GetProbability(values1);
      Assert.AreEqual(6.26, ps[0], 0.01);
    }

    [Test]
    public void TestDetect()
    {
      var detector = new GrubbsTestOutlierDetector(0.05);
      var values = new double[] { 3.2, 3.3, 8.1, 3.2, 2.9, 3.7, 3.1, 3.5, 3.3, 9.2 };
      var outliers = detector.Detect(values);
      Assert.AreEqual(0, outliers.Count);

      values = new double[] { 3.2, 3.3, 3.2, 2.9, 3.7, 3.1, 3.5, 3.3, 9.2 };
      outliers = detector.Detect(values);
      Assert.AreEqual(1, outliers.Count);
      Assert.AreEqual(9.2, values[outliers[0]], 0.1);
    }
  }
}
