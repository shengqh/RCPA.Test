using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotPeptide
  {
    [Test]
    public void TestModifications()
    {
      var mphit = new IdentifiedSpectrum();
      Assert.AreEqual(new string[0], mphit.GetModifications());

      string expect = "Oxidation (M)";
      mphit.Modifications = expect;
      Assert.AreEqual(1, mphit.GetModifications().Length);
      Assert.AreEqual(expect, mphit.GetModifications()[0]);

      mphit.Modifications = "Oxidation (M); Label:18O(1) (C-term)";
      Assert.AreEqual(2, mphit.GetModifications().Length);
      Assert.AreEqual("Oxidation (M)", mphit.GetModifications()[0]);
      Assert.AreEqual("Label:18O(1) (C-term)", mphit.GetModifications()[1]);
    }
  }
}