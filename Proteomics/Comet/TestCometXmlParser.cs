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
  public class TestCometXmlParser
  {
    [Test]
    public void Test()
    {
      var spectra = new CometXmlParser()
      {
        TitleParser = TitleParserUtils.FindByName("DTA")
      }.ReadFromFile(@"../../../data/comet.pep.xml");

      Assert.AreEqual(2, spectra.Count);
      Assert.AreEqual("R.HFM*LEMK.S", spectra[0].Sequence);
      Assert.AreEqual(1, spectra[0].Rank);
      Assert.AreEqual(0.778, spectra[0].Score, 0.001);
      Assert.AreEqual(1.91E+01, spectra[0].ExpectValue, 0.01);
      Assert.AreEqual(1077, spectra[0].Query.MatchCount);
      Assert.AreEqual("Elite_CIDIT_Human.minus10dalton.2.2.2.dta", spectra[0].Query.FileScan.LongFileName);
    }
  }
}
