using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Comet
{
  [TestFixture]
  public class TestCometXmlRank2Parser
  {
    [Test]
    public void Test()
    {
      var spectra = new CometXmlRank2Parser()
      {
        TitleParser = TitleParserUtils.FindByName("DTA")
      }.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/comet.pep.xml");

      Assert.AreEqual(2, spectra.Count);
      Assert.AreEqual("K.M*REGPAK.N", spectra[0].Sequence);
      Assert.AreEqual(3, spectra[0].Rank);
      Assert.AreEqual(0.743, spectra[0].Score, 0.001);
      Assert.AreEqual(0.035, spectra[0].DeltaScore, 0.001);
      Assert.AreEqual(66.0, spectra[0].SpScore, 0.01);
      Assert.AreEqual(13, spectra[0].SpRank);
      Assert.AreEqual(2.47E+01, spectra[0].ExpectValue, 0.01);
      Assert.AreEqual(1077, spectra[0].Query.MatchCount);
      Assert.AreEqual("Elite_CIDIT_Human.minus10dalton.2.2.2.dta", spectra[0].Query.FileScan.LongFileName);
    }
  }
}
