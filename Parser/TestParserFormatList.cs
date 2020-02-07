using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Windows.Forms;
using System.IO;

namespace RCPA.Parser
{
  [TestFixture]
  public class TestParserFormatList
  {
    string optionFile = @TestContext.CurrentContext.TestDirectory + "/../../../data//MiscOptions.xml";

    [Test]
    [ExpectedException(typeof(System.IO.FileNotFoundException))]
    public void TestReadFromFile_FileNotFoundException()
    {
      new ParserFormatList().ReadFromFile(null, null);
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestReadFromFile_ArgumentException()
    {
      new ParserFormatList().ReadFromFile(optionFile, null);
    }

    [Test]
    public void TestReadFromFile()
    {
      Console.WriteLine(Application.ExecutablePath);

      var list = new ParserFormatList();
      list.ReadFromFile(optionFile, "DatabaseParseDefinitions");

      Assert.AreEqual(4, list.Count);
      Assert.AreEqual("IPI", list[0].FormatName);
      Assert.AreEqual("33001", list[0].FormatId);
      Assert.AreEqual("IPI:IPI00022229.1|SWISS-PROT:P04114|...", list[0].Sample);

      Assert.AreEqual(1, list[0].Count);
      Assert.AreEqual("accessNumber", list[0][0].ItemName);
      Assert.AreEqual(@"(IPI\d+)", list[0][0].RegularExpression);
      Assert.AreEqual(1.0, list[0][0].Slope);
      Assert.AreEqual(0.0, list[0][0].Offset);
    }
  }
}
