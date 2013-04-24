using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using RCPA.Proteomics.Summary; 

namespace RCPA.Proteomics.PeptideProphet
{
  [TestFixture]
  public class TestProteinProphetXmlReader
  {
    [Test]
    public void TestRead()
    {
      var result = new ProteinProphetXmlReader().ReadFromFile(@"..\..\data\proteinprophet.xml");

      var filters = result.GetProteinSummaryDataFilterList();
      Assert.AreEqual(16, filters.Count);
      Assert.AreEqual(1.0, filters.Last().MinProbability);
      Assert.AreEqual(0.493, filters.Last().Sensitivity);
      Assert.AreEqual(0.000, filters.Last().FalsePositiveErrorRate);
      Assert.AreEqual(1982, filters.Last().PredictedNumCorrect);
      Assert.AreEqual(0, filters.Last().PredictedNumIncorrect);

      var group = result[0];
      Assert.AreEqual("sp|A2A791|ZMYM4_MOUSE", group[0].Name);
      Assert.AreEqual(6.3, group[0].Coverage);
      Assert.AreEqual("Zinc finger MYM-type protein 4 OS=Mus musculus GN=Zmym4 PE=1 SV=1", group[0].Description);

      var items = group[0].Peptides;
      Assert.AreEqual(8, items.Count);

      Assert.AreEqual("GETEQDLEADFPSESFDPLNK", items[0].Sequence);
      Assert.AreEqual(0.9990, items[0].Spectrum.PValue);
      Assert.AreEqual(2, items[0].Spectrum.Charge);

      Assert.AreEqual("GETEQDLEADFPSESFDPLNK*", items[1].Sequence);
      Assert.AreEqual(0.9990, items[1].Spectrum.PValue);
      Assert.AreEqual(2, items[1].Spectrum.Charge);
    }
  }
}