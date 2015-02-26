using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  [TestFixture]
  public class TestIsobaricPeptideStatisticBuilder
  {
    [Test]
    public void TestFilterSpectraByQuantifyMode()
    {
      var options = new IsobaricPeptideStatisticBuilderOptions()
      {
        ModifiedAminoacids = "STY",
        MinimumSiteProbability = 0.8
      };

      //For all spectra, nothing will be filtered
      options.Mode = QuantifyMode.qmAll;
      var isoSpectra = InitializeSpectra();
      new IsobaricPeptideStatisticBuilder(options).FilterSpectraByQuantifyMode(isoSpectra);
      Assert.AreEqual(3, isoSpectra.Count);

      //Unmodified spectra
      options.Mode = QuantifyMode.qmUnmodifiedPeptide;
      isoSpectra = InitializeSpectra();
      new IsobaricPeptideStatisticBuilder(options).FilterSpectraByQuantifyMode(isoSpectra);
      Assert.AreEqual(1, isoSpectra.Count);
      Assert.AreEqual("^AAAQLLQSQAQQSGAQQTK@", isoSpectra[0].Sequence);

      //modified spectra, not filter site probability
      options.Mode = QuantifyMode.qmModificationSite;
      options.MinimumSiteProbability = -0.1;
      isoSpectra = InitializeSpectra();
      new IsobaricPeptideStatisticBuilder(options).FilterSpectraByQuantifyMode(isoSpectra);
      Assert.AreEqual(2, isoSpectra.Count);
      Assert.IsTrue(isoSpectra.All(m => !m.Sequence.Equals("^AAAQLLQSQAQQSGAQQTK@")));

      //modified spectra, filter site probability
      options.MinimumSiteProbability = 0.8;
      isoSpectra = InitializeSpectra();
      new IsobaricPeptideStatisticBuilder(options).FilterSpectraByQuantifyMode(isoSpectra);
      Assert.AreEqual(1, isoSpectra.Count);
      Assert.AreEqual("^AAADTGAS*PR", isoSpectra[0].Sequence);
    }

    private static List<IIdentifiedSpectrum> InitializeSpectra()
    {
      var isoSpectra = new List<IIdentifiedSpectrum>();
      AddSpectrum(isoSpectra, "^AAADTGAS*PR", "S(8): 100");
      AddSpectrum(isoSpectra, "^AAAQLLQSQAQQSGAQQTK@", "");//not modified
      AddSpectrum(isoSpectra, "^AELGM#NDSPS*QS*PPVK@", "S(10): 0; S(12): 100");
      return isoSpectra;
    }

    private static void AddSpectrum(List<IIdentifiedSpectrum> isoSpectra, string seq, string site)
    {
      var spec = new IdentifiedSpectrum();
      spec.AddPeptide(new IdentifiedPeptide(spec)
      {
        Sequence = seq,
        SiteProbability = site
      });
      isoSpectra.Add(spec);
    }
  }
}
