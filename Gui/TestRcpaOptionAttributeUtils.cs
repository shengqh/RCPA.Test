using NUnit.Framework;
using System.Xml.Linq;

namespace RCPA.Gui
{
  [TestFixture]
  public class TestRcpaOptionAttributeUtils
  {
    [RcpaOption("StrValue", RcpaOptionType.String)]
    public string StrValue { get; set; }

    [RcpaOption("IntValue", RcpaOptionType.Int32)]
    public int IntValue { get; set; }

    [RcpaOption("DoubleValue", RcpaOptionType.Double)]
    public double DoubleValue { get; set; }

    [RcpaOption("StringArrayValue", RcpaOptionType.StringArray)]
    public string[] StringValues { get; set; }

    [Test]
    public void Test()
    {
      var strs = new string[] { "Test1", "Test2" };
      this.StrValue = "TestValue";
      this.IntValue = 5;
      this.DoubleValue = 10.4;
      this.StringValues = strs;

      XElement a = new XElement("Root");

      RcpaOptionUtils.SaveToXml(this, a);
      //Console.WriteLine(a.ToString());

      this.StrValue = "";
      this.IntValue = 10;
      this.DoubleValue = 5.5;
      this.StringValues = null;

      RcpaOptionUtils.LoadFromXml(this, a);
      Assert.AreEqual("TestValue", this.StrValue);
      Assert.AreEqual(5, this.IntValue);
      Assert.AreEqual(10.4, this.DoubleValue);
      Assert.IsNotNull(this.StringValues);
      Assert.AreEqual(strs.Length, this.StringValues.Length);
      for (int i = 0; i < strs.Length; i++)
      {
        Assert.AreEqual(strs[i], this.StringValues[i]);
      }
    }
  }
}
