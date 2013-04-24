using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Processor
{
  [TestFixture]
  public class TestPeakListMinIonCountProcessor
  {
    [Test]
    public void TestProcess()
    {
      var peaks = new PeakList<Peak>();
      peaks.Add(new Peak(1.0, 1.0));
      peaks.Add(new Peak(2.0, 1.0));

      var processor = new PeakListMinIonCountProcessor<Peak>(3);
      Assert.AreSame(null, processor.Process(peaks));

      peaks.Add(new Peak(3.0, 1.0));
      Assert.AreSame(peaks, processor.Process(peaks));
    }
  }
}