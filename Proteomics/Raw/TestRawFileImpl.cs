using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System;

namespace RCPA.Proteomics.Raw
{
  [TestFixture]
  public class TestRawFileImpl
  {
    private RawFileImpl fullRawFile;
    private RawFileImpl ms2OnlyRawFile;

    [OneTimeSetUp]
    public void TestSetup()
    {
      this.fullRawFile = new RawFileImpl();
      this.fullRawFile.Open(@TestContext.CurrentContext.TestDirectory + "/../../../data//Standard_Protein_FT2_Lock_Re5.RAW");
      this.ms2OnlyRawFile = new RawFileImpl();
      this.ms2OnlyRawFile.Open(@TestContext.CurrentContext.TestDirectory + "/../../../data//Full_scan_apiginin_ms2.RAW");
    }

    [OneTimeTearDown]
    public void TestTearDown()
    {
      this.fullRawFile.Close();
      this.ms2OnlyRawFile.Close();
    }

    [Test]
    public void TestFullRawFileGetPrecursorPeak()
    {
      Peak precursorPeak = this.fullRawFile.GetPrecursorPeak(2968);
      Assert.AreEqual(726.3757, precursorPeak.Mz, 0.0001);
      Assert.AreEqual(1, precursorPeak.Charge);

      precursorPeak = this.fullRawFile.GetPrecursorPeak(4310);
      Assert.AreEqual(555.8007, precursorPeak.Mz, 0.0001);
      Assert.AreEqual(2, precursorPeak.Charge);

      //This ion charge is hard to be determinated by manually,
      //But, the correct one can be determinated by dll function
      var sts = new List<ScanTime>();
      sts.Add(new ScanTime(6308, 85.24));
      sts.Add(new ScanTime(6310, 85.24));
      Peak peak = this.fullRawFile.GetPrecursorPeak(sts);
      Assert.AreEqual(898.81, peak.Mz, 0.01);
      Assert.AreEqual(3, peak.Charge);
    }

    //[Test]
    //public void TestGetMSOrderForScanNum()
    //{
    //  Assert.AreEqual(1, this.fullRawFile.GetMSOrderForScanNum(4509));
    //  Assert.AreEqual(2, this.fullRawFile.GetMSOrderForScanNum(4510));
    //  Assert.AreEqual(2, this.fullRawFile.GetMSOrderForScanNum(4511));
    //  Assert.AreEqual(2, this.fullRawFile.GetMSOrderForScanNum(4512));

    //  //using (RawFileImpl rawFile = new RawFileImpl(@"E:\sqh\Science\Project\liqingrun\4plex\20110915_iTRAQ_4plex_GK_6ug_Exp_2.raw"))
    //  //{
    //  //  Assert.AreEqual(2, rawFile.GetMSOrderForScanNum(319));
    //  //}
    //}

    [Test]
    public void TestMs2GetPrecursorPeak()
    {
      var precursor = this.ms2OnlyRawFile.GetPrecursorPeakWithMasterScan(19);
      Assert.AreEqual(271.0, precursor.IsolationMass, 0.1);
      Assert.AreEqual(0, precursor.Charge);
      Assert.AreEqual(0, precursor.MasterScan);
    }


    [Test]
    public void TestFullRawFileGetPrecursorPeak2()
    {
      var precursor = this.fullRawFile.GetPrecursorPeakWithMasterScan(4512);
      Assert.AreEqual(716.66, precursor.IsolationMass, 0.01);
      Assert.AreEqual(716.3293, precursor.MonoIsotopicMass, 0.0001);
      Assert.AreEqual(3, precursor.Charge);
      Assert.AreEqual(4509, precursor.MasterScan);
    }

    [Test]
    public void TestGetIsolationWidth()
    {
      Assert.AreEqual(4.0, this.fullRawFile.GetIsolationWidth(4512), 0.1);
    }

    [Test]
    public void TestGetPrecursorRatioInIsolationWindow()
    {
      var pkls = new List<Peak>(new Peak[]{
        new Peak(853.4315,61720.7,0),
        new Peak(853.5131,194892.4,0),
        new Peak(853.7602,52777.0,0),
        new Peak(853.9072,43998.6,0),
        new Peak(854.3759,94792.7,0),
        new Peak(854.5167,116125.9,0),
        new Peak(854.6985,27936.1,0),
        new Peak(854.7495,2581131.0,3),
        new Peak(854.8786,67194.7,0),
        new Peak(855.0834,3478605.8,3),
        new Peak(855.3676,118008.5,0),
        new Peak(855.4175,2792174.0,3),
        new Peak(855.7515,1231560.0,3),
        new Peak(855.8677,119439.1,0),
        new Peak(856.0851,547235.6,3),
        new Peak(856.3688,73538.4,0),
        new Peak(856.4202,242455.5,3),
        new Peak(856.7521,85777.1,3),
        new Peak(856.8657,30313.8,0),
        new Peak(857.0681,37960.8,3)});

      var precursorIntensity = (from p in pkls
                                where p.Charge == 3
                                select p.Intensity).Sum() - 37960.8;

      var totalIntensity = pkls.Sum(m => m.Intensity);

      Assert.AreEqual(precursorIntensity / totalIntensity, fullRawFile.GetPrecursorPercentageInIsolationWindow(4234, 10), 0.001);
      //fullRawFile.GetPrecursorRatioInIsolationWindow(4528, 10);

      //for (int i = fullRawFile.GetFirstSpectrumNumber(); i <= fullRawFile.GetLastSpectrumNumber(); i++)
      //{
      //  if (2 == fullRawFile.GetMsLevel(i))
      //  {
      //    fullRawFile.GetPrecursorRatioInIsolationWindow(i, 10);
      //  }
      //}
    }

    [Test]
    public void TestGetLabelData()
    {
      int scan = 4677;
      double ppmTolerance = 20;
      PeakList<Peak> pkl1 = this.fullRawFile.GetLabelData(scan);
      pkl1.FilterByTolerance(ppmTolerance);

      var cd = new ChargeDeconvolution(ppmTolerance, 10);
      PeakList<Peak> pkl2 = this.fullRawFile.GetMassListFromScanNum(scan, true);
      pkl2.FilterByTolerance(ppmTolerance);
      cd.Deconvolute(pkl2);

      Assert.AreEqual(pkl1.Count, pkl2.Count);
    }

    [Test]
    public void TestGetNumSpectra()
    {
      Assert.AreEqual(8632, this.fullRawFile.GetNumSpectra());
      Assert.AreEqual("FTMS + c ESI Full ms [400.00-2000.00]", this.fullRawFile.GetFilterForScanNum(1));

      Assert.AreEqual(29, this.ms2OnlyRawFile.GetNumSpectra());
      Assert.AreEqual("- p Full ms2 271.00@cid0.00 [80.00-275.00]", this.ms2OnlyRawFile.GetFilterForScanNum(1));
    }

    [Test]
    public void TestGetPeakListInfo()
    {
      var pkl = new PeakList<Peak>();

      this.fullRawFile.GetPeakListInfo(2968, pkl);
      Assert.AreEqual(726.3757, pkl.PrecursorMZ, 0.0001);
      Assert.AreEqual(1, pkl.PrecursorCharge);

      this.fullRawFile.GetPeakListInfo(4310, pkl);
      Assert.AreEqual(555.8007, pkl.PrecursorMZ, 0.0001);
      Assert.AreEqual(2, pkl.PrecursorCharge);

      this.fullRawFile.GetPeakListInfo(4372, pkl);
      Assert.AreEqual(671.84, pkl.PrecursorMZ, 0.01);
      Assert.AreEqual(0, pkl.PrecursorCharge);
    }

    [Test]
    public void TestMs2OnlyRawFileGetPrecursorPeak()
    {
      Peak precursorPeak = this.ms2OnlyRawFile.GetPrecursorPeak(2);
      Assert.AreEqual(271.00, precursorPeak.Mz, 0.01);
      Assert.AreEqual(0, precursorPeak.Charge);
    }

    [Test]
    public void TestGetIonInjectionTimeFromTrailerExtraValue()
    {
      var iit = this.fullRawFile.GetIonInjectionTime(4310);
      Assert.AreEqual(11.29, iit, 0.01);
    }

    [Test]
    public void TestGetMsType()
    {
      Assert.AreEqual("", this.fullRawFile.GetScanMode(1));
      Assert.AreEqual("cid", this.fullRawFile.GetScanMode(30));
    }
  }
}