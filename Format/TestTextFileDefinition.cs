using NUnit.Framework;
using RCPA.Utils;
using System.IO;

namespace RCPA.Format
{
  [TestFixture]
  public class TestTextFileDefinition
  {
    public static string DefinitionFile = @TestContext.CurrentContext.TestDirectory + "/../../../data//TextFileDefinition.xml";

    [Test]
    public void TestWrite()
    {
      TextFileDefinition def = new TextFileDefinition();
      def.Delimiter = '\t';
      def.Description = "Test File ...";
      def.Add(new FileDefinitionItem()
      {
        AnnotationName = "AnnoKey1",
        PropertyName = "PropName1",
        ValueType = "double",
        Format = "{0:0.0000}"
      });
      def.Add(new FileDefinitionItem()
      {
        AnnotationName = "AnnoKey2",
        PropertyName = "PropName2",
        ValueType = "int"
      });
      def.Add(new FileDefinitionItem()
      {
        AnnotationName = "AnnoKey3",
        PropertyName = "PropName3",
        ValueType = "string"
      });

      var newfile = @TestContext.CurrentContext.TestDirectory + "/../../../data//TextFileDefinition2.xml";
      def.WriteToFile(newfile);

      AssertUtils.AssertFileEqual(DefinitionFile, newfile);
      File.Delete(newfile);
    }

    [Test]
    public void TestRead()
    {
      TextFileDefinition def = new TextFileDefinition();
      def.ReadFromFile(DefinitionFile);
      Assert.AreEqual('\t', def.Delimiter);
      Assert.AreEqual("Test File ...", def.Description);
      Assert.AreEqual(3, def.Count);
      Assert.AreEqual("AnnoKey1", def[0].AnnotationName);
      Assert.AreEqual("PropName1", def[0].PropertyName);
      Assert.AreEqual("double", def[0].ValueType);
      Assert.AreEqual("{0:0.0000}", def[0].Format);
      Assert.AreEqual("AnnoKey2", def[1].AnnotationName);
      Assert.AreEqual("PropName2", def[1].PropertyName);
      Assert.AreEqual("int", def[1].ValueType);
      Assert.AreEqual("AnnoKey3", def[2].AnnotationName);
      Assert.AreEqual("PropName3", def[2].PropertyName);
      Assert.AreEqual("string", def[2].ValueType);
    }
  }
}
