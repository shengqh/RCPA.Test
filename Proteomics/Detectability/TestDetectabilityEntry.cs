using NUnit.Framework;
using System.Collections.Generic;

namespace RCPA.Proteomics.Detectability
{
  [TestFixture]
  public class TestDetectabilityEntry
  {
    [Test]
    public void TestReadFromDirectory()
    {
      List<DetectabilityEntry> deList = DetectabilityEntry.ReadFromDirectory(@TestContext.CurrentContext.TestDirectory + "/../../../data//detectability");
      Assert.AreEqual(135, deList.Count);
      Assert.AreEqual("APHWTSASLTEAAAHPHSPEMK", deList[1].Peptide);
      Assert.AreEqual("IPI:IPI00206171.1|", deList[1].Protein);
      Assert.AreEqual(0.44827, deList[1].Detectability, 0.00001);
      Assert.AreEqual(10, deList[1].Position);

      //DetectabilityEntry.WriteToFile(TestContext.CurrentContext.TestDirectory + "/../../../data//IPI00206171.detectability",deList);
    }

    [Test]
    public void TestReadFromFile()
    {
      List<DetectabilityEntry> deList = DetectabilityEntry.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/IPI00206171.detectability");
      Assert.AreEqual(135, deList.Count);
      Assert.AreEqual("APHWTSASLTEAAAHPHSPEMK", deList[1].Peptide);
      Assert.AreEqual("IPI:IPI00206171.1|", deList[1].Protein);
      Assert.AreEqual(0.4483, deList[1].Detectability, 0.0001);
      Assert.AreEqual(10, deList[1].Position);
    }
  }
}