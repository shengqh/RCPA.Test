using NUnit.Framework;
using System.Collections.Generic;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestListFileReader
  {
    [Test]
    public void TestReadFromFile()
    {
      List<string> actual = new ListFileReader().ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/TestListFileReader.lst");
      Assert.AreEqual(5, actual.Count);
      Assert.AreEqual(@"Z:\Orbitrap\060222\Standard_Protein_FIT_060222", actual[0]);
    }
  }
}
