using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Isotopic;

namespace RCPA.Proteomics.Quantification.O18
{
  [TestFixture]
  public class TestO18QuantificationSummaryItemXmlFormat
  {
    [Test]
    public void TestLoadFromFile()
    {
      O18QuantificationSummaryItem item =
        new O18QuantificationSummaryItemXmlFormat().ReadFromFile(@"..\..\data\O18QuantificationInformation.xml");
      Assert.AreEqual(@"D:\sqh\Science\Project/4NLFOR.raw", item.RawFilename);
      Assert.AreEqual("AFATDITDAEEDK", item.PeptideSequence);
      Assert.AreEqual("C60H92N14O26", item.PeptideAtomComposition);
      Assert.AreEqual(0.95, item.PurityOfO18Water, 0.01);

      var expectPeptideProfile = new List<double>(new[] {0.4611, 0.3340, 0.1439, 0.0459, 0.0119, 0.0026, 0.0005, 0.0001});
      Assert.AreEqual(expectPeptideProfile, item.PeptideProfile);

      Assert.AreEqual(713.3209, item.SpeciesAbundance.RegressionItems[0].Mz);
      Assert.AreEqual(36513020.6, item.SpeciesAbundance.RegressionItems[0].ObservedIntensity);
      Assert.AreEqual(36512100.5, item.SpeciesAbundance.RegressionItems[0].RegressionIntensity);
      Assert.AreEqual(713.8254, item.SpeciesAbundance.RegressionItems[1].Mz);
      Assert.AreEqual(26438219.2, item.SpeciesAbundance.RegressionItems[1].ObservedIntensity);
      Assert.AreEqual(26447909.0, item.SpeciesAbundance.RegressionItems[1].RegressionIntensity);
      Assert.AreEqual(714.3221, item.SpeciesAbundance.RegressionItems[2].Mz);
      Assert.AreEqual(29639265.4, item.SpeciesAbundance.RegressionItems[2].ObservedIntensity);
      Assert.AreEqual(29595148.6, item.SpeciesAbundance.RegressionItems[2].RegressionIntensity);
      Assert.AreEqual(714.8242, item.SpeciesAbundance.RegressionItems[3].Mz);
      Assert.AreEqual(16687280.4, item.SpeciesAbundance.RegressionItems[3].ObservedIntensity);
      Assert.AreEqual(16812006.0, item.SpeciesAbundance.RegressionItems[3].RegressionIntensity);
      Assert.AreEqual(715.3243, item.SpeciesAbundance.RegressionItems[4].Mz);
      Assert.AreEqual(7087781.0, item.SpeciesAbundance.RegressionItems[4].ObservedIntensity);
      Assert.AreEqual(6823450.3, item.SpeciesAbundance.RegressionItems[4].RegressionIntensity);
      Assert.AreEqual(715.8316, item.SpeciesAbundance.RegressionItems[5].Mz);
      Assert.AreEqual(1799203.9, item.SpeciesAbundance.RegressionItems[5].ObservedIntensity);
      Assert.AreEqual(2164120.0, item.SpeciesAbundance.RegressionItems[5].RegressionIntensity);

      Assert.AreEqual(2, item.ObservedEnvelopes.Count);

      Assert.AreEqual(6741, item.ObservedEnvelopes[0].ScanTimes[0].Scan);
      Assert.AreEqual(12.34, item.ObservedEnvelopes[0].ScanTimes[0].RetentionTime);
      Assert.IsTrue(item.ObservedEnvelopes[0].Enabled);
      Assert.IsTrue(item.ObservedEnvelopes[0].IsIdentified);
      Assert.AreEqual(713.3209, item.ObservedEnvelopes[0][0].Mz);
      Assert.AreEqual(424345.9, item.ObservedEnvelopes[0][0].Intensity);
      Assert.AreEqual(713.8254, item.ObservedEnvelopes[0][1].Mz);
      Assert.AreEqual(198378.3, item.ObservedEnvelopes[0][1].Intensity);
      Assert.AreEqual(714.3221, item.ObservedEnvelopes[0][2].Mz);
      Assert.AreEqual(246866.8, item.ObservedEnvelopes[0][2].Intensity);
      Assert.AreEqual(714.8242, item.ObservedEnvelopes[0][3].Mz);
      Assert.AreEqual(221648.0, item.ObservedEnvelopes[0][3].Intensity);
      Assert.AreEqual(715.3243, item.ObservedEnvelopes[0][4].Mz);
      Assert.AreEqual(107701.3, item.ObservedEnvelopes[0][4].Intensity);
      Assert.AreEqual(715.8316, item.ObservedEnvelopes[0][5].Mz);
      Assert.AreEqual(0.0, item.ObservedEnvelopes[0][5].Intensity);

      Assert.AreEqual(6746, item.ObservedEnvelopes[1].ScanTimes[0].Scan);
      Assert.AreEqual(13.34, item.ObservedEnvelopes[1].ScanTimes[0].RetentionTime);
      Assert.IsFalse(item.ObservedEnvelopes[1].Enabled);
      Assert.IsFalse(item.ObservedEnvelopes[1].IsIdentified);
      Assert.AreEqual(713.3209, item.ObservedEnvelopes[1][0].Mz);
      Assert.AreEqual(796597.7, item.ObservedEnvelopes[1][0].Intensity);
      Assert.AreEqual(713.8254, item.ObservedEnvelopes[1][1].Mz);
      Assert.AreEqual(530778.4, item.ObservedEnvelopes[1][1].Intensity);
      Assert.AreEqual(714.3221, item.ObservedEnvelopes[1][2].Mz);
      Assert.AreEqual(456099.6, item.ObservedEnvelopes[1][2].Intensity);
      Assert.AreEqual(714.8242, item.ObservedEnvelopes[1][3].Mz);
      Assert.AreEqual(339509.8, item.ObservedEnvelopes[1][3].Intensity);
      Assert.AreEqual(715.3243, item.ObservedEnvelopes[1][4].Mz);
      Assert.AreEqual(182812.3, item.ObservedEnvelopes[1][4].Intensity);
      Assert.AreEqual(715.8316, item.ObservedEnvelopes[1][5].Mz);
      Assert.AreEqual(0.0, item.ObservedEnvelopes[1][5].Intensity);

      Assert.AreEqual(79184307.0, item.SpeciesAbundance.O16);
      Assert.AreEqual(39464386.0, item.SpeciesAbundance.O181);
      Assert.AreEqual(440969.4, item.SpeciesAbundance.O182);

      Assert.AreEqual(0.0219, item.SampleAbundance.LabellingEfficiency);
      Assert.AreEqual(50, item.SampleAbundance.Ratio);
      Assert.AreEqual(0.0, item.SampleAbundance.O16);
      Assert.AreEqual(922867772.7, item.SampleAbundance.O18);
    }

