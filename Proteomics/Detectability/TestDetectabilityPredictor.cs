using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace RCPA.Proteomics.Detectability
{
  [TestFixture]
  public class TestDetectabilityPredictor
  {
    //[Test]
    //public void TestProcess()
    //{
    //  IEnumerable<string> resultFiles = new DetectabilityPredictor("../../../ProteomicsLib/extends/PeptideDetectabilityPredictor.exe").Process("../../../data/detectability/fasta/IPI00206171.fasta");

    //  try
    //  {
    //    List<DetectabilityEntry> deList = DetectabilityEntry.ReadFromFile(resultFiles.First());
    //    Assert.AreEqual(135, deList.Count);
    //    Assert.AreEqual("APHWTSASLTEAAAHPHSPEMK", deList[1].Peptide);
    //    Assert.AreEqual("IPI:IPI00206171.1|", deList[1].Protein);
    //    Assert.AreEqual(0.4483, deList[1].Detectability, 0.0001);
    //    Assert.AreEqual(10, deList[1].Position);
    //  }
    //  finally
    //  {
    //    File.Delete(resultFiles.First());
    //  }
    //}
  }
}