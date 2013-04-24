using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace RCPA
{
  [TestFixture]
  public class TestOptionFileItemInfosAdaptor
  {
    [Test]
    public void TestReadFromNewVersion()
    {
      SimpleItemInfos infos = new SimpleItemInfos();

      OptionFileItemInfosAdaptor adaptor = new OptionFileItemInfosAdaptor(infos, "Items");
      XElement option = XElement.Load(@"..\..\data\ListFileFormatNew.lst");
      adaptor.LoadFromXml(option);
      Assert.AreEqual(2, infos.Items.Count);
    }

    [Test]
    public void TestReadFromOldVersion()
    {
      SimpleItemInfos infos = new SimpleItemInfos();

      var adaptor = new OptionFileItemInfosAdaptor(infos, "DatFiles");
      var option = XElement.Load(@"..\..\data\ListFileFormatOld.lst", LoadOptions.SetBaseUri);
      adaptor.LoadFromXml(option);
      Assert.AreEqual(1, infos.Items.Count);


    }
  }
}
