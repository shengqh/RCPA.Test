using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Processor
{
  [TestFixture]
  public class TestPeakListMinTotalIonIntensityProcessor
  {
    [Test]
    public void TestProcess()
    {
      var peaks = new PeakList<Peak>();
      peaks.Add(new Peak(1.0, 10.0));
      peaks.Add(new Peak(2.0, 100.0));

      var processor1 = new PeakListMinTotalIonIntensityProcessor<Peak>(50.0);
      Assert.AreSame(peaks, processor1.Process(peaks));

      var processor2 = new PeakListMinIonIntensityProcessor<Peak>(150.0);
      Assert.AreSame(null, processor2.Process(peaks));
    }
  }
}