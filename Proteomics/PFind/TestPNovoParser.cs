using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PFind
{
  [TestFixture]
  public class TestPNovoParser
  {
    [Test]
    public void Parse()
    {
      var parser = new PNovoParser(new DefaultTitleParser());
      var spectra = parser.ParsePeptides(@"../../../data/pnovo.result");
      Assert.AreEqual(20, spectra.Count);
      Assert.AreEqual("K*TESHHK", spectra[0].Peptide.Sequence);

      Assert.AreEqual(2, parser.ModificationCharMap.Count);
      Assert.AreEqual('*', parser.ModificationCharMap["+8.014199"]);
      Assert.AreEqual('#', parser.ModificationCharMap["+10.008269"]);
    }
  }
}
