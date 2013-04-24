using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using System.Windows.Forms;

namespace RCPA.Gui
{
  [TestFixture]
  public class TestOptionFileTextBoxAdaptor
  {
    private XElement item;

    [SetUp]
    public void SetUp()
    {
      item = new XElement("ROOT", new XElement("KEY1", "abc"));
    }

    [Test]
    public void TestLoadFromXml()
    {
      TextBox box = new TextBox ();

      OptionFileTextBoxAdaptor adaptor = new OptionFileTextBoxAdaptor(box, "KEY1", "111");
      adaptor.LoadFromXml(item);
      Assert.AreEqual("abc", box.Text);

      OptionFileTextBoxAdaptor adaptor2 = new OptionFileTextBoxAdaptor(box, "KEY2", "111");
      adaptor2.LoadFromXml(item);
      Assert.AreEqual("111", box.Text);
    }

    [Test]
    public void TestRemoveFromXml()
    {
      TextBox box = new TextBox();

      OptionFileTextBoxAdaptor adaptor2 = new OptionFileTextBoxAdaptor(box, "KEY2", "111");
      adaptor2.RemoveFromXml(item);
      Assert.AreEqual(1, item.Descendants().Count());

      OptionFileTextBoxAdaptor adaptor = new OptionFileTextBoxAdaptor(box, "KEY1", "111");
      adaptor.RemoveFromXml(item);
      Assert.AreEqual(0, item.Descendants().Count());
    }

    [Test]
    public void TestSaveToXml()
    {
      TextBox box = new TextBox();
      box.Text = "AAA";
      
      OptionFileTextBoxAdaptor adaptor2 = new OptionFileTextBoxAdaptor(box, "KEY2", "111");
      adaptor2.SaveToXml(item);

      Assert.AreEqual("AAA", item.Descendants("KEY2").First().Value);
    }
  }
}
