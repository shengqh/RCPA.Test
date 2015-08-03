using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.MSAmanda
{
  [TestFixture]
  public class TestMSAmandaRank2Parser
  {
    [Test]
    public void Test()
    {
      var peptides = new MSAmandaRank2Parser()
      {
        TitleParser = new DefaultTitleParser()
      }.ReadFromFile(@"../../../data/msamanda.tsv");

      Assert.AreEqual(4, peptides.Count);
      Assert.AreEqual(1, peptides[0].Peptides.Count);
      Assert.AreEqual("HGHIHR", peptides[0].Peptides[0].Sequence);
      Assert.AreEqual("tr|D3ZBX7", peptides[0].GetProteins(" | "));
      Assert.AreEqual("20110915_iTRAQ_4plex_GK_6ug_Exp_2.1962.1962.3.dta", peptides[0].Query.FileScan.LongFileName);
      Assert.AreEqual(1119.8, peptides[0].Query.FileScan.RetentionTime);
      Assert.AreEqual("N-Term(itraq114 on nterm|144.105918|fixed)", peptides[0].Modifications);

      //The peptide rank 2 had identical sequence to rank 1, so rank 3 will be picked.
      Assert.AreEqual(1, peptides[3].Peptides.Count);
      Assert.AreEqual("ASTMIYK", peptides[3].Peptides[0].Sequence);
    }
  }
}
