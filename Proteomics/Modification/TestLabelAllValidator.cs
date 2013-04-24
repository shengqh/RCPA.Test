using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Modification
{
  [TestFixture]
  public class TestLabelAllValidator
  {
    [Test]
    public void TestValidate()
    {
      ILabelValidator validator = new LabelAllValidator("KQ");

      Assert.IsFalse(validator.Validate(null));
      Assert.IsFalse(validator.Validate(""));
      
      Assert.IsTrue(validator.Validate("DDDQDKDD"));
      Assert.IsTrue(validator.Validate("DDDDQ#DK*DD"));

      Assert.IsFalse(validator.Validate("DDD"));
      Assert.IsFalse(validator.Validate("DDDKDDQ*N"));
    }
  }
}
