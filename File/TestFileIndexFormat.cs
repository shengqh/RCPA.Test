using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RCPA.Format
{
  [TestFixture]
  public class TestFileIndexFormat
  {
    public void TestReadWrite()
    {
      List<FileIndexItem> items = new List<FileIndexItem>();
      items.Add(new FileIndexItem(12, 54, "TEST1"));
      items.Add(new FileIndexItem(122, 776, "TEST2"));
      items.Add(new FileIndexItem(12483, 39484, "TEST3"));
      items.Add(new FileIndexItem(121111111111111, 38948392309484, "TEST4"));

      var format = new FileIndexFormat();
      var tmpFilename = TestContext.CurrentContext.TestDirectory + "/../../../data//fif.txt";
      format.WriteToFile(tmpFilename, items);

      var newitems = format.ReadFromFile(tmpFilename);
      CheckExtension.CheckEquals(items, newitems);

      File.Delete(tmpFilename);
    }
  }
}
