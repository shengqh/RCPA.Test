using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestByteConvert
  {
    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestConstruction()
    {
      new ByteConvert(true, 14);
    }
  }

  public abstract class AbstractTestConvertEndian32
  {
    protected IByteConvert convert = null;

    protected abstract void InitializeConvert();

    [OneTimeSetUp]
    public void Setup()
    {
      InitializeConvert();
    }

    [Test]
    public void TestValidateLength1()
    {
      convert.ValidateLength(new byte[4]);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void TestValidateLengthException()
    {
      convert.ValidateLength(null);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestValidateLengthException2()
    {
      convert.ValidateLength(new byte[3]);
    }

    [Test]
    public void TestValidateLength2()
    {
      convert.ValidateLength(new byte[16], 4);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestValidateLengthException2_2()
    {
      convert.ValidateLength(new byte[16], 3);
    }
  }

  public abstract class AbstractTestConvertEndian64
  {
    protected IByteConvert convert = null;

    protected abstract void InitializeConvert();

    [OneTimeSetUp]
    public void Setup()
    {
      InitializeConvert();
    }

    [Test]
    public void TestValidateLength1()
    {
      convert.ValidateLength(new byte[8]);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void TestValidateLengthException()
    {
      convert.ValidateLength(null);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestValidateLengthException2()
    {
      convert.ValidateLength(new byte[7]);
    }

    [Test]
    public void TestValidateLength2()
    {
      convert.ValidateLength(new byte[16], 2);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestValidateLengthException2_2()
    {
      convert.ValidateLength(new byte[16], 3);
    }
  }
}
