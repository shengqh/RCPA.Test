using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestListFileReader
  {
    [Test]
    public void TestReadFromFile()
    {
      List<string> actual = new ListFileReader().ReadFromFile(@"..\..\data\TestListFileReader.lst");
      Assert.AreEqual(5, actual.Count);
      Assert.AreEqual(@"Z:\Orbitrap\060222\Standard_Protein_FIT_060222", actual[0]);
    }
  }
}
