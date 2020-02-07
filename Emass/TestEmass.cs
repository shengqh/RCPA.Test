using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.emass;

namespace RCPA.Emass
{
  [TestFixture]
  public class TestEmass
  {
    [Test]
    public void TestInitializeData()
    {
      var calc = new EmassCalculator(@TestContext.CurrentContext.TestDirectory + "/../../../data//ISOTOPE.DAT");

      var fm = "C6H10";

      var result = calc.Calculate(fm, 0, 0);

      //result.ForEach(m => Console.WriteLine("{0:0.000000}\t{1:0.000000}", m.Mz, m.Intensity));

      Assert.AreEqual(82.078250, result[0].Mz, 0.00001);
      Assert.AreEqual(0.93641, result[0].Intensity, 0.00001);

      Assert.AreEqual(83.08166, result[1].Mz, 0.00001);
      Assert.AreEqual(0.06185, result[1].Intensity, 0.00001);

      Assert.AreEqual(84.08508, result[2].Mz, 0.00001);
      Assert.AreEqual(0.00171, result[2].Intensity, 0.00001);

      Assert.AreEqual(85.08854, result[3].Mz, 0.00001);
      Assert.AreEqual(0.00003, result[3].Intensity, 0.00001);
    }
  }
}
