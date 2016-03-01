using NUnit.Framework;

namespace RCPA.Proteomics.Isotopic
{
  [TestFixture]
  public class TestSilacIsotopeFile
  {
    [Test]
    public void TestReadFromFile()
    {
      ISilacIsotopeFile file = new MonoisotopicSilacIsotopeFile(@"../../../data/Leucine_isotope.ini");
      double sampleL = file.SampleCalculator.GetMass("L");
      double refL = file.ReferenceCalculator.GetMass("L");
      Assert.AreEqual(new Aminoacids().MonoPeptideMass("L"), sampleL, 0.001);
      Assert.AreEqual(6.0201, refL - sampleL, 0.0001);
    }
  }
}