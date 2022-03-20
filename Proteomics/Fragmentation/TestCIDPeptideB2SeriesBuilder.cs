using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Fragmentation
{
  [TestFixture]
  public class TestCIDPeptideB2SeriesBuilder : AbstractTestFragmentationBuilder
  {

    [Test]
    public void TestBuild()
    {
      var aas = new Aminoacids();
      aas['*'].ResetMass(79.9799, 79.9799);
      aas['C'].ResetMass(160.1652, 160.1652);

      var builder = new CIDPeptideB2SeriesBuilder<IonTypePeak>()
      {
        CurAminoacids = aas
      };

      List<IonTypePeak> pkl = builder.Build("APYMEEQLQLLMCKYPEMT*LEDK");

      //Output(pkl, IonType.B2);

      Assert.IsTrue(pkl.All(m => m.Charge == 2));

      Assert.AreEqual(22, pkl.Count);

      AssertPeak(pkl[0], IonType.B2, 1, 36.5264);
      AssertPeak(pkl[1], IonType.B2, 2, 85.0528);
      AssertPeak(pkl[2], IonType.B2, 3, 166.5844);
      AssertPeak(pkl[3], IonType.B2, 4, 232.1047);
      AssertPeak(pkl[4], IonType.B2, 5, 296.6260);
      AssertPeak(pkl[5], IonType.B2, 6, 361.1473);
      AssertPeak(pkl[6], IonType.B2, 7, 425.1765);
      AssertPeak(pkl[7], IonType.B2, 8, 481.7186);
      AssertPeak(pkl[8], IonType.B2, 9, 545.7479);
      AssertPeak(pkl[9], IonType.B2, 10, 602.2899);
      AssertPeak(pkl[10], IonType.B2, 11, 658.8319);
      AssertPeak(pkl[11], IonType.B2, 12, 724.3522);
      AssertPeak(pkl[12], IonType.B2, 13, 804.4348);
      AssertPeak(pkl[13], IonType.B2, 14, 868.4823);
      AssertPeak(pkl[14], IonType.B2, 15, 950.0139);
      AssertPeak(pkl[15], IonType.B2, 16, 998.5403);
      AssertPeak(pkl[16], IonType.B2, 17, 1063.0616);
      AssertPeak(pkl[17], IonType.B2, 18, 1128.5818);
      AssertPeak(pkl[18], IonType.B2, 19, 1219.0956);
      AssertPeak(pkl[19], IonType.B2, 20, 1275.6377);
      AssertPeak(pkl[20], IonType.B2, 21, 1340.1590);
      AssertPeak(pkl[21], IonType.B2, 22, 1397.6724);
    }
  }
}
