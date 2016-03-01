using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RCPA.Proteomics.Quantification.Reproducibility
{
  [TestFixture]
  public class TestPrecursorItemListExcelReader
  {
    [Test]
    public void Test()
    {
      string file = @"../../../data/massprofiler.xls";
      file = new FileInfo(file).FullName;

      PrecursorItemListExcelReader reader = new PrecursorItemListExcelReader();
      var list = reader.ReadFromFile(file);

      Assert.AreEqual(162, list.Count);
      Assert.AreEqual(4.869, list[0].RetentionTime, 0.000);
      Assert.AreEqual(1019.4944, list[0].Mz, 0.0000);
      Assert.AreEqual(37842.86, list[0].Abundance, 0.00);
    }
  }
}