    [Test]
    public void TestWriteToFile()
    {
      var item = new O18QuantificationSummaryItem();
      item.RawFilename = @"D:\sqh\Science\Project/4NLFOR.raw";
      item.PeptideSequence = "AFATDITDAEEDK";
      item.PeptideAtomComposition = "C60H92N14O26";
      item.PurityOfO18Water = 0.95;
      item.PeptideProfile = IsotopicBuilderFactory.GetBuilder().GetProfile(new AtomComposition(item.PeptideAtomComposition), 1, 10);

      item.ObservedEnvelopes = new List<O18QuanEnvelope> ();

      var pkl1 = new O18QuanEnvelope();
      pkl1.ScanTimes.Add(new ScanTime(6741, 12.34));
      pkl1.Enabled = true;
      pkl1.IsIdentified = true;
      pkl1.Add(new Peak(713.3209, 424345.9));
      pkl1.Add(new Peak(713.8254, 198378.3));
      pkl1.Add(new Peak(714.3221, 246866.8));
      pkl1.Add(new Peak(714.8242, 221648.0));
      pkl1.Add(new Peak(715.3243, 107701.3));
      pkl1.Add(new Peak(715.8316, 0.0));
      item.ObservedEnvelopes.Add(pkl1);

      var pkl2 = new O18QuanEnvelope();
      pkl2.ScanTimes.Add(new ScanTime(6746, 13.34));
      pkl2.Enabled = false;
      pkl2.Add(new Peak(713.3209, 796597.7));
      pkl2.Add(new Peak(713.8254, 530778.4));
      pkl2.Add(new Peak(714.3221, 456099.6));
      pkl2.Add(new Peak(714.8242, 339509.8));
      pkl2.Add(new Peak(715.3243, 182812.3));
      pkl2.Add(new Peak(715.8316, 0.0));
      item.ObservedEnvelopes.Add(pkl2);

      item.SpeciesAbundance = new SpeciesAbundanceInfo();
      item.SpeciesAbundance.O16 = 79184307.0;
      item.SpeciesAbundance.O181 = 39464386.0;
      item.SpeciesAbundance.O182 = 440969.4;

      item.SampleAbundance = new SampleAbundanceInfo();
      item.SampleAbundance.LabellingEfficiency = 0.0219;
      item.SampleAbundance.Ratio = 50;
      item.SampleAbundance.O16 = 0;
      item.SampleAbundance.O18 = 922867772.7;

      item.SpeciesAbundance.RegressionCorrelation = 0.9999;
      item.SpeciesAbundance.RegressionItems.Clear();
      item.SpeciesAbundance.RegressionItems.Add(new SpeciesRegressionItem(713.3209, 36513020.6, 36512100.5));
      item.SpeciesAbundance.RegressionItems.Add(new SpeciesRegressionItem(713.8254, 26438219.2, 26447909.0));
      item.SpeciesAbundance.RegressionItems.Add(new SpeciesRegressionItem(714.3221, 29639265.4, 29595148.6));
      item.SpeciesAbundance.RegressionItems.Add(new SpeciesRegressionItem(714.8242, 16687280.4, 16812006.0));
      item.SpeciesAbundance.RegressionItems.Add(new SpeciesRegressionItem(715.3243, 7087781.0, 6823450.3));
      item.SpeciesAbundance.RegressionItems.Add(new SpeciesRegressionItem(715.8316, 1799203.9, 2164120.0));

      new O18QuantificationSummaryItemXmlFormat().WriteToFile(@"..\..\data\O18QuantificationInformation.xml.tmp", item);
      AssertUtils.AssertFileEqual(@"..\..\data\O18QuantificationInformation.xml.tmp",
                                  @"..\..\data\O18QuantificationInformation.xml");
      new FileInfo(@"..\..\data\O18QuantificationInformation.xml.tmp").Delete();
    }
  }
}