using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Modification
{
  [TestFixture]
  public class TestLabelCTermValidator
  {
    [Test]
    public void TestValidate()
    {
      LabelCTermValidator validator = new LabelCTermValidator("KQ");
      Assert.IsFalse(validator.Validate(null));
      Assert.IsFalse(validator.Validate(""));

      Assert.IsTrue(validator.Validate("DDDDDDK"));
      Assert.IsTrue(validator.Validate("DDDDDDK*"));
      Assert.IsTrue(validator.Validate("DDDDDDQ"));
      Assert.IsTrue(validator.Validate("DDDDDDQ*"));
      
      Assert.IsFalse(validator.Validate("DDDKDDQN"));
      Assert.IsFalse(validator.Validate("DDDKDDQN*"));
    }
  }
}
