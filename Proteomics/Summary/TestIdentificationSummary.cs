using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentificationSummary
  {
    [Test]
    public void TestParse()
    {
      var result = IdentificationSummary.Parse(@TestContext.CurrentContext.TestDirectory + "/../../../data/buildsummary/hppp_mascot.noredundant", "^REV", new TotalFalseDiscoveryRateCalculator());
      Assert.AreEqual(381, result.FullSpectrumCount);
      Assert.AreEqual(62, result.FullPeptideCount);
      Assert.AreEqual(31, result.ProteinGroupCount);
      Assert.AreEqual(16, result.Unique2ProteinGroupCount);
    }
  }
}
