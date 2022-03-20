using NUnit.Framework;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestFalseDiscoveryRateOptions
  {
    [Test]
    public void Test()
    {
      FalseDiscoveryRateOptions option = new FalseDiscoveryRateOptions()
      {
        FilterByFdr = true,
        FdrLevel = FalseDiscoveryRateLevel.Peptide,
        FdrPeptideCount = 10,
        FdrType = FalseDiscoveryRateType.Total,
        FdrValue = 0.01,
        MaxPeptideFdr = 0.01
      };

      XElement root = new XElement("Root");
      option.Save(root);

      FalseDiscoveryRateOptions target = new FalseDiscoveryRateOptions();
      target.Load(root);

      Assert.AreEqual(option.FilterByFdr, target.FilterByFdr);
      Assert.AreEqual(option.FdrLevel, target.FdrLevel);
      Assert.AreEqual(option.FdrPeptideCount, target.FdrPeptideCount);
      Assert.AreEqual(option.FdrType, target.FdrType);
      Assert.AreEqual(option.FdrValue, target.FdrValue);
      Assert.AreEqual(option.MaxPeptideFdr, target.MaxPeptideFdr);
    }
  }

}
