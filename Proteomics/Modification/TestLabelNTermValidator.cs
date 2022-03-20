using NUnit.Framework;

namespace RCPA.Proteomics.Modification
{
  [TestFixture]
  public class TestLabelNTermValidator
  {
    [Test]
    public void TestValidate()
    {
      LabelNTermValidator validator = new LabelNTermValidator("KQ");
      Assert.IsFalse(validator.Validate(null));
      Assert.IsFalse(validator.Validate(""));

      Assert.IsTrue(validator.Validate("KDDDDDD"));
      Assert.IsTrue(validator.Validate("K*DDDDDD"));
      Assert.IsTrue(validator.Validate("QDDDDDD"));
      Assert.IsTrue(validator.Validate("Q*DDDDDD"));

      Assert.IsFalse(validator.Validate("DDDKDDQN"));
      Assert.IsFalse(validator.Validate("DDDK*DDQN*"));
    }
  }
}
