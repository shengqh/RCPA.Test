using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Image
{
  public class AbstractTestMatcher
  {
    protected void AssertPeak(MatchedPeak p, string displayName, double mz)
    {
      Assert.AreEqual(displayName, p.DisplayName);
      Assert.AreEqual(mz, p.Mz, 0.01);
    }
  }
}
