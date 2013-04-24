using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestNonNegativeLeastSquaresCalc
  {
    [Test]
    public void TestNNLS_1()
    {
      double[][] a = new double[2][];
      a[0] = new double[] { 1, 2, 3, 4 };
      a[1] = new double[] { 1, 4, 9, 16 };

      double[] b = new double[] { 0.6, 2.2, 4.8, 8.4 };

      double[] x = new double[2];

      double rnorm;

      NonNegativeLeastSquaresCalc.NNLS(a, 4, 2, b, x, out rnorm, null, null, null);

      Assert.AreEqual(0.1, x[0], 0.01);
      Assert.AreEqual(0.5, x[1], 0.01);
    }

    [Test]
    public void TestNNLS_2()
    {
      double[][] a = new double[3][];
      a[0] = new double[] { 1, 2, 3, 4 };
      a[1] = new double[] { 1, 4, 9, 16 };
      a[2] = new double[] { 1, 8, 27, 64 };

      double[] b = new double[] { 0.73, 3.24, 8.31, 16.72 };

      double[] x = new double[3];

      double rnorm;

      NonNegativeLeastSquaresCalc.NNLS(a, 4, 3, b, x, out rnorm, null, null, null);

      Assert.AreEqual(0.1, x[0], 0.01);
      Assert.AreEqual(0.5, x[1], 0.01);
      Assert.AreEqual(0.13, x[2], 0.01);
    }

    [Test]
    public void TestNNLS_3()
    {
      double[][] a = new double[4][];
      a[0] = new double[] { 1, 2, 3, 4 };
      a[1] = new double[] { 1, 4, 9, 16 };
      a[2] = new double[] { 1, 8, 27, 64 };
      a[3] = new double[] { 1, 16, 81, 256 };

      double[] b = new double[] { 0.73, 3.24, 8.31, 16.72 };

      double[] x = new double[4];

      double rnorm;

      NonNegativeLeastSquaresCalc.NNLS(a, 4, 4, b, x, out rnorm, null, null, null);

      Assert.AreEqual(0.1, x[0], 0.01);
      Assert.AreEqual(0.5, x[1], 0.01);
      Assert.AreEqual(0.13, x[2], 0.01);
      Assert.AreEqual(0, x[3], 0.01);
    }

    [Test]
    public void TestNNLS_4()
    {
      double[][] a = new double[3][];
      a[0] = new double[] { 1, 2, 3, 4 };
      a[1] = new double[] { 1, 4, 9, 16 };
      a[2] = new double[] { 1, 8, 27, 64 };

      double[] b = new double[] { 0.23, 1.24, 3.81, 8.72 };

      double[] x = new double[3];

      double rnorm;

      NonNegativeLeastSquaresCalc.NNLS(a, 4, 3, b, x, out rnorm, null, null, null);

      Assert.AreEqual(0.1, x[0], 0.01);
      Assert.AreEqual(0.0, x[1], 0.01);
      Assert.AreEqual(0.13, x[2], 0.01);
    }

  }
}
