using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestOutZipParser
  {

    [Test]
    public void Run()
    {
      var file = @"..\..\data\FIT_HPPP_Bound_060622_04.zip";
      SequestOutZipParser parser = new SequestOutZipParser();
      var spectra = parser.ParsePeptides(file);
      Assert.AreEqual(2, spectra.Count);
    }
  }
}
