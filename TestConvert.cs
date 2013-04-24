using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;
using System.Globalization;

namespace RCPA
{
  [TestFixture]
  public class TestConvert
  {
    [Test]
    public void TestCulture()
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

      var exp = "123.456";

      var d = Convert.ToDouble(exp);
      Assert.AreEqual(123456, d);

      d = MyConvert.ToDouble("123.456");
      Assert.AreEqual(123.456, d, 0.001);

      var ds = string.Format("{0:0.0000}", d);
      Assert.AreEqual("123,4560", ds);

      ds = MyConvert.Format("{0:0.0000}", d);
      Assert.AreEqual("123.4560", ds);
    }
  }
}
