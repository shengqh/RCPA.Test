using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestChinese
  {
    [Test]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void TestGetSpellsException()
    {
      Chinese.GetSpells(null);
    }

    [Test]
    public void TestGetSpells()
    {
      Assert.AreEqual("", Chinese.GetSpells(""));
      Assert.AreEqual("ZHRMGHAG123ABC", Chinese.GetSpells("中华人民共和A国123ABC"));
      Assert.AreEqual("ZHRMGHG", Chinese.GetSpells("中华人民（共和国）"));
    }
  }
}
