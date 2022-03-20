using NUnit.Framework;
using RCPA.Converter;

namespace RCPA
{
  [TestFixture]
  public class TestDoubleConverter
  {
    class TestItem
    {
      public double Dvalue { get; set; }
    }

    [Test]
    public void TestConvert()
    {
      var item = new TestItem()
      {
        Dvalue = -1
      };

      var conv = new DoubleConverter<TestItem>("Dvalue", "{0:0.0000}");
      conv.SetProperty(item, "100.5");
      Assert.AreEqual(100.5, item.Dvalue);
      Assert.AreEqual("100.5000", conv.GetProperty(item));
    }
  }
}
