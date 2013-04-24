using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace RCPA
{
  [TestFixture]
  public class TestXmlLinqExtension
  {
    [Test]
    public void TestGetChildValue_SpecialCharacter()
    {
      var expect = "D<V>VC&PD IC";
      var ele = new XElement("Root", new XElement("Test", expect, new XAttribute("attr",3.4), new XElement("Another", 5)));
      var value = ele.GetChildValue("Test");
      Assert.AreEqual(expect, value);
    }

    [Test]
    public void TestGetChildValue_Empty()
    {
      var ele = new XElement("Root", new XElement("Test", new XAttribute("attr", 3.4), new XElement("Another", 5)));
      var value = ele.GetChildValue("Test");
      Assert.AreEqual(string.Empty, value);
    }
  }
}
