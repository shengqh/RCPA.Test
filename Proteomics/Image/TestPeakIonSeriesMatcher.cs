using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestPeakIonSeriesMatcher
  {
    [Test]
    public void TestMatch()
    {
      PeakIonSeriesMatcher matcher = new PeakIonSeriesMatcher(IonType.B, 0.5);

      MockFactory factory = new MockFactory(MockBehavior.Strict);

      var dic = new Dictionary<IonType, List<MatchedPeak>>();
      dic[IonType.B] = new List<MatchedPeak>();
      dic[IonType.B].Add(new MatchedPeak(100, 0.0, 1) { PeakType = IonType.B, PeakIndex = 1 });
      dic[IonType.B].Add(new MatchedPeak(200, 0.0, 1) { PeakType = IonType.B, PeakIndex = 2 });

      PeakList<MatchedPeak> expPeaks = new PeakList<MatchedPeak>()
      {
        new MatchedPeak(100.03, 1,1),
        new MatchedPeak(200.02, 1,1),
        new MatchedPeak(300, 1,1)
      };
      expPeaks.PrecursorCharge = 1;

      var sr = factory.Create<IIdentifiedPeptideResult>();
      sr.Setup(x => x.Peptide).Returns("K.S*R.F");
      sr.Setup(x => x.GetIonSeries()).Returns(dic);
      sr.Setup(x => x.ExperimentalPeakList).Returns(expPeaks);

      matcher.Match(sr.Object);

      Assert.AreEqual(null, expPeaks[0].DisplayName);
      Assert.AreEqual(IonType.B, expPeaks[0].PeakType);
      Assert.AreEqual(1, expPeaks[0].PeakIndex);

      Assert.AreEqual(null, expPeaks[1].DisplayName);
      Assert.AreEqual(IonType.B, expPeaks[1].PeakType);
      Assert.AreEqual(2, expPeaks[1].PeakIndex);

      Assert.AreEqual(null, expPeaks[2].DisplayName);
      Assert.AreEqual(IonType.UNKNOWN, expPeaks[2].PeakType);
      Assert.AreEqual(0, expPeaks[2].PeakIndex);
    }
  }
}
