using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotGenericFormatShiftPrecursorProcessor
  {
    [Test]
    public void TestUpdatePrecursor()
    {
      var pkl = new PeakList<Peak>();
      pkl.Precursor = new PrecursorPeak(new Peak(1000.0, 1.0, 4));
      pkl.FirstScan = 620;
      pkl.Annotations["TITLE"] = "TCGA-AA-A01F-01A-23_W_VU_20120727_A0218_1B_R_FR04.620.620.4.dta";
      var titleFormat = TitleParserUtils.FindByName("DTA");
      MascotGenericFormatShiftPrecursorProcessor.UpdatePrecursor(titleFormat, pkl, -10.0, 10000);
      Assert.AreEqual(10620, pkl.FirstScan);
      Assert.AreEqual("TCGA-AA-A01F-01A-23_W_VU_20120727_A0218_1B_R_FR04.10620.10620.4.dta", pkl.Annotations["TITLE"]);
      Assert.AreEqual(997.5, pkl.PrecursorMZ, 0.1);
    }
  }
}
