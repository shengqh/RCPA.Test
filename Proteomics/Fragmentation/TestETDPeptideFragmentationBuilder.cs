using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  [TestFixture]
  public class TestETDPeptideFragmentationBuilder : AbstractTestFragmentationBuilder
  {
    [Test]
    public void TestBuild()
    {
      var aas = new Aminoacids();

      var builder = new ETDPeptideCSeriesBuilder<IonTypePeak>(2000, 3)
      {
        CurAminoacids = aas
      };

      aas['*'].ResetMass(1217.117, 1217.117);
      List<IonTypePeak> pkl = builder.Build("SRN*LTK");

      AssertPeak(pkl[0], IonType.C, 1, 105.0664);
      AssertPeak(pkl[1], IonType.C, 2, 261.1675);
      AssertPeak(pkl[2], IonType.C, 3, 1592.3274);
      AssertPeak(pkl[3], IonType.C, 4, 1705.4115);
      AssertPeak(pkl[4], IonType.C, 5, 1806.4592);

      //pkl.ForEach(p => Console.Out.WriteLine(MyConvert.Format("{0}{1}\t{2:0.0000}", p.PeakType, p.PeakIndex, p.Mz)));
    }
  }
}