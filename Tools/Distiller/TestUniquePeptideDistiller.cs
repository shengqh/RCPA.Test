using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics;

namespace RCPA.Tools.Distiller
{
  [TestFixture]
  public class TestUniquePeptideDistiller
  {
    [Test]
    public void TestKeepMaxScorePeptideOnly()
    {
      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();
      spectra.Add(new IdentifiedSpectrum() { Score = 3.0 });
      spectra.Add(new IdentifiedSpectrum() { Score = 2.0 });
      
      spectra.ForEach(m =>
      {
        m.AddPeptide(new IdentifiedPeptide(m) { Sequence = "A1" });
        m.Charge = 2;
      });

      IdentifiedSpectrum other = new IdentifiedSpectrum() { Score = 2.0, Charge = 2 };
      other.AddPeptide(new IdentifiedPeptide(other) { Sequence = "B1" });
      spectra.Add(other);

      List<IIdentifiedSpectrum> filtered = UniquePeptideDistiller.KeepMaxScorePeptideOnly(spectra);
      Assert.AreEqual(2, filtered.Count);
      Assert.AreEqual(3.0, filtered[0].Score);
      Assert.AreEqual("A1", filtered[0].Sequence);
      Assert.AreEqual(2.0, filtered[1].Score);
      Assert.AreEqual("B1", filtered[1].Sequence);
    }
  }
}
