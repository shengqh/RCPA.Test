using NUnit.Framework;
using RCPA.Format;

namespace RCPA.Proteomics.Quantification.Srm
{
  [TestFixture]
  public class TestSrmTransactionAtaqsReader
  {
    string data = @TestContext.CurrentContext.TestDirectory + "/../../../data//ataqs.csv";

    [Test]
    public void Test()
    {
      var mrms = new SrmTransitionAtaqsReader().ReadFromFile(data);
      Assert.AreEqual(2000, mrms.Count);
      Assert.AreEqual("YBL100C", mrms[0].ObjectName);
      Assert.AreEqual("AAAAGLAALVELIR", mrms[0].PrecursorFormula);
      Assert.AreEqual(2, mrms[0].PrecursorCharge);
      Assert.AreEqual(669.91, mrms[0].PrecursorMZ, 0.01);
      Assert.AreEqual(1, mrms[0].ProductIonCharge);
      Assert.AreEqual(884.56, mrms[0].ProductIon, 0.01);
      Assert.AreEqual("y", mrms[0].IonType);
      Assert.AreEqual(8, mrms[0].IonIndex);
      Assert.AreEqual(43.16, mrms[0].ExpectCenterRetentionTime);
      Assert.AreEqual(34.98, mrms[0].CollisionEnergy);
    }

    [Test]
    public void Test2()
    {
      var formatFile = FileUtils.GetAssemblyPath() + "\\template\\ataqs.srmformat";
      var reader = new TextFileReader<SrmTransition>(formatFile);
      var mrms = reader.ReadFromFile(data);
      Assert.AreEqual(2000, mrms.Count);
      Assert.AreEqual("YBL100C", mrms[0].ObjectName);
      Assert.AreEqual("AAAAGLAALVELIR", mrms[0].PrecursorFormula);
      Assert.AreEqual(669.91, mrms[0].PrecursorMZ, 0.01);
      Assert.AreEqual(884.56, mrms[0].ProductIon, 0.01);
      Assert.AreEqual(43.16, mrms[0].ExpectCenterRetentionTime);
    }
  }
}
