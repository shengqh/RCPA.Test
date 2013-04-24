using System.IO;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotResultTextFormat
  {
    [Test]
    public void TestWrite()
    {
      var format = new MascotResultTextFormat();
      string oldFile = "../../data/mascot_summary.txt";
      IIdentifiedResult mr = format.ReadFromFile(oldFile);
      string dupFile = oldFile + ".dup";
      format.WriteToFile(dupFile, mr);
      FileAssert.AreEqual(oldFile, dupFile);
      new FileInfo(dupFile).Delete();
    }
  }
}