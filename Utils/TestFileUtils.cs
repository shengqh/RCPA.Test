using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestFileUtils
  {
    [Test]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void TestChangeExtensionException()
    {
      FileUtils.ChangeExtension("", null);
    }

    [Test]
    public void TestChangeExtension()
    {
      string oldFilename = @"d:\dd\dddddd.cccc.eeee";
      string newFilename;

      newFilename = FileUtils.ChangeExtension(oldFilename, "ffff");
      Assert.AreEqual(@"d:\dd\dddddd.cccc.ffff", newFilename);

      newFilename = FileUtils.ChangeExtension(oldFilename, ".ffff");
      Assert.AreEqual(@"d:\dd\dddddd.cccc.ffff", newFilename);

      oldFilename = @"d:\dd\dddddd";
      newFilename = FileUtils.ChangeExtension(oldFilename, ".ffff");
      Assert.AreEqual(@"d:\dd\dddddd.ffff", newFilename);

      Assert.AreEqual(@"/dd/dddddd.ffff", FileUtils.ChangeExtension(@"/dd/dddddd.ddd", ".ffff"));
    }

    [Test]
    public void TestRemoveExtension()
    {
      Assert.AreEqual(@"d:\dd.aaa\dddddd", FileUtils.RemoveAllExtension(@"d:\dd.aaa\dddddd.cccc.eeee"));
      Assert.AreEqual("/dd.aaa/dddddd", FileUtils.RemoveAllExtension("/dd.aaa/dddddd.cccc.eeee"));
      Assert.AreEqual("/dd.aaa/dddddd", FileUtils.RemoveAllExtension("/dd.aaa/dddddd"));
      Assert.AreEqual("dddddd", FileUtils.RemoveAllExtension("dddddd.cccc.eeee"));
    }

    [Test]
    public void TestGetFileName()
    {
      Assert.AreEqual("dddddd.cccc.eeee", FileUtils.GetFileName(@"d:\dd.aaa\dddddd.cccc.eeee"));
      Assert.AreEqual("dddddd.cccc.eeee", FileUtils.GetFileName("/dd.aaa/dddddd.cccc.eeee"));
      Assert.AreEqual("dddddd.cccc.eeee", FileUtils.GetFileName("dddddd.cccc.eeee"));
    }
  }
}
