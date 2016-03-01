using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.MaxQuant
{
  [TestFixture]
  public class TestMaxQuantPeptideTextReader
  {
    [Test]
    public void TestRead()
    {
      MaxQuantPeptideTextReader reader = new MaxQuantPeptideTextReader();
      var spectra = reader.ReadFromFile(@"../../../data/maxquant_sites.txt");
      Assert.AreEqual(162, spectra.Count);
      Assert.AreEqual("LQR_SCS_Nu_CA_SAX_Online_071226_06", spectra[0].Query.FileScan.Experimental);
      Assert.AreEqual(12044, spectra[0].Query.FileScan.FirstScan);
      Assert.AreEqual(0.000888, spectra[0].ExpectValue, 0.000001);
      Assert.AreEqual(21.4, spectra[0].Score, 0.0001);
      Assert.AreEqual("ASEDES*DLEDEEEKSQEDTEQK", spectra[0].Sequence);

      Assert.AreEqual("#S*GEDEQQEQTIAEDLVVTK", spectra[15].Sequence);

      Assert.AreEqual("*", reader.ModificationMap["(ph)"]);
      Assert.AreEqual("#", reader.ModificationMap["(ac)"]);
      Assert.AreEqual("@", reader.ModificationMap["(ox)"]);
    }

    [Test]
    public void TestReadPeptideFile()
    {
      MaxQuantPeptideTextFormat reader = new MaxQuantPeptideTextFormat();

      var spectra = reader.ReadFromFile(@"../../../data/maxquant.peptides.txt");

      for (int i = 0; i < spectra.Count; i++)
      {
        var s = spectra[i];
        if (s.Query.FileScan.Experimental == "exp3_T_SAX_091215_02")
        {
          Console.WriteLine("{0}, {1}", i, s.Query.FileScan.LongFileName);
          //break;
        }
      }
      
    }


    //[Test]
    //public void TestRead2()
    //{
    //  MaxQuantPeptideTextReader reader = new MaxQuantPeptideTextReader();

    //  //      var spectra = reader.ReadFromFile(@"../../../data/maxquant_sites_2.txt");
    //  var spectra = reader.ReadFromFile(@"Z:\cli_1\20091212_Orbi_WntExp1234_275mgTA\combined\Phospho (STY)Sites_1771.txt");

    //  for (int i = 0; i < spectra.Count; i++)
    //  {
    //    var s = spectra[i];
    //    if (s.Query.FileScan.Experimental == "exp3_T_SAX_091215_02")
    //    {
    //      Console.WriteLine("{0}, {1}", i, s.Query.FileScan.LongFileName);
    //      //break;
    //    }
    //  }

    //  //Assert.AreEqual(1, spectra.Count);
    //  //Assert.AreEqual("exp1_T_SAX_091211_03", spectra[0].Query.FileScan.Experimental);
    //  //Assert.AreEqual(10536, spectra[0].Query.FileScan.FirstScan);
    //  //Assert.AreEqual(0.659591, spectra[0].ExpectValue, 0.000001);
    //  //Assert.AreEqual(70.17, spectra[0].Score, 0.0001);
    //  //Assert.AreEqual("KT*SFDQDSDVDIFPSDFPTEPPSLPR", spectra[0].Sequence);

    //  //foreach (var mod in reader.ModificationMap)
    //  //{
    //  //  Console.WriteLine("{0} ==> {1}", mod.Value, mod.Key);
    //  //}
    //}
  }
}
