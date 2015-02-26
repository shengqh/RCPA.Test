using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace RCPA.Proteomics.Modification
{
  [TestFixture]
  public class TestModificationUtils
  {
    [Test]
    public void TestParseFromOutFileLine()
    {
      Dictionary<char, double> map =
        ModificationUtils.ParseFromOutFileLine(
          "(STY* +79.96633) (M# +15.99492) (ST@ -18.00000) C=160.16523  Enzyme:Trypsin(KR) (1)");
      Assert.AreEqual(4, map.Count);
      Assert.AreEqual(79.96633, map['*'], 0.00001);
      Assert.AreEqual(15.99492, map['#'], 0.00001);
      Assert.AreEqual(-18.00000, map['@'], 0.00001);
      Assert.AreEqual(160.16523, map['C'], 0.00001);
    }

    [Test]
    public void TestIsModification()
    {
      Assert.IsTrue(ModificationUtils.IsModification('*'));
      Assert.IsTrue(ModificationUtils.IsModification('p'));
      Assert.IsFalse(ModificationUtils.IsModification('A'));
    }

    [Test]
    public void TestGetModifiedAminiacids()
    {
      var actual = ModificationUtils.GetModifiedAminiacids("^SEETAY*FVWLGK@");
      Assert.AreEqual(2, actual.Count);
      Assert.AreEqual('Y', actual[6]);
      Assert.AreEqual('K', actual[12]);
    }

    [Test]
    public void TestParseProbability()
    {
      var probs = ModificationUtils.ParseProbability("S(1): 0.0; S(3): 0.4; S(4): 99.6");
      Assert.AreEqual(3, probs.Count);
      Assert.IsTrue(probs.All(l => l.Aminoacid.Equals("S")));
      Assert.AreEqual(4, probs[2].Site);
      Assert.AreEqual(99.6, probs[2].Probability);
    }

    [Test]
    public void TestFilterSiteProbability()
    {
      var actual = ModificationUtils.FilterSiteProbability("SQSS*PR", "S(1): 0.0; S(3): 0.4; S(4): 99.6");
      Assert.AreEqual("S(4): 99.6", actual);

      actual = ModificationUtils.FilterSiteProbability("^SQSS*PR", "S(1): 0.0; S(3): 0.4; S(4): 99.6");
      Assert.AreEqual("S(4): 99.6", actual);

      actual = ModificationUtils.FilterSiteProbability("^SRK@T*SSVSSSPST*PT*QVT*K@", "Too many isoforms");
      Assert.AreEqual("K(3): 0; T(4): 0; T(13): 0; T(15): 0; T(18): 0; K(19): 0", actual);
    }
  }
}