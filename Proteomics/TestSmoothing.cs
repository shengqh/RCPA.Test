using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestSmoothing
  {
    [Test]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestSavitzkyGolaySmoothingConstruction()
    {
      SavitzkyGolaySmoothing sgs = new SavitzkyGolaySmoothing(3);
    }

    [Test]
    public void TestSavitzkyGolaySmoothing()
    {
      double[] values = new double[] { 1, 2, 3, 10, 11, 12 };
      SavitzkyGolaySmoothing sgs = new SavitzkyGolaySmoothing(5);
      sgs.Smooth(values);
      Assert.AreEqual(1, values[0]);
      Assert.AreEqual(2, values[1]);
      Assert.AreEqual(4.543, values[2], 0.001);
      Assert.AreEqual(8.986, values[3], 0.001);
      Assert.AreEqual(11, values[4]);
      Assert.AreEqual(12, values[5]);
    }
  }
}
