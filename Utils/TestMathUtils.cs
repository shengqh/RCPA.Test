using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestMathUtils
  {
    [Test]
    public void TestExp10()
    {
      Assert.AreEqual(100.0, MathUtils.Exp10(2), 0.00001);
    }

    [Test]
    public void TestInt32BitsToSingle()
    {
      float a = 2.34F;

      int bits = 1075167887;

      Assert.AreEqual(bits, MathUtils.SingleToInt32Bits(a));

      Assert.AreEqual(a, MathUtils.Int32BitsToSingle(bits));
    }

    [Test]
    public void TestByte64ToDoubleList()
    {
      byte[] bytes = new byte[] { 64, 77, 8, 134, 199, 222, 201, 42, 64, 82, 5, 69, 100, 21, 226, 227 };

      double[] mzs = MathUtils.Byte64ToDoubleList(2, true, bytes);

      Assert.AreEqual(2, mzs.Length);

      Assert.AreEqual(58.0666, mzs[0], 0.0001);

      Assert.AreEqual(72.0824, mzs[1], 0.0001);
    }

    [Test]
    public void TestByte32ToDoubleList()
    {
      byte[] bytes = new byte[] { 69, 179, 149, 32, 69, 6, 126, 78 };

      double[] ints = MathUtils.Byte32ToDoubleList(2, true, bytes);

      Assert.AreEqual(2, ints.Length);

      Assert.AreEqual(5746.6, ints[0], 0.1);

      Assert.AreEqual(2151.9, ints[1], 0.1);
    }

    [Test]
    public void TestPositiveQuadraticFunction()
    {
      Assert.AreEqual(2, MathUtils.PositiveQuadraticFunction(1.6667, -1.6667, -3.3333), 0.0001);
      Assert.AreEqual(1, MathUtils.PositiveQuadraticFunction(2.2222, 0, -2.2222), 0.0001);
    }

    [Test]
    public void TestPositiveRatio()
    {
      Assert.AreEqual(1, MathUtils.CarlibrateForwardReverseRatio(1.2222, 1.2222), 0.0001);
      Assert.AreEqual(2, MathUtils.CarlibrateForwardReverseRatio(2.3333, 0.6667), 0.0001);

      Assert.AreEqual(1, MathUtils.CarlibrateForwardReverseRatio(0.8, 0.8), 0.0001);
    }
  }
}
