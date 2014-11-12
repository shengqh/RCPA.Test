using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using Moq;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestPrecursorNeutralLossMatcher
  {
    [Test]
    public void TestMatch()
    {
      PrecursorNeutralLossMatcher matcher = new PrecursorNeutralLossMatcher(0.5, 0.0);
      var factory = new MockRepository(MockBehavior.Strict);

      var sr = factory.Create<IIdentifiedPeptideResult>();
      sr.Setup(x => x.Peptide).Returns("K.S*R.A");

      PeakList<MatchedPeak> expPeaks = new PeakList<MatchedPeak>()
      {
        new MatchedPeak(964.96, 1,1),
        new MatchedPeak(981.99, 1,1),
        new MatchedPeak(982.97, 1,1)
      };
      expPeaks.PrecursorCharge = 1;
      expPeaks.PrecursorMZ = 1000.0;

      sr.Setup(x => x.ExperimentalPeakList).Returns(expPeaks);

      matcher.Match(sr.Object);

      Assert.AreEqual("[MH-H2O-NH3]", expPeaks[0].DisplayName);
      Assert.AreEqual("[MH-H2O]", expPeaks[1].DisplayName);
      Assert.AreEqual("[MH-NH3]", expPeaks[2].DisplayName);
    }
  }
}
