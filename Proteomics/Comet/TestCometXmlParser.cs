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
      }.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/comet.pep.xml");

      Assert.AreEqual(2, spectra.Count);
      Assert.AreEqual("R.HFM*LEMK.S", spectra[0].Sequence);
      Assert.AreEqual(1, spectra[0].Rank);
      Assert.AreEqual(0.778, spectra[0].Score, 0.001);
      Assert.AreEqual(0.001, spectra[0].DeltaScore, 0.001);
      Assert.AreEqual(80.2, spectra[0].SpScore, 0.01);
      Assert.AreEqual(7, spectra[0].SpRank);
      Assert.AreEqual(1.91E+01, spectra[0].ExpectValue, 0.01);
      Assert.AreEqual("sp|Q9BV73|CP250_HUMAN", spectra[0].Proteins[0]);
      Assert.AreEqual(4, spectra[0].MatchedIonCount);
      Assert.AreEqual(10, spectra[0].TheoreticalIonCount);
      Assert.AreEqual(803.399995, spectra[0].TheoreticalMass, 0.000001);
      Assert.AreEqual(2, spectra[0].Query.FileScan.Charge);

      Assert.AreEqual(1077, spectra[0].Query.MatchCount);
      Assert.AreEqual("Elite_CIDIT_Human.minus10dalton.2.2.2.dta", spectra[0].Query.FileScan.LongFileName);
    }
  }
}
