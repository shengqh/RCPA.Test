using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;
using RCPA.Seq;

namespace RCPA.Proteomics.XTandem
{
  [TestFixture]
  public class TestXTandemSpectrumXmlParser
  {
    [Test]
    public void TestParsePeptides()
    {
      List<IIdentifiedSpectrum> spectra = new XTandemSpectrumXmlParser(AccessNumberParserFactory.FindOrCreateParser(@"^>{0,1}(\S+)", "SwissProt")).ParsePeptides(@"..\..\data\xtandem.xml");
      Assert.AreEqual(2, spectra.Count);
      Assert.AreEqual("K.DLGEEHFK.G", spectra[0].Sequence);
      Assert.AreEqual(32.2, spectra[0].Score, 0.1);
      Assert.AreEqual(5.1e-003, spectra[0].ExpectValue, 0.001);
      Assert.AreEqual(974.458, spectra[0].TheoreticalMH, 0.001);
      Assert.AreEqual(975.773, spectra[0].ExperimentalMH, 0.001);
      Assert.AreEqual(2, spectra[0].Query.Charge);

      Assert.AreEqual("K.DDDEEHFK.G", spectra[1].Sequence);
    }

    [Test]
    public void TestGetSourceFile()
    {
      Assert.AreEqual(@"D:\ws\ws_2\WS_2.RAW.mgf", XTandemSpectrumXmlParser.GetSourceFile(@"..\..\data\xtandem.xml"));
      Assert.AreEqual(@"/people/xiaobinxin/AS_II/raw_data/raw.3T3L1.SCX/mzXML.3T3L1/3T3L1_MDI_0min_SAX_Online_081215_01.RAW.mzXml", XTandemSpectrumXmlParser.GetSourceFile(@"..\..\data\xtandem_linux.xml"));
    }
  }
}
