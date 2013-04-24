using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Format
{
  [TestFixture]
  public class TestTextFileReader
  {
    class TextItem
    {
      public double PropName1 { get; set; }
      public int PropName2 { get; set; }
      public string PropName3 { get; set; }
    }

    [Test]
    public void TestRead()
    {
      var reader = new TextFileReader<TextItem>(TestTextFileDefinition.DefinitionFile);
      var items = reader.ReadFromFile(@"..\..\data\TextFile.txt");
      Assert.AreEqual(2, items.Count);
      Assert.AreEqual(2.1, items[0].PropName1);
      Assert.AreEqual(1, items[0].PropName2);
      Assert.AreEqual("hahaha", items[0].PropName3);
      Assert.AreEqual(3.1, items[1].PropName1);
      Assert.AreEqual(2, items[1].PropName2);
      Assert.AreEqual("lalala", items[1].PropName3);
    }
  }
}
