using NUnit.Framework;

namespace RCPA
{
  [TestFixture]
  public class TestItemInfoListOldReader
  {
    [Test]
    public void TestReadFromFileConstruction1()
    {
      ItemInfoListOldReader reader = new ItemInfoListOldReader();
      ItemInfoList lst = reader.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/ListFileFormatOld.lst");
      Assert.AreEqual(2, lst.Count);

      Assert.AreEqual(@"Z:\Orbitrap\060222\Standard_Protein_FIT_060222", lst[0].SubItems[0]);
      Assert.AreEqual("SEQUEST", lst[0].SubItems[1]);
      Assert.AreEqual(false, lst[0].Selected);

      Assert.AreEqual(@"Z:\Orbitrap\060222\Standard_Protein_FIT_Lock", lst[1].SubItems[0]);
      Assert.AreEqual(true, lst[1].Selected);
    }

    [Test]
    public void TestReadFromFileConstruction2()
    {
      ItemInfoListOldReader reader = new ItemInfoListOldReader("DatFiles");
      ItemInfoList lst = reader.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/ListFileFormatOld.lst");
      Assert.AreEqual(1, lst.Count);

      Assert.AreEqual(@"D:\inetpub\mascot\data\20081223\F001237.dat", lst[0].SubItems[0]);
      Assert.AreEqual("20081204_F17_NoModif.mgf", lst[0].SubItems[1]);
      Assert.AreEqual("1", lst[0].SubItems[2]);
      Assert.AreEqual(false, lst[0].Selected);
    }
  }
}
