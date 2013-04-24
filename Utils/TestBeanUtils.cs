using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestBeanUtils
  {
    class One
		{
			public int Value1 { get; set; }
			public string Value2 { get; set; }
			public double Value3 { get; set; }
			public int Value4 { get; set; }
			public int Value5 { get; set; }
		}

    class Another
		{
			public int Value1 { get; set; }
			public string Value2 { get; set; }
			public double Value3 { get; set; }
		}

    [Test]
    public void Test()
    {
      var one = new One()
      {
        Value1 = 1,
        Value2 = "2",
        Value3 = 3.3,
        Value4 = 4,
        Value5 = 5
      };

      var two = new Another();
      BeanUtils.CopyPropeties(one, two);

      Assert.AreEqual(1, two.Value1);
      Assert.AreEqual("2", two.Value2);
      Assert.AreEqual(3.3, two.Value3);
    }
  }
}
