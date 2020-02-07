using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Spectrum
{
  [TestFixture]
  public class TestPeakListDeductionByWindow
  {
    public void TestDeduct()
    {
      var pkls = new MascotGenericFormatReader<Peak>().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/TestPeakListDeductionByWindow.mgf");
      var pkl = pkls[0];

      Assert.AreEqual(296, pkl.Count);
      new PeakListDeductionByWindow(6).Deduct(pkl);
      Assert.AreEqual(37, pkl.Count);

      //pkl.ForEach(m => Console.WriteLine("{0:0.00000}\t{1:0.0}", m.Mz, m.Intensity));
    }
  }
}
