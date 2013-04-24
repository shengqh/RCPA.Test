using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestConvertBigEndian32 : AbstractTestConvertEndian32
  {
    protected override void InitializeConvert()
    {
      convert = new ByteConvert(true, 32);
    }

    [Test]
    public void TestByteToArray()
    {
      byte[] bytes = new byte[] { 69, 179, 149, 32, 69, 6, 126, 78 };

      double[] ints = convert.ByteToArray(bytes);

      Assert.AreEqual(2, ints.Length);

      Assert.AreEqual(5746.6, ints[0], 0.1);

      Assert.AreEqual(2151.9, ints[1], 0.1);
    }
  }
}
