using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestConvertLittleEndian64 : AbstractTestConvertEndian64
  {
    protected override void InitializeConvert()
    {
      convert = new ByteConvert(false, 64);
    }

    [Test]
    public void TestByteToArray()
    {
      byte[] bytes = new byte[] { 42, 201, 222, 199, 134, 8, 77, 64, 227, 226, 21, 100, 69, 5, 82, 64 };

      double[] mzs = convert.ByteToArray(bytes);

      Assert.AreEqual(2, mzs.Length);

      Assert.AreEqual(58.0666, mzs[0], 0.0001);

      Assert.AreEqual(72.0824, mzs[1], 0.0001);
    }
  }
}
