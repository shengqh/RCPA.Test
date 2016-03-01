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
    public void TestPrepareOptions()
    {

      var options = new DeuteriumCalculatorOptions()
      {
        InputFile = "aaa",
        RawDirectory = "bbb"
      };

      Assert.IsFalse(options.PrepareOptions());
      Assert.IsTrue(options.ParsingErrors.Any(l => l.Contains("aaa")));
      Assert.IsTrue(options.ParsingErrors.Any(l => l.Contains("bbb")));
    }
  }
}
