using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Deuterium
{
  [TestFixture]
  public class TestDeuteriumRatioBuilderOptions
  {
    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestPrepareOptions()
    {

      var options = new DeuteriumCalculatorOptions()
      {
        InputFile = "aaa",
      };

      Assert.IsFalse(options.PrepareOptions());
      Assert.IsTrue(options.ParsingErrors.Any(l => l.Contains("aaa")));
    }
  }
}
