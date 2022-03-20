using NUnit.Framework;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Lipid
{
  [TestFixture]
  public class TestQueryItemListFormat
  {
    [Test]
    public void Test()
    {
      List<QueryItem> items = new QueryItemListFormat().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/LipidProductIon.txt");
      Assert.AreEqual(2, items.Count);
      Assert.AreEqual(185.1029, items[0].ProductIonMz, 0.0001);
      Assert.AreEqual(0.3, items[0].MinRelativeIntensity, 0.1);
      Assert.AreEqual(157.3049, items[1].ProductIonMz, 0.0001);
      Assert.AreEqual(0.5, items[1].MinRelativeIntensity, 0.1);
    }
  }
}
