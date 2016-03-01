using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Xml;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestXmlHelper
  {
    private XmlDocument doc;
    private XmlHelper helper;

    [TestFixtureSetUp]
    public void SetUp()
    {
      doc = new XmlDocument();
      doc.Load(@"../../../data/pepxml.xml");

      helper = new XmlHelper(doc);
    }

    [Test]
    public void TestGetFirstChildByXPath()
    {
      Assert.IsNotNull(helper.GetFirstChildByXPath(doc.DocumentElement, helper.PREFIX + ":msms_run_summary"));
      Assert.IsNull(helper.GetFirstChildByXPath(doc.DocumentElement, helper.PREFIX + ":msms_run_summary_1"));
    }

    [Test]
    public void TestGetChildrenByXPath()
    {
      string xpath = MyConvert.Format("{0}:msms_run_summary/{0}:search_summary/{0}:parameter", helper.PREFIX);
      List<XmlNode> nodes = helper.GetChildrenByXPath(doc.DocumentElement, xpath);
      Assert.AreEqual(13, nodes.Count);
    }

    [Test]
    public void TestGetFirstChild()
    {
      Assert.IsNotNull(helper.GetFirstChild(doc.DocumentElement, "msms_run_summary"));
      Assert.IsNull(helper.GetFirstChild(doc.DocumentElement, "msms_run_summary_1"));
    }

    [Test]
    public void TestHasChild()
    {
      Assert.IsTrue(helper.HasChild(doc.DocumentElement, "analysis_summary"));
      Assert.IsFalse(helper.HasChild(doc.DocumentElement, "analysis_summary_1"));
    }

    [Test]
    public void TestGetChildrenByName()
    {
      List<XmlNode> nodes = helper.GetChildren(doc.DocumentElement, "msms_run_summary");
      Assert.AreEqual(1, nodes.Count);

      List<XmlNode> spectra = helper.GetChildren(doc.DocumentElement, "msms_run_summary/spectrum_query");
      Assert.AreEqual(2, spectra.Count);
    }

    [Test]
    public void TestGetFirstChildByNameAndAttribute()
    {
      Assert.IsNotNull(helper.GetFirstChildByNameAndAttribute(doc.DocumentElement, "msms_run_summary", "raw_data_type", "raw"));
      Assert.IsNull(helper.GetFirstChildByNameAndAttribute(doc.DocumentElement, "msms_run_summary", "raw_data_type", "raw1"));
      Assert.IsNull(helper.GetFirstChildByNameAndAttribute(doc.DocumentElement, "msms_run_summary_1", "raw_data_type", "raw"));
      Assert.IsNull(helper.GetFirstChildByNameAndAttribute(doc.DocumentElement, "msms_run_summary", "raw_data_type_1", "raw"));
    }

    [Test]
    public void TestGetChildrenByNameAndAttribute()
    {
      List<XmlNode> nodes = helper.GetChildrenByNameAndAttribute(doc.DocumentElement, "msms_run_summary", "raw_data_type", "raw");
      Assert.AreEqual(1, nodes.Count);

      List<XmlNode> spectraCharge1 = helper.GetChildrenByNameAndAttribute(nodes[0], "spectrum_query", "assumed_charge", "1");
      Assert.AreEqual(2, spectraCharge1.Count);

      List<XmlNode> spectraIndex1 = helper.GetChildrenByNameAndAttribute(nodes[0], "spectrum_query", "index", "1");
      Assert.AreEqual(1, spectraIndex1.Count);
    }

    [Test]
    public void TestGetChildValue1()
    {
      Assert.AreEqual("test", helper.GetChildValue(doc.DocumentElement, "test_value"));
    }

    [Test]
    [ExpectedException("System.ArgumentException")]
    public void TestGetChildValue1Exception()
    {
      helper.GetChildValue(doc.DocumentElement, "FakeChild");
    }

    [Test]
    public void TestGetChildValue2()
    {
      Assert.IsFalse(helper.HasChild(doc.DocumentElement, "FakeChild"));
      Assert.AreEqual("test", helper.GetChildValue(doc.DocumentElement, "FakeChild", "test"));
      Assert.AreEqual("test", helper.GetChildValue(doc.DocumentElement, "test_value", "test_2"));
    }

    [Test]
    public void TestGetValidChild()
    {
      Assert.IsNotNull(helper.GetValidChild(doc.DocumentElement, "msms_run_summary"));
    }

    [Test]
    [ExpectedException("System.ArgumentException")]
    public void TestGetValidChildException()
    {
      helper.GetValidChild(doc.DocumentElement, "FakeChild");
    }

    [Test]
    public void TestNamespaceManager()
    {
      XmlNode node = helper.GetFirstChild(doc.DocumentElement, "msms_run_summary");

      Assert.IsNull(doc.DocumentElement.SelectSingleNode("msms_run_summary"));

      Assert.IsNotNull(doc.DocumentElement.SelectSingleNode(helper.PREFIX + ":msms_run_summary",helper.NamespaceManager));
    }

  }
}
