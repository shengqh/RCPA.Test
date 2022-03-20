using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Processor
{
  [TestFixture]
  public class TestPeakListMassRangeProcessor
  {
    [Test]
    public void TestProcess()
    {
      var peaks = new PeakList<Peak>();
      peaks.PrecursorMZ = 1000;
      peaks.PrecursorCharge = 1;

      var processor = new PeakListMassRangeProcessor<Peak>(800, 1200, new[] { 2, 3 });
      Assert.AreSame(peaks, processor.Process(peaks));

      peaks.PrecursorCharge = 0;
      Assert.AreSame(null, processor.Process(peaks));

      peaks.PrecursorMZ = 500;
      Assert.AreSame(peaks, processor.Process(peaks));

      peaks.PrecursorMZ = 300;
      Assert.AreSame(peaks, processor.Process(peaks));

      var processor2 = new PeakListMassRangeProcessor<Peak>(800, 1200, new int[] { });
      Assert.AreSame(peaks, processor2.Process(peaks));
    }
  }
}