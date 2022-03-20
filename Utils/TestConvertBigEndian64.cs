using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestConvertBigEndian64 : AbstractTestConvertEndian64
  {
    protected override void InitializeConvert()
    {
      convert = new ByteConvert(true, 64);
    }

    [Test]
    public void TestByteToArray()
    {
      byte[] bytes = new byte[] { 64, 77, 8, 134, 199, 222, 201, 42, 64, 82, 5, 69, 100, 21, 226, 227 };

      double[] mzs = convert.ByteToArray(bytes);

      Assert.AreEqual(2, mzs.Length);

      Assert.AreEqual(58.0666, mzs[0], 0.0001);

      Assert.AreEqual(72.0824, mzs[1], 0.0001);
    }
  }
}
