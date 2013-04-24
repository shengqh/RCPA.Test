using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestScoreDistribution
  {
    [Test]
    public void Test()
    {
      ScoreDistribution s = new ScoreDistribution();
      OptimalResultCondition or = new OptimalResultCondition(1, 1, 1, 1);

      s[or] = new List<OptimalResult>(){
        new OptimalResult(1.0, 1.0, 100,10,0.1),
        new OptimalResult(2.0, 1.0, 200,10,0.05),
        new OptimalResult(3.0, 1.0, 1000,10,0.01)
      };

      ScoreDistribution subset = new ScoreDistribution();
      subset[or] = new List<OptimalResult>(){
        new OptimalResult(2.0, 1.0, 100,0,0),
        new OptimalResult(3.0, 1.0, 900,0,0)
      };

      IFalseDiscoveryRateCalculator calc = new TargetFalseDiscoveryRateCalculator();

      double fdr = s.CalculateSubsetFdr(subset, calc);

      Assert.AreEqual(0.014, fdr);
    }
  }
}
