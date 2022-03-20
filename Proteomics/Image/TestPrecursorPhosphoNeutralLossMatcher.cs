using Moq;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestPrecursorPhosphoNeutralLossMatcher : AbstractTestMatcher
  {
    private PrecursorPhosphoNeutralLossMatcher matcher;

    [OneTimeSetUp]
    public void SetUp()
    {
      Dictionary<char, double> dic = new Dictionary<char, double>();
      dic['*'] = 80;
      dic['#'] = 57;

      matcher = new PrecursorPhosphoNeutralLossMatcher(dic, 0.3);
    }

    [Test]
    public void TestIsPhospho()
    {
      Assert.IsTrue(matcher.IsPhospho("AFT*R", 2));
      Assert.IsFalse(matcher.IsPhospho("AFT#R", 2));
    }

    [Test]
    public void TestGetPhosphoModificationString()
    {
      Assert.AreEqual("STY", matcher.GetPhosphoModificationString("K.AS*FT*RY*DY#R.F"));
    }

    [Test]
    public void TestGetPhosphoNeutralLossCandidates()
    {
      List<INeutralLossType> nls = matcher.GetPhosphoNeutralLossCandidates("STY");
      Assert.AreEqual(3, nls.Count);
      Assert.AreEqual(NeutralLossConstants.NL_H3PO4, nls[0]);
      Assert.AreEqual(NeutralLossConstants.NL_H3PO4, nls[1]);
      Assert.AreEqual(NeutralLossConstants.NL_HPO3, nls[2]);
    }

    [Test]
    public void TestGetPrecursorNeutralLossPeaks()
    {
      List<INeutralLossType> nls = new INeutralLossType[] { NeutralLossConstants.NL_H3PO4 }.ToList();

      MatchedPeak precursor = new MatchedPeak(1000, 0, 2);
      List<MatchedPeak> pkls = matcher.GetNeutralLossPeaks(IonType.PRECURSOR_NEUTRAL_LOSS_PHOSPHO, precursor, nls, (m => m.IsPhosphoNeutralLossType()));
      Assert.AreEqual("[MH2-H3PO4]", pkls[0].DisplayName);
      Assert.AreEqual(1000 - NeutralLossConstants.NL_H3PO4.Mass / 2, pkls[0].Mz, 0.01);

      precursor.Charge = 1;
      pkls = matcher.GetNeutralLossPeaks(IonType.PRECURSOR_NEUTRAL_LOSS_PHOSPHO, precursor, nls, (m => m.IsPhosphoNeutralLossType()));
      Assert.AreEqual("[MH-H3PO4]", pkls[0].DisplayName);
      Assert.AreEqual(1000 - NeutralLossConstants.NL_H3PO4.Mass, pkls[0].Mz, 0.01);
    }

    [Test]
    public void TestMatch()
    {
      var factory = new MockRepository(MockBehavior.Strict);

      PeakList<MatchedPeak> expPeaks = new PeakList<MatchedPeak>()
      {
        new MatchedPeak(902.02, 1,1),
        new MatchedPeak(920.03, 1,1),
        new MatchedPeak(822.06, 1,1),
        new MatchedPeak(884.01, 1,1),
        new MatchedPeak(885, 1,1),
        new MatchedPeak(903, 1,1)
      };
      expPeaks.PrecursorCharge = 1;
      expPeaks.PrecursorMZ = 1000.0;

      var sr = factory.Create<IIdentifiedPeptideResult>();
      sr.Setup(x => x.Peptide).Returns("K.S*Y*R.F");
      sr.Setup(x => x.ExperimentalPeakList).Returns(expPeaks);

      matcher.Match(sr.Object);

      AssertPeak(expPeaks[0], "[MH-H3PO4]", 902.023104375);
      AssertPeak(expPeaks[1], "[MH-HPO3]", 920.033669075);
      AssertPeak(expPeaks[2], "[MH-H3PO4-HPO3]", 822.05677345);
      AssertPeak(expPeaks[3], "[MH-H3PO4-H2O]", 884.012539675);
      AssertPeak(expPeaks[4], "[MH-H3PO4-NH3]", 884.99655527);
      AssertPeak(expPeaks[5], "[MH-HPO3-NH3]", 903.00711997);
    }
  }
}
