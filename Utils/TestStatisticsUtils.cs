using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestStatisticsUtils
  {
    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestGetCombinationProbabilityException()
    {
      StatisticsUtils.GetCombinationProbability(4, 1, 0.5, 0.5);
    }

    [Test]
    public void TestGetCombinationProbability()
    {
      Assert.AreEqual(0.25, StatisticsUtils.GetCombinationProbability(1, 4, 0.5, 0.5));
    }

    [Test]
    public void TestRSquareCrossOrigin()
    {
      double[] b = { 1, 3, 2, 5, 4 };

      double[] expectb = { 0.9636364, 1.9272727, 2.8909091, 3.8545455, 4.8181818 };

      double rsquare = StatisticsUtils.RSquare(b, expectb);
      Assert.AreEqual(0.6073, rsquare, 0.0001);
    }

    [Test]
    public void TestFDistributionCriticalValue()
    {
      Assert.AreEqual(0.95, StatisticsUtils.FDistributionCriticalValue(1, 3, 10.13), 0.01);
      Assert.AreEqual(0.99, StatisticsUtils.FDistributionCriticalValue(1, 3, 34.12), 0.01);
    }

    [Test]
    public void TestFProbabilityForLinearRegression()
    {
      Assert.AreEqual(0.95, StatisticsUtils.FProbabilityForLinearRegression(5, 10.13), 0.01);
    }

    [Test]
    public void TestCorrel()
    {
      double[] a = { 1.19, 1.34, 1.38, 1.11, 1.01, 0.8, 1.22, 1.55, 1.35, 1.17, 0.98, 2.15, 1.02, 1.64, 0.81, 0.72, 1.5, 1.63, 1.32 };
      double[] b = { 1.04, 0.89, 1.26, 0.82, 0.62, 0.52, 0.73, 2.09, 1.48, 1.41, 0.56, 1.4, 1.3, 0.89, 0.89, 1.11, 1.46, 1.64, 0.85 };
      Assert.AreEqual(0.538, StatisticsUtils.PearsonCorrelation(a, b), 0.001);
      Assert.AreEqual(0.9551, StatisticsUtils.CosineAngle(a, b), 0.0001);
    }

    [Test]
    public void TestCosineAngle()
    {
      double[] a = { 1, 2, 3, 4, 5, 6};
      double[] b = { 11, 22, 33, 44, 55, 66 };
      Assert.AreEqual(1, StatisticsUtils.CosineAngle(a,b), 0.0001);
    }

    [Test]
    public void TestKullbackLeibleDistance()
    {
      double[] a = { 1.5, 2.0, 2.5 };
      double[] b = { 1, 2, 3 };
      Assert.AreEqual(0.0353, StatisticsUtils.KullbackLeiblerDistance(a, b, 3), 0.0001);
    }
    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestCorrelException1()
    {
      double[] b = { 1.04};
      StatisticsUtils.PearsonCorrelation(null, b);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestCorrelException2()
    {
      double[] b = { 1.04 };
      StatisticsUtils.PearsonCorrelation(new double[]{}, b);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestCorrelException3()
    {
      double[] a = { 1.19, 1.34 };
      double[] b = { 1.04 };
      StatisticsUtils.PearsonCorrelation(a, b);
    }

    [Test]
    public void TestSpearmanCorrelation()
    {
      double[] a = { 1.19, 1.34, 1.38, 1.11, 1.01, 0.8, 1.22, 1.55, 1.35, 1.17, 0.98, 2.15, 1.02, 1.64, 0.81, 0.72, 1.5, 1.63, 1.32 };
      double[] b = { 1.04, 0.89, 1.26, 0.82, 0.62, 0.52, 0.73, 2.09, 1.48, 1.41, 0.56, 1.4, 1.3, 0.89, 0.89, 1.11, 1.46, 1.64, 0.85 };
      Assert.AreEqual(0.5870, StatisticsUtils.SpearmanCorrelation(a.ToList(),b.ToList()), 0.0001);
    }
  }
}
