using NUnit.Framework;

namespace RCPA.Proteomics.MSGF
{
  [TestFixture]
  public class TestMSGFMzIdentParser
  {
    [Test]
    public void Test()
    {
      var parser = new MSGFMzIdentParser();
      var peptides = parser.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/msgf.mzid");
      //var peptides = parser.ReadFromFile(@"H:\shengquanhu\projects\20161013_shifted_precursor_10ppm\Fusion_HCDIT_Yeast_MSGF\result\10sep2013_yeast_control_1.msgf.mzid");
      Assert.AreEqual(4, peptides.Count);
      Assert.AreEqual(-9.0, peptides[0].Score);
      Assert.AreEqual(19.0, peptides[0].SpScore);
      Assert.AreEqual(1.1130518E-6, peptides[0].ExpectValue);
      Assert.AreEqual(11.768287, peptides[0].Probability);
      Assert.AreEqual(1, peptides[0].IsotopeError);
      Assert.AreEqual(302.09814, peptides[0].MatchedTIC);
    }
  }
}
