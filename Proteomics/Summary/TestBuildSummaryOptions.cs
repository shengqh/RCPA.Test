using NUnit.Framework;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestBuildSummaryOptions
  {
    [Test]
    public void Test()
    {
      var option = new BuildSummaryOptions();
      option.LoadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data//BuildSummary.param");
    }
  }
}
