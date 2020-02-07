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
  public class TestBYPhosphoNeutralLossMatcher : AbstractTestMatcher
  {
    private BYPhosphoNeutralLossMatcher matcher;

    [OneTimeSetUp]
    public void SetUp()
    {
      Dictionary<char, double> dic = new Dictionary<char, double>();
      dic['*'] = 80;
      dic['#'] = 57;

      matcher = new BYPhosphoNeutralLossMatcher(dic, 0.5, 0.1);
    }

    [Test]
    public void TestGetPhosphoModificationPositionMapB()
    {
      Dictionary<int, string> map = matcher.GetPhosphoModificationPositionMapB("K.AS*FT*RY*DY#R.F");

      Assert.AreEqual(9, map.Count);

      Assert.AreEqual("", map[0]);
      Assert.AreEqual("S", map[1]);
      Assert.AreEqual("S", map[2]);
      Assert.AreEqual("ST", map[3]);
      Assert.AreEqual("ST", map[4]);
      Assert.AreEqual("STY", map[5]);
      Assert.AreEqual("STY", map[6]);
      Assert.AreEqual("STY", map[7]);
      Assert.AreEqual("STY", map[8]);
    }

    [Test]
    public void TestGetPhosphoModificationPositionMapY()
    {
      Dictionary<int, string> map = matcher.GetPhosphoModificationPositionMapY("K.AS*FT*RY*DY#R.F");

      Assert.AreEqual(9, map.Count);

      Assert.AreEqual("", map[0]);
      Assert.AreEqual("", map[1]);
      Assert.AreEqual("", map[2]);
      Assert.AreEqual("Y", map[3]);
      Assert.AreEqual("Y", map[4]);
      Assert.AreEqual("YT", map[5]);
      Assert.AreEqual("YT", map[6]);
      Assert.AreEqual("YTS", map[7]);
      Assert.AreEqual("YTS", map[8]);
    }

    [Test]
    public void TestGetNeutralLossPeaks()
    {
      List<MatchedPeak> theoreticalPeaks = new List<MatchedPeak>(){
        new MatchedPeak(300, 1,1){PeakIndex = 1,PeakType = IonType.B},
        new MatchedPeak(400, 1,1){PeakIndex = 2,PeakType = IonType.B},
        new MatchedPeak(500, 1,1){PeakIndex = 3,PeakType = IonType.B}};

      Dictionary<int, string> phosphoAminoacidsMap = new Dictionary<int, string>();
      phosphoAminoacidsMap[0] = "";
      phosphoAminoacidsMap[1] = "S";
      phosphoAminoacidsMap[2] = "SY";

      List<MatchedPeak> actual = matcher.GetPhosphoNeutralLossPeaks(theoreticalPeaks, IonType.B2, 1, phosphoAminoacidsMap, new[] { false, true, false }, new[] { false, false, false });

      actual.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      Assert.AreEqual("[b2-H3PO4-H2O]", actual[0].DisplayName);
      Assert.AreEqual(theoreticalPeaks[1].Mz - NeutralLossConstants.NL_H3PO4.Mass - NeutralLossConstants.NL_WATER.Mass, actual[0].Mz, 0.01);

      Assert.AreEqual("[b2-H3PO4]", actual[1].DisplayName);
      Assert.AreEqual("[b3-H3PO4-HPO3]", actual[2].DisplayName);
      Assert.AreEqual("[b3-H3PO4]", actual[3].DisplayName);
      Assert.AreEqual("[b3-HPO3]", actual[4].DisplayName);
    }

    [Test]
    public void TestMatch()
    {
      var factory = new MockRepository(MockBehavior.Strict);

      PeakList<MatchedPeak> expPeaks = new PeakList<MatchedPeak>()
      {
        new MatchedPeak(102.0, 1,1),
        new MatchedPeak(220, 1,1),
        new MatchedPeak(1003, 1,1)
      };
      expPeaks.PrecursorCharge = 1;
      expPeaks.PrecursorMZ = 1000.0;

      var dic = new Dictionary<IonType, List<MatchedPeak>>();
      dic[IonType.B] = new List<MatchedPeak>();
      dic[IonType.B].Add(new MatchedPeak(200, 0.0, 1) { PeakIndex = 1, PeakType = IonType.B });
      dic[IonType.B].Add(new MatchedPeak(300, 0.0, 1) { PeakIndex = 2, PeakType = IonType.B });
      dic[IonType.Y] = new List<MatchedPeak>();
      dic[IonType.Y].Add(new MatchedPeak(1000, 0.0, 1) { PeakIndex = 1, PeakType = IonType.Y });
      dic[IonType.Y].Add(new MatchedPeak(1100, 0.0, 1) { PeakIndex = 2, PeakType = IonType.Y });

      var sr = factory.Create<IIdentifiedPeptideResult>();
      sr.Setup(x => x.GetIonSeries()).Returns(dic);
      sr.Setup(x => x.Peptide).Returns("K.S*Y*R.F");
      sr.Setup(x => x.ExperimentalPeakList).Returns(expPeaks);

      matcher.Match(sr.Object);

      //expPeaks.ForEach(p => Console.WriteLine(p.DisplayName + "---" + p.Mz));

      PeakList<MatchedPeak> annotatedPeaks = sr.Object.ExperimentalPeakList;
      AssertPeak(annotatedPeaks[0], "[b1-H3PO4]", 102);
      AssertPeak(annotatedPeaks[1], "[b2-HPO3]", 220);
      AssertPeak(annotatedPeaks[2], "[y2-HPO3-NH3]", 1003);
    }
  }
}
