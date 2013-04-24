using System.Collections.Generic;
using NUnit.Framework;

namespace RCPA.Proteomics.Isotopic
{
  [TestFixture]
  public class TestCarbonProfileBuilder
  {
    private readonly AtomComposition ac = new AtomComposition("C112");
    private readonly CarbonProfileBuilder cp1 = new CarbonProfileBuilder();

    [Test]
    public void TestGetProfile()
    {
      List<double> profile1 = this.cp1.GetProfile(this.ac, 8);

      var cp2 = new CHONSIsotopicProfileBuilder();
      List<double> profile2 = cp2.GetProfile(this.ac, 8);

      for (int i = 0; i < 8; i++)
      {
        Assert.AreEqual(profile2[i], profile1[i], 0.0001);
      }
    }

    [Test]
    public void TestGetProfile2()
    {
      List<double> result = this.cp1.GetProfile(this.ac);
      Assert.Less(AbstractIsotopicProfileBuilder.PERCENT_TOLERANCE, result[result.Count - 2]);
      Assert.Less(result[result.Count - 1], AbstractIsotopicProfileBuilder.PERCENT_TOLERANCE);
    }
  }
}