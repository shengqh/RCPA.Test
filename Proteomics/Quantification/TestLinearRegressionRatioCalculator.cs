using NUnit.Framework;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestLinearRegressionRatioCalculator
  {
    public void TestDistance()
    {
      double[][] a = new double[][]
      {
        new double[]{ 1, 2, 3, 4 } ,
        new double[]{ 2, 4, 6, 8 }
      };

      var result = LinearRegressionRatioCalculator.CalculateRatioDistance(a);
      Assert.AreEqual(2, result.Ratio);
      Assert.AreEqual(0, result.Distance);

      double[][] b = new double[][]
      {
        new double[]{ 1, 2, 3, 4 } ,
        new double[]{ 3, 5, 7, 9 }
      };

      result = LinearRegressionRatioCalculator.CalculateRatioDistance(b);
      Assert.AreEqual(2, result.Ratio, 0.001);
      Assert.AreEqual(1, result.Distance, 0.001);
    }
  }
}
