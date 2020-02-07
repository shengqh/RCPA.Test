using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Spectrum
{
  [TestFixture]
  public class TestPeakList
  {
    private PeakList<Peak> pl;

    [SetUp]
    public void SetUp()
    {
      pl = new PeakList<Peak>();
      pl.Add(new Peak(430.0884, 43875.5, 1));

      pl.Add(new Peak(445.1205, 335180.5, 1));
      pl.Add(new Peak(446.1185, 51638.0, 1));
      pl.Add(new Peak(446.1255, 129918.6, 1));
      pl.Add(new Peak(447.1170, 30164.7, 1));

      pl.Add(new Peak(491.9578, 442529.3, 3));
      pl.Add(new Peak(492.2919, 206717.3, 3));
      pl.Add(new Peak(492.6270, 137434.5, 3));
      pl.Add(new Peak(492.9613, 26216.6, 3));

      pl.Add(new Peak(531.2642, 129351.8, 4));

      pl.Add(new Peak(631.2642, 129351.8, 0));
    }

    [Test]
    public void TestFindPeak()
    {
      PeakList<Peak> findPeak = pl.FindPeak(446.12, 0.1);
      Assert.AreEqual(2, findPeak.Count);
      Assert.AreSame(pl[2], findPeak[0]);
      Assert.AreSame(pl[3], findPeak[1]);
    }

    [Test]
    public void TestFindMaxIntensity()
    {
      Peak peak = pl.FindMaxIntensityPeak();
      Assert.AreSame(pl[5], peak);
    }

    [Test]
    public void TestFindEnvelop()
    {
      PeakList<Peak> envolopCharge1 = pl.FindEnvelope(pl[2], 0.02, false);
      Assert.AreEqual(3, envolopCharge1.Count);
      Assert.AreSame(pl[1], envolopCharge1[0]);
      Assert.AreSame(pl[3], envolopCharge1[1]);
      Assert.AreSame(pl[4], envolopCharge1[2]);

      PeakList<Peak> envolopCharge3 = pl.FindEnvelope(pl[8], 0.02, false);
      Assert.AreEqual(4, envolopCharge3.Count);
      Assert.AreSame(pl[5], envolopCharge3[0]);
      Assert.AreSame(pl[6], envolopCharge3[1]);
      Assert.AreSame(pl[7], envolopCharge3[2]);
      Assert.AreSame(pl[8], envolopCharge3[3]);
    }

    [Test]
    public void TestFindEnvelopeDirectly()
    {
      var e = new Envelope(444.12, 1, 4).ToList().ConvertAll(m => new Peak() { Mz = m }).ToList();
      PeakList<Peak> envolopCharge1 = pl.FindEnvelopeDirectly(e, 0.02, () => new Peak());
      Assert.AreEqual(4, envolopCharge1.Count);
      Assert.AreEqual(0, envolopCharge1[0].Intensity);
      Assert.AreSame(pl[1], envolopCharge1[1]);
      Assert.AreSame(pl[3], envolopCharge1[2]);
      Assert.AreSame(pl[4], envolopCharge1[3]);
    }

    [Test]
    public void TestGetEnvelopes()
    {
      List<PeakList<Peak>> envelopes = pl.GetEnvelopes(20);
      Assert.AreEqual(5, envelopes.Count);
    }

    [Test]
    public void TestFindEnvelopeInList()
    {
      List<PeakList<Peak>> envelopes = pl.GetEnvelopes(20);
      Peak peak = new Peak(446.1185, 51638.0, 1);
      double mzTolerance = PrecursorUtils.ppm2mz(peak.Mz, 20);
      PeakList<Peak> actual = PeakList<Peak>.FindEnvelopeInList(envelopes, peak, mzTolerance);
      Assert.AreSame(envelopes[1], actual);

      Peak peak2 = new Peak(631.2642, 129351.8, 1);
      double mzTolerance2 = PrecursorUtils.ppm2mz(peak2.Mz, 20);
      PeakList<Peak> actual2 = PeakList<Peak>.FindEnvelopeInList(envelopes, peak2, mzTolerance2);
      Assert.AreSame(envelopes[4], actual2);

      Peak missPeak = new Peak(731.2642, 129351.8, 1);
      double missMzTolerance = PrecursorUtils.ppm2mz(missPeak.Mz, 20);
      PeakList<Peak> actualMiss = PeakList<Peak>.FindEnvelopeInList(envelopes, missPeak, missMzTolerance);
      Assert.AreSame(null, actualMiss);
    }

    [Test]
    public void TestFilterByTolerance()
    {
      pl.FilterByTolerance(20);

      //The Peak(446.1255, 29918.6, 1) will be removed
      Assert.AreEqual(10, pl.Count);
      Assert.AreEqual(446.1255, pl[2].Mz, 0.0001);
    }

    [Test]
    public void TestMergeWith()
    {
      PeakList<Peak> pl1 = new PeakList<Peak>();
      pl1.Add(new Peak(420, 10, 1));
      pl1.Add(new Peak(445, 10, 1));

      PeakList<Peak> pl2 = new PeakList<Peak>();
      pl2.Add(new Peak(420.004, 30, 2));
      pl2.Add(new Peak(445.004, 30, 1));

      pl1.MergeByMZFirst(pl2, 50);

      Assert.AreEqual(3, pl1.Count);
      Assert.AreEqual(420, pl1[0].Mz, 0.001);
      Assert.AreEqual(1, pl1[0].Charge);
      Assert.AreEqual(420.004, pl1[1].Mz, 0.001);
      Assert.AreEqual(2, pl1[1].Charge);
      Assert.AreEqual(445.003, pl1[2].Mz, 0.001);
      Assert.AreEqual(1, pl1[2].Charge);

      Assert.AreEqual(10, pl1[0].Intensity);
      Assert.AreEqual(30, pl1[1].Intensity);
      Assert.AreEqual(40, pl1[2].Intensity);
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestGetRangeException()
    {
      double min = 440;
      double max = 450;
      pl.GetRange(max, min);
    }

    [Test]
    public void TestGetRange()
    {
      double min = 440;
      double max = 450;
      PeakList<Peak> rangePeaks = pl.GetRange(min, max);
      Assert.AreEqual(4, rangePeaks.Count);
      foreach (Peak p in rangePeaks)
      {
        Assert.Greater(p.Mz, min);
        Assert.Less(p.Mz, max);
      }
    }

    public void TestDeduct()
    {
      var pkls = new MascotGenericFormatReader<Peak>().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/TestPeakListDeductionByWindow.mgf");
      var pkl = pkls[0];

      Assert.AreEqual(296, pkl.Count);
      pkl.KeepTopXInWindow(6, 100);
      
      //pkl.ForEach(m => Console.WriteLine("{0:0.00000}\t{1:0.0}", m.Mz, m.Intensity));
      Assert.AreEqual(47, pkl.Count);
    }
  }
}
