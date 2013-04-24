using System.Collections.Generic;
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
  }
}