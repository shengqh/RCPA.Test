using NUnit.Framework;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Gui
{
  [TestFixture]
  public class TestOptionFileItemInfosAdaptor
  {
    [Test]
    public void Test()
    {
      SimpleItemInfos infos = new SimpleItemInfos();

      infos.Items = new ItemInfoList();
      infos.Items.Add(new ItemInfo() { Selected = true, SubItems = new[] { "Item1", "SubItem1" }.ToList() });
      infos.Items.Add(new ItemInfo() { Selected = false, SubItems = new[] { "Item2", "SubItem2" }.ToList() });

      XElement root = new XElement("Root");

      OptionFileItemInfosAdaptor adaptor = new OptionFileItemInfosAdaptor(infos, "TEST");
      adaptor.SaveToXml(root);

      SimpleItemInfos readInfos = new SimpleItemInfos();
      OptionFileItemInfosAdaptor readAdaptor = new OptionFileItemInfosAdaptor(readInfos, "TEST");
      readAdaptor.LoadFromXml(root);

      Assert.AreEqual(readInfos.Items.Count, infos.Items.Count);

      for (int i = 0; i < readInfos.Items.Count; i++)
      {
        Assert.AreEqual(readInfos.Items[i].Selected, infos.Items[i].Selected);
        Assert.AreEqual(readInfos.Items[i].SubItems, infos.Items[i].SubItems);
      }
    }
  }
}
