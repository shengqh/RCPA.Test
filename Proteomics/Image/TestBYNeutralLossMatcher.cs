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
  public class TestBYNeutralLossMatcher : AbstractTestMatcher
  {
    private BYNeutralLossMatcher matcher;
    private Dictionary<IonType, List<MatchedPeak>> dic;

    [OneTimeSetUp]
    public void TestFixtureSetUp()
    {
      matcher = new BYNeutralLossMatcher(0.5, 0.0);

      dic = new Dictionary<IonType, List<MatchedPeak>>();
      dic[IonType.B] = new List<MatchedPeak>();
      dic[IonType.B].Add(new MatchedPeak(100, 0.0, 1) { PeakIndex = 1,PeakType = IonType.B });
      dic[IonType.B].Add(new MatchedPeak(200, 0.0, 1) { PeakIndex = 2, PeakType = IonType.B });
      dic[IonType.Y] = new List<MatchedPeak>();
      dic[IonType.Y].Add(new MatchedPeak(1000, 0.0, 1) { PeakIndex = 1, PeakType = IonType.Y });
      dic[IonType.Y].Add(new MatchedPeak(1100, 0.0, 1) { PeakIndex = 2, PeakType = IonType.Y });
    }

    [Test]
    public void TestGetNeutralLossPeaks()
    {
      var factory = new MockRepository(MockBehavior.Strict);

      var nlc = factory.Create<INeutralLossCandidates>();
      nlc.Setup(x => x.BLossWater).Returns(new[] { true, true });
      nlc.Setup(x => x.BLossAmmonia).Returns(new[] { false, true });//b1不脱氨
      nlc.Setup(x => x.YLossWater).Returns(new[] { false, true });//y1不脱水
      nlc.Setup(x => x.YLossAmmonia).Returns(new[] { true, true });


      List<MatchedPeak> actual = matcher.GetNeutralLossPeaks(dic, 1, nlc.Object);

      actual.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      //int index = 0;
      //actual.ForEach(m => Console.WriteLine(MyConvert.Format("AssertPeak(actual[{2}], \"{0}\",{1:0.00});", m.DisplayName, m.Mz, index++)));

      AssertPeak(actual[0], "[b1-H2O]", 81.99);
      AssertPeak(actual[1], "[b2-H2O-NH3]", 164.96);
      AssertPeak(actual[2], "[b2-H2O]", 181.99);
      AssertPeak(actual[3], "[b2-NH3]", 182.97);
      AssertPeak(actual[4], "[y1-NH3]", 982.97);
      AssertPeak(actual[5], "[y2-H2O-NH3]", 1064.96);
      AssertPeak(actual[6], "[y2-H2O]", 1081.99);
      AssertPeak(actual[7], "[y2-NH3]", 1082.97);
    }

    [Test]
    public void TestMatch()
    {
      var factory = new MockRepository(MockBehavior.Strict);

      PeakList<MatchedPeak> expPeaks = new PeakList<MatchedPeak>()
      {
        new MatchedPeak(165.00, 1,1),
        new MatchedPeak(182.40, 1,1),
        new MatchedPeak(982.5, 1,1)
      };
      expPeaks.PrecursorCharge = 1;

      var sr = factory.Create<IIdentifiedPeptideResult>();
      sr.Setup(x => x.GetIonSeries()).Returns(dic);
      sr.Setup(x => x.Peptide).Returns("K.S*R.F");
      sr.Setup(x => x.ExperimentalPeakList).Returns(expPeaks);

      matcher.Match(sr.Object);

      PeakList<MatchedPeak> annotatedPeaks = sr.Object.ExperimentalPeakList;
      AssertPeak(annotatedPeaks[0], "[b2-H2O-NH3]", 165);
      AssertPeak(annotatedPeaks[1], "[b2-H2O]", 182.40);
      AssertPeak(annotatedPeaks[2], "[y1-NH3]", 982.5);
    }
  }
}
