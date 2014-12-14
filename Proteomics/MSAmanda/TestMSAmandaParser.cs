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
  public class TestMSAmandaParser
  {
    [Test]
    public void Test()
    {
      var peptides = new MSAmandaParser()
      {
        TitleParser = new DefaultTitleParser()
      }.ReadFromFile(@"../../data/msamanda.tsv");

      Assert.AreEqual(3, peptides.Count);
      Assert.AreEqual(2, peptides[0].Peptides.Count);
      Assert.AreEqual("aSFIYR", peptides[0].Peptides[0].Sequence);
      Assert.AreEqual("fSAIYR", peptides[0].Peptides[1].Sequence);
      Assert.AreEqual("REVERSED_00007090/REVERSED_00026998 | tr|D3ZKM2/tr|F1M966/tr|F1LNY2/tr|F1LSM8/tr|F1LSC1", peptides[0].GetProteins(" | "));
      Assert.AreEqual("20110915_iTRAQ_4plex_GK_6ug_Exp_2.1962.1962.3.dta", peptides[0].Query.FileScan.LongFileName);
      Assert.AreEqual(1119.8, peptides[0].Query.RetentionTime);
      Assert.AreEqual("N-Term(itraq114 on nterm|144.105918|fixed)", peptides[0].Modifications);
    }
  }
}
