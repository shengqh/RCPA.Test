using NUnit.Framework;
using RCPA.Proteomics.Quantification.O18;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestO18QuantificationSummaryItemTextFormat
  {
    [Test]
    public void TestReadFromFile()
    {
      O18QuantificationSummaryItem item =
        new O18QuantificationSummaryItemTextFormat().ReadFromFile("../../data/DH1_3a.RAW.DH1_3a.EMSDIIQR.4399.o18");

      Assert.AreEqual(@"D:\DH1_3a.raw", item.RawFilename);

      Assert.AreEqual("EMSDIIQR", item.PeptideSequence);

      Assert.AreEqual(0.95, item.PurityOfO18Water);

      Assert.AreEqual("H70O15C40N12S1", item.PeptideAtomComposition);

      Assert.AreEqual(9, item.PeptideProfile.Count);
      Assert.AreEqual(0.5635, item.PeptideProfile[0], 0.0001);

      Assert.AreEqual(28, item.ObservedEnvelopes.Count);
      Assert.AreEqual(4397, item.ObservedEnvelopes[0].ScanTimes[0].Scan);
      Assert.AreEqual(6, item.ObservedEnvelopes[0].Count);
      Assert.AreEqual(496.2486, item.ObservedEnvelopes[0][0].Mz, 0.0001);
      Assert.AreEqual(343843.5, item.ObservedEnvelopes[0][0].Intensity, 0.1);

      Assert.AreEqual(104497572.8, item.SpeciesAbundance.O16, 0.1);
      Assert.AreEqual(0.0, item.SpeciesAbundance.O181, 0.1);
      Assert.AreEqual(22380072.0, item.SpeciesAbundance.O182, 0.1);

      Assert.AreEqual(0.9879, item.SpeciesAbundance.RegressionCorrelation, 0.0001);
      Assert.AreEqual(6, item.SpeciesAbundance.RegressionItems.Count);
      Assert.AreEqual(496.2486, item.SpeciesAbundance.RegressionItems[0].Mz, 0.0001);
      Assert.AreEqual(63470188.9, item.SpeciesAbundance.RegressionItems[0].ObservedIntensity, 0.1);
      Assert.AreEqual(58885083.0, item.SpeciesAbundance.RegressionItems[0].RegressionIntensity, 0.1);

      Assert.AreEqual(104441482.4, item.SampleAbundance.O16, 0.1);
      Assert.AreEqual(22436162.4, item.SampleAbundance.O18, 0.1);
      Assert.AreEqual(5.0, item.SampleAbundance.Ratio);
    }
  }
}