using System;
using NUnit.Framework;
using RCPA.Proteomics.Quantification.Census;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestCensusProteinItem
  {
    [Test]
    [ExpectedException(typeof (Exception))]
    public void TestException()
    {
      CensusProteinItem.Parse("");
    }

    [Test]
    public void TestParse()
    {
      CensusProteinItem item =
        CensusProteinItem.Parse(
          "P	YDR050C	0.98	0.27	0.99	530	624	YDR050C TPI1 SGDID:S000002457, Chr IV from 556470-555724, reverse complement, Verified ORF, \"Triose phosphate isomerase, abundant glycolytic enzyme; mRNA half-life is regulated by iron availability; transcription is controlled by activators Reb1p, Gcr1p, and Rap1p through binding sites in the 5' non-coding region\"");

      Assert.IsNotNull(item);

      Assert.AreEqual("YDR050C", item.Locus);

      Assert.AreEqual(0.98, item.AverageRatio, 0.01);

      Assert.AreEqual(0.27, item.StandardDeviation, 0.01);

      Assert.AreEqual(0.99, item.WeightedAverage, 0.01);

      Assert.AreEqual(530, item.PeptideNumber);

      Assert.AreEqual(624, item.SpectraCount);

      Assert.AreEqual(
        "YDR050C TPI1 SGDID:S000002457, Chr IV from 556470-555724, reverse complement, Verified ORF, \"Triose phosphate isomerase, abundant glycolytic enzyme; mRNA half-life is regulated by iron availability; transcription is controlled by activators Reb1p, Gcr1p, and Rap1p through binding sites in the 5' non-coding region\"",
        item.Description);
    }
  }
}