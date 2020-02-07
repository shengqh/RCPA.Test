using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using RCPA.Format;

namespace RCPA.Proteomics.Quantification.ITraq
{
  [TestFixture]
  public class TestITraqResultXmlIndexBuilder
  {
    string dataFile = TestContext.CurrentContext.TestDirectory + "/../../../data//ITraqResult.xml";
    string expectFile = TestContext.CurrentContext.TestDirectory + "/../../../data//ITraqResult.xml.expect.index";

    public void TestBuild()
    {
      var indexFile = new ITraqResultXmlIndexBuilder(true).Process(dataFile).First();

      FileAssert.AreEqual(expectFile, indexFile);

      File.Delete(indexFile);
    }

    //[Test]
    public void MyTestFile()
    {
      var items = new FileIndexFormat().ReadFromFile(expectFile);
      using (FileStream fs = new FileStream(dataFile, FileMode.Open))
      {
        for (int i = 0; i < items.Count; i++)
        {
          fs.Position = items[i].StartPosition;
          var bytes = new byte[items[i].Length];
          fs.Read(bytes, 0, (int)items[i].Length);
          string str = Encoding.ASCII.GetString(bytes);
          Console.WriteLine(str);
        }
      }
    }
  }
}
