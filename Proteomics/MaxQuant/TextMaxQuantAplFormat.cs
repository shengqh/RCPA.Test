using NUnit.Framework;
using RCPA.Proteomics.Summary;
using System.Linq;

namespace RCPA.Proteomics.MaxQuant
{
  [TestFixture]
  public class TextMaxQuantAplFormat
  {
    [Test]
    public void Test()
    {
      var parser = TitleParserUtils.GetTitleParsers().Find(m => m.FormatName == "MaxQuant");
      var format = new MaxQuantAplFormat(parser);
      var pkls = format.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/maxquant.peak.apl");

      Assert.AreEqual(562, pkls.Count);
      Assert.AreEqual("20110915_iTRAQ_4plex_GK_6ug_Exp_1", pkls[0].Experimental);
      Assert.AreEqual(10258, pkls[0].ScanTimes[0].Scan);
      Assert.AreEqual("HCD", pkls[0].ScanMode);
      Assert.AreEqual(301.68467, pkls[0].PrecursorMZ, 0.00001);
      Assert.AreEqual(2, pkls[0].PrecursorCharge);

      Assert.AreEqual(37, pkls[0].Count);
      Assert.AreEqual(110.07127, pkls[0].First().Mz, 0.00001);
      Assert.AreEqual(2116.9580, pkls[0].First().Intensity, 0.0001);
      Assert.AreEqual(590.83301, pkls[0].Last().Mz, 0.00001);
      Assert.AreEqual(248.6751, pkls[0].Last().Intensity, 0.0001);
    }
  }
}
