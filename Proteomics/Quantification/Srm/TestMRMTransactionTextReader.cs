using NUnit.Framework;

namespace RCPA.Proteomics.Quantification.Srm
{
  [TestFixture]
  public class TestMRMTransactionTextReader
  {
    [Test]
    public void Test()
    {
      var data = @TestContext.CurrentContext.TestDirectory + "/../../../data//MRMDefinition.txt";

      var mrms = new SrmTransitionAgilentFormat().ReadFromFile(data);
      Assert.AreEqual(437, mrms.Count);
    }
  }
}
