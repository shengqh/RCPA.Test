using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Processor
{
  [TestFixture]
  public class TestPeakListMinIonIntensityProcessor
  {
    [Test]
    public void TestProcess()
    {
      var peaks = new PeakList<Peak>();
      peaks.Add(new Peak(1.0, 10.0));
      peaks.Add(new Peak(2.0, 100.0));

      var processor1 = new PeakListMinIonIntensityProcessor<Peak>(5.0);
      Assert.AreSame(peaks, processor1.Process(peaks));
      Assert.AreEqual(2, peaks.Count);

      var processor2 = new PeakListMinIonIntensityProcessor<Peak>(15.0);
      Assert.AreSame(peaks, processor2.Process(peaks));
      Assert.AreEqual(1, peaks.Count);

      var processor3 = new PeakListMinIonIntensityProcessor<Peak>(150.0);
      Assert.AreSame(null, processor3.Process(peaks));
    }
  }
}