using NUnit.Framework;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RCPA.Gui
{
  [TestFixture]
  public class TestOptionFileComboBoxAdaptor
  {
    private XElement item;

    private ComboBox box;

    [SetUp]
    public void SetUp()
    {
      item = new XElement("ROOT", new XElement("KEY1", "2"));

      box = new ComboBox();
      box.Items.Add("1");
      box.Items.Add("2");
      box.Items.Add("3");
      box.SelectedIndex = 1;
    }

    [Test]
    public void TestLoadFromXml()
    {

      OptionFileComboBoxAdaptor adaptor = new OptionFileComboBoxAdaptor(box, "KEY1", 0);
      adaptor.LoadFromXml(item);
      Assert.AreEqual(2, box.SelectedIndex);

      OptionFileComboBoxAdaptor adaptor2 = new OptionFileComboBoxAdaptor(box, "KEY2", 0);
      adaptor2.LoadFromXml(item);
      Assert.AreEqual(0, box.SelectedIndex);
    }

    [Test]
    public void TestRemoveFromXml()
    {
      OptionFileComboBoxAdaptor adaptor2 = new OptionFileComboBoxAdaptor(box, "KEY2", 0);
      adaptor2.RemoveFromXml(item);
      Assert.AreEqual(1, item.Descendants().Count());

      OptionFileComboBoxAdaptor adaptor = new OptionFileComboBoxAdaptor(box, "KEY1", 0);
      adaptor.RemoveFromXml(item);
      Assert.AreEqual(0, item.Descendants().Count());
    }

    [Test]
    public void TestSaveToXml()
    {
      OptionFileComboBoxAdaptor adaptor2 = new OptionFileComboBoxAdaptor(box, "KEY2", 0);
      adaptor2.SaveToXml(item);
      Assert.AreEqual("1", item.Descendants("KEY2").First().Value);
    }
  }
}
