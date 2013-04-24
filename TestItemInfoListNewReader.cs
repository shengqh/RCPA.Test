using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA
{
  [TestFixture]
  public class TestItemInfoListNewReader
  {
    [Test]
    public void TestReadFromFile()
    {
      ItemInfoListNewReader reader = new ItemInfoListNewReader("Items");
      ItemInfoList lst = reader.ReadFromFile(@"..\..\data\ListFileFormatNew.lst");
      Assert.AreEqual(2, lst.Count);
      
      Assert.AreEqual(@"Z:\GK_PPN\GKPPN_iTRAQ_SAX_114115_AP_treat\GKPPN_114_115AP+_500ug_SAX_1", lst[0].SubItems[0]);
      Assert.AreEqual("SEQUEST", lst[0].SubItems[1]);
      Assert.AreEqual(true, lst[0].Selected);

      Assert.AreEqual(@"Z:\GK_PPN\GKPPN_iTRAQ_SAX_114115_AP_treat\GKPPN_114_115AP+_500ug_SAX_2", lst[1].SubItems[0]);
      Assert.AreEqual(false, lst[1].Selected);
    }
  }
}
