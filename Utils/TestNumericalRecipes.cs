using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestNumericalReceipes
  {
    const long IA = 16807;
    const long IM = 2147483647;
    const double AM = (1.0 / IM);
    const long IQ = 127773;
    const long IR = 2836;
    const int NTAB = 32;
    const double NDIV = (1 + (IM - 1) / NTAB);
    const double EPS = 1.2e-7;
    const double RNMX = (1.0 - EPS);

    static long iy = 0;
    static long[] iv = new long[NTAB];

    public static double ran1(ref long idum)
    {
      int j;
      long k;
      double temp;

      if (idum <= 0 || iy != 0)
      {
        if (-idum < 1) idum = 1;
        else idum = -idum;
        for (j = NTAB + 7; j >= 0; j--)
        {
          k = idum / IQ;
          idum = IA * (idum - k * IQ) - IR * k;
          if (idum < 0) idum += IM;
          if (j < NTAB) iv[j] = idum;
        }
        iy = iv[0];
      }
      k = idum / IQ;
      idum = IA * (idum - k * IQ) - IR * k;
      if (idum < 0) idum += IM;
      j = (int)(iy / NDIV);
      iy = iv[j];
      iv[j] = idum;
      if ((temp = AM * iy) > RNMX) return RNMX;
      else return temp;
    }

    private static int iset = 0;
    private static double gset = 0.0;

    public static double gasdev(ref long idum)
    {
      double fac, rsq, v1, v2;

      if (iset == 0)
      {
        do
        {
          v1 = 2.0 * ran1(ref idum) - 1.0;
          v2 = 2.0 * ran1(ref idum) - 1.0;
          rsq = v1 * v1 + v2 * v2;
        } while (rsq >= 1.0 || rsq == 0.0);
        fac = Math.Sqrt(-2.0 * Math.Log(rsq) / rsq);
        gset = v1 * fac;
        iset = 1;
        return v2 * fac;
      }
      else
      {
        iset = 0;
        return gset;
      }
    }

    const int NPT = 100;
    const double SPREAD = 0.5;

    [Test]
    public void TestFit2()
    {
      double a1 = 0.0, b1 = 0.0, chi2 = 0.0, q = 0.0, siga = 0.0, sigb = 0.0;
      double[] x, y;

      x = new double[] { 2845043.471, 2904192.16, 3222368.576, 3223051.761, 3779554.361, 3784043.963, 3836284.445, 3916942.383, 3919616.715, 4431096.042, 4434133.709, 13674181.76, 15275113.86 };
      y = new double[] { 2238887.514, 2944982.476, 3274840.433, 2924749.938, 3369602.546, 3009743.359, 3021352.624, 3451250.443, 3234344.226, 2963374.234, 3699316.076, 9507378.289, 13379423.79 };

      new NumericalRecipes().fit(x, y, null, false, ref a1, ref b1, ref siga, ref sigb, ref chi2, ref q);

      Assert.AreEqual(0.7769, b1, 0.0001);
      Assert.AreEqual(247841, a1, 1);
    }

    [Test]
    public void TestFitexy()
    {
      var p_x = new double[] { 2845043.471, 2904192.16, 3222368.576, 3223051.761, 3779554.361, 3784043.963, 3836284.445, 3916942.383, 3919616.715, 4431096.042, 4434133.709, 13674181.76, 15275113.86 };
      var p_y = new double[] { 2238887.514, 2944982.476, 3274840.433, 2924749.938, 3369602.546, 3009743.359, 3021352.624, 3451250.443, 3234344.226, 2963374.234, 3699316.076, 9507378.289, 13379423.79 };

      double a1 = 0.0, b1 = 0.0, a2 = 0.0, b2 = 0.0, siga = 0.0, sigb = 0.0, chi2 = 0.0, q = 0.0;
      new NumericalRecipes().fitexy(p_x, p_y, null, null, ref a1, ref b1, ref siga, ref sigb, ref chi2, ref q);
      new NumericalRecipes().fitexy(p_y, p_x, null, null, ref a2, ref b2, ref siga, ref sigb, ref chi2, ref q);

      Assert.AreEqual(0.7882, b1, 0.0001);
      Assert.AreEqual(1 / b1, b2, 0.01);

      var p_sx = new double[p_x.Length];
      var p_sy = new double[p_x.Length];
      for (int i = 0; i < p_sx.Length; i++)
      {
        p_sx[i] = p_sy[i] = 0.0001;
      }
      new NumericalRecipes().fitexy(p_y, p_x, p_sy, p_sx, ref a2, ref b2, ref siga, ref sigb, ref chi2, ref q);
      Assert.AreEqual(0.7882, b1, 0.0001);
      Assert.AreEqual(1 / b1, b2, 0.01);
    }

    [Test]
    public void TestFit0()
    {
      double b1 = 0.0;
      double[] x, y;

      x = new double[] { 2845043.471, 2904192.16, 3222368.576, 3223051.761, 3779554.361, 3784043.963, 3836284.445, 3916942.383, 3919616.715, 4431096.042, 4434133.709, 13674181.76, 15275113.86 };
      y = new double[] { 2238887.514, 2944982.476, 3274840.433, 2924749.938, 3369602.546, 3009743.359, 3021352.624, 3451250.443, 3234344.226, 2963374.234, 3699316.076, 9507378.289, 13379423.79 };

      new NumericalRecipes().fit0(x, y, ref b1);

      Assert.AreEqual(0.807, b1, 0.0001);
    }

    [Test]
    public void TestFitexy0()
    {
      double b1 = 0.0, b2 = 0.0;
      double[] x, y;

      x = new double[] { 2845043.471, 2904192.16, 3222368.576, 3223051.761, 3779554.361, 3784043.963, 3836284.445, 3916942.383, 3919616.715, 4431096.042, 4434133.709, 13674181.76, 15275113.86 };
      y = new double[] { 2238887.514, 2944982.476, 3274840.433, 2924749.938, 3369602.546, 3009743.359, 3021352.624, 3451250.443, 3234344.226, 2963374.234, 3699316.076, 9507378.289, 13379423.79 };

      x = (from i in x
           select i / 10).ToArray();

      new NumericalRecipes().fitexy0(x, y, ref b1);
      new NumericalRecipes().fitexy0(y, x, ref b2);

      Assert.AreEqual(1 / b1, b2, 0.01);
    }

  }
}
