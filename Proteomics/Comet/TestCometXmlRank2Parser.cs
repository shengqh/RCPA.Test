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
      }.ReadFromFile(@"../../../data/comet.pep.xml");

      Assert.AreEqual(2, spectra.Count);
      Assert.AreEqual("K.M*REGPAK.N", spectra[0].Sequence);
      Assert.AreEqual(3, spectra[0].Rank);
      Assert.AreEqual("R.HFLEMK.S", spectra[1].Sequence);
      Assert.AreEqual(2, spectra[1].Rank);
    }
  }
}
