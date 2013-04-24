using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  [TestFixture]
  public class TestCIDPeptideBSeriesBuilder : AbstractTestFragmentationBuilder
  {
    [Test]
    public void TestBuild()
    {
      var aas = new Aminoacids();
      aas['*'].ResetMass(79.9799, 79.9799);
      aas['C'].ResetMass(160.1652, 160.1652);

      var builder = new CIDPeptideBSeriesBuilder<IonTypePeak>()
      {
        CurAminoacids = aas
      };

      List<IonTypePeak> pkl = builder.Build("R.CGETVES*GDEKDLAK.A");

      //Output(pkl, IonType.B);

      Assert.AreEqual(14, pkl.Count);

      AssertPeak(pkl[0], IonType.B, 1, 161.1730);
      AssertPeak(pkl[1], IonType.B, 2, 218.1945);
      AssertPeak(pkl[2], IonType.B, 3, 347.2371);
      AssertPeak(pkl[3], IonType.B, 4, 448.2848);
      AssertPeak(pkl[4], IonType.B, 5, 547.3532);
      AssertPeak(pkl[5], IonType.B, 6, 676.3958);
      AssertPeak(pkl[6], IonType.B, 7, 843.4077);
      AssertPeak(pkl[7], IonType.B, 8, 900.4292);
      AssertPeak(pkl[8], IonType.B, 9, 1015.4561);
      AssertPeak(pkl[9], IonType.B, 10, 1144.4987);
      AssertPeak(pkl[10], IonType.B, 11, 1272.5937);
      AssertPeak(pkl[11], IonType.B, 12, 1387.6206);
      AssertPeak(pkl[12], IonType.B, 13, 1500.7047);
      AssertPeak(pkl[13], IonType.B, 14, 1571.7418);
    }
  }
}
