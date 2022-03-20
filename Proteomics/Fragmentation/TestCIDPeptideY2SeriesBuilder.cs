using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  [TestFixture]
  public class TestCIDPeptideY2SeriesBuilder : AbstractTestFragmentationBuilder
  {
    [Test]
    public void TestBuild()
    {
      var aas = new Aminoacids();
      aas['*'].ResetMass(79.9799, 79.9799);
      aas['C'].ResetMass(160.1652, 160.1652);

      var builder = new CIDPeptideY2SeriesBuilder<IonTypePeak>()
      {
        CurAminoacids = aas
      };


      List<IonTypePeak> pkl = builder.Build("APYMEEQLQLLMCKYPEMT*LEDK");

      //Output(pkl, IonType.Y2);

      Assert.AreEqual(22, pkl.Count);
      AssertPeak(pkl[0], IonType.Y2, 1, 74.0606);
      AssertPeak(pkl[1], IonType.Y2, 2, 131.5741);
      AssertPeak(pkl[2], IonType.Y2, 3, 196.0954);
      AssertPeak(pkl[3], IonType.Y2, 4, 252.6374);
      AssertPeak(pkl[4], IonType.Y2, 5, 343.1512);
      AssertPeak(pkl[5], IonType.Y2, 6, 408.6714);
      AssertPeak(pkl[6], IonType.Y2, 7, 473.1927);
      AssertPeak(pkl[7], IonType.Y2, 8, 521.7191);
      AssertPeak(pkl[8], IonType.Y2, 9, 603.2508);
      AssertPeak(pkl[9], IonType.Y2, 10, 667.2982);
      AssertPeak(pkl[10], IonType.Y2, 11, 747.3808);
      AssertPeak(pkl[11], IonType.Y2, 12, 812.9011);
      AssertPeak(pkl[12], IonType.Y2, 13, 869.4431);
      AssertPeak(pkl[13], IonType.Y2, 14, 925.9851);
      AssertPeak(pkl[14], IonType.Y2, 15, 990.0144);
      AssertPeak(pkl[15], IonType.Y2, 16, 1046.5565);
      AssertPeak(pkl[16], IonType.Y2, 17, 1110.5858);
      AssertPeak(pkl[17], IonType.Y2, 18, 1175.1071);
      AssertPeak(pkl[18], IonType.Y2, 19, 1239.6284);
      AssertPeak(pkl[19], IonType.Y2, 20, 1305.1486);
      AssertPeak(pkl[20], IonType.Y2, 21, 1386.6803);
      AssertPeak(pkl[21], IonType.Y2, 22, 1435.2066);
    }
  }
}
