using NUnit.Framework;
using System.IO;
using System.Text;
using System.Xml;

namespace RCPA
{
  [TestFixture]
  public class TestXmlExtension
  {
    [Test]
    public void TestWrite()
    {
      var ms = new MemoryStream();
      XmlWriterSettings setting = new XmlWriterSettings()
      {
        Encoding = Encoding.ASCII,
        Indent = false
      };

      using (var xw = XmlTextWriter.Create(ms, setting))
      {
        xw.WriteStartDocument();
        xw.WriteStartElement("Ions");
        xw.WriteElement("I114", 114.5);
        xw.WriteElement("I115", 115.5);
        xw.WriteEndElement();
        xw.WriteEndDocument();
      }

      var actualXml = Encoding.ASCII.GetString(ms.ToArray());

      //Console.WriteLine(actualXml);

      Assert.AreEqual("<?xml version=\"1.0\" encoding=\"us-ascii\"?><Ions><I114>114.5</I114><I115>115.5</I115></Ions>", actualXml);

      ms.Position = 0;
      using (var xr = XmlTextReader.Create(ms))
      {
        xr.ReadStartElement("Ions");

        Assert.AreEqual(114.5, xr.ReadElementAsDouble("I114"));
        Assert.AreEqual(115.5, xr.ReadElementAsDouble("I115"));

        xr.ReadEndElement();
      }
    }
  }
}
