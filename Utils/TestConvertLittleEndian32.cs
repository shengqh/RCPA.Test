using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestConvertLittleEndian32 : AbstractTestConvertEndian32
  {
    protected override void InitializeConvert()
    {
      convert = new ByteConvert(false, 32);
    }

    [Test]
    public void TestByteToArray()
    {
      byte[] bytes = new byte[] { 32, 149, 179, 69, 78, 126, 6, 69 };

      double[] ints = convert.ByteToArray(bytes);

      Assert.AreEqual(2, ints.Length);

      Assert.AreEqual(5746.6, ints[0], 0.1);

      Assert.AreEqual(2151.9, ints[1], 0.1);
    }
  }
}
