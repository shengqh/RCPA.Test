using NUnit.Framework;
using System.Linq;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotModification
  {
    [Test]
    public void TestModificationItem()
    {
      var strings = new string[]{
        "delta1=42.010559,Acetyl (Protein N-term)",
        "NeutralLoss1=0.000000",
        "delta2=8.014206,Lysine-13C615N2 (K-full) (K)",
        "NeutralLoss2=0.000000",
        "delta3=15.994919,Oxidation (M)",
        "NeutralLoss3=63.998285",
        "NeutralLoss3_master=0.000000",
        "delta4=79.966324,Phospho (STY)",
        "NeutralLoss4=0.000000",
        "NeutralLoss4_master=97.976898",
        "FixedMod1=57.021465,Carbamidomethyl (C)",
        "FixedModResidues1=C"};
      var dic = strings.ToDictionary(m => m.Substring(0, m.IndexOf('=')), m => m.Substring(m.IndexOf('=') + 1));
      var mm = new MascotModification();
      mm.Parse(dic);

      Assert.AreEqual(4, mm.DynamicModification.Count);
      Assert.AreEqual("42.010559 Acetyl (Protein N-term)", mm.DynamicModification[0].ToString());
      Assert.AreEqual("8.014206 Lysine-13C615N2 (K-full) (K)", mm.DynamicModification[1].ToString());
      Assert.AreEqual("15.994919 Oxidation (M)", mm.DynamicModification[2].ToString());
      Assert.AreEqual("79.966324 Phospho (STY)", mm.DynamicModification[3].ToString());

      Assert.AreEqual(1, mm.StaticModification.Count);
      Assert.AreEqual("57.021465 Carbamidomethyl (C)", mm.StaticModification[0].ToString());
    }
  }
}