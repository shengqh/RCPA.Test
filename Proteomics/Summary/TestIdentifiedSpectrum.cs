using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedSpectrum
  {
    [Test]
    public void TestSort()
    {
      IdentifiedSpectrum sp1 = new IdentifiedSpectrum();
      sp1.Query.FileScan.LongFileName = "YEAST_0612_G1_SAX_080806_09.17702.17702.2.out";
      new IdentifiedPeptide(sp1) { Sequence = "K.GFFSFATQK.L" };

      IdentifiedSpectrum sp2 = new IdentifiedSpectrum();
      sp2.Query.FileScan.LongFileName = "YEAST_0612_G1_SAX_080806_09.17707.17708.1.out";
      new IdentifiedPeptide(sp2) { Sequence = "K.GFFSFATQK.L" };

      IdentifiedSpectrum sp3 = new IdentifiedSpectrum();
      sp3.Query.FileScan.LongFileName = "YEAST_0612_G1_SAX_080806_09.7707.7708.2.out";
      new IdentifiedPeptide(sp3) { Sequence = "K.GFFSFATQK.L" };

      IdentifiedSpectrum sp4 = new IdentifiedSpectrum();
      sp4.Query.FileScan.LongFileName = "YEAST_0612_G1_SAX_080806_09.7707.7708.2.out";
      new IdentifiedPeptide(sp4) { Sequence = "K.AAAAAAAK.L" };

      List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();
      spectra.Add(sp2);
      spectra.Add(sp1);
      spectra.Add(sp3);
      spectra.Add(sp4);

      spectra.Sort();

      Assert.AreSame(sp4, spectra[0]);
      Assert.AreSame(sp3, spectra[1]);
      Assert.AreSame(sp1, spectra[2]);
      Assert.AreSame(sp2, spectra[3]);
    }
  }
}
