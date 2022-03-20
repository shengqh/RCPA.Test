using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  [TestFixture]
  public class TestCIDPeptideYSeriesBuilder : AbstractTestFragmentationBuilder
  {
    [Test]
    public void TestBuild()
    {
      var aas = new Aminoacids();
      aas['*'].ResetMass(79.9799, 79.9799);
      aas['C'].ResetMass(160.1652, 160.1652);

      var builder = new CIDPeptideYSeriesBuilder<IonTypePeak>()
      {
        CurAminoacids = aas
      };

      List<IonTypePeak> pkl = builder.Build("CGETVES*GDEKDLAK");

      //Output(pkl, IonType.Y);

      Assert.AreEqual(14, pkl.Count);
      AssertPeak(pkl[0], IonType.Y, 1, 147.1134);
      AssertPeak(pkl[1], IonType.Y, 2, 218.1505);
      AssertPeak(pkl[2], IonType.Y, 3, 331.2345);
      AssertPeak(pkl[3], IonType.Y, 4, 446.2615);
      AssertPeak(pkl[4], IonType.Y, 5, 574.3564);
      AssertPeak(pkl[5], IonType.Y, 6, 703.3990);
      AssertPeak(pkl[6], IonType.Y, 7, 818.4260);
      AssertPeak(pkl[7], IonType.Y, 8, 875.4474);
      AssertPeak(pkl[8], IonType.Y, 9, 1042.4594);
      AssertPeak(pkl[9], IonType.Y, 10, 1171.5020);
      AssertPeak(pkl[10], IonType.Y, 11, 1270.5704);
      AssertPeak(pkl[11], IonType.Y, 12, 1371.6181);
      AssertPeak(pkl[12], IonType.Y, 13, 1500.6606);
      AssertPeak(pkl[13], IonType.Y, 14, 1557.6821);
    }
  }
}
