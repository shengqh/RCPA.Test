using NUnit.Framework;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Processor
{
  [TestFixture]
  public class TestPeakListDeisotopicByChargeProcessor : PeakListDeisotopicByChargeProcessor<Peak>
  {
    public TestPeakListDeisotopicByChargeProcessor() : base(200) { }

    [Test]
    public void TestFindEnvelope()
    {
      var peaks = new PeakList<Peak>();
      peaks.Add(new Peak(301, 1, 1));
      peaks.Add(new Peak(302, 2, 1));
      peaks.Add(new Peak(303, 1, 1));
      peaks.Add(new Peak(304, 2, 1));

      var env = this.FindEnvelope(peaks, peaks[0], 0.1);
      Assert.AreEqual(3, env.Count);
      Assert.IsTrue(!env.Contains(peaks[3]));

      peaks.RemoveAt(0);
      env = this.FindEnvelope(peaks, peaks[0], 0.1);
      Assert.AreEqual(2, env.Count);
      Assert.IsTrue(!env.Contains(peaks[2]));
    }

    [Test]
    public void TestDeisotopic()
    {
      var peaks = new PeakList<Peak>();

      peaks.Add(new Peak(101.0000, 10, 1));
      peaks.Add(new Peak(101.2000, 3, 0));
      peaks.Add(new Peak(102.0034, 20, 1));
      peaks.Add(new Peak(103.0067, 10, 1));
      peaks.Add(new Peak(104.0101, 20, 1));
      peaks.Add(new Peak(105.0134, 10, 1));

      //peaks.ForEach(m => Console.WriteLine(m));

      var cur = this.Process(peaks);
      Assert.AreEqual(4, cur.Count);

      //no charge
      Assert.AreEqual(101.2000, peaks[1].Mz, 0.1);

      //first envelope
      Assert.AreEqual(101.0000, peaks[0].Mz, 0.1);
      Assert.AreEqual(102.0034, peaks[2].Mz, 0.1);

      //second envelope
      Assert.AreEqual(104.0101, peaks[3].Mz, 0.1);
    }
  }
}