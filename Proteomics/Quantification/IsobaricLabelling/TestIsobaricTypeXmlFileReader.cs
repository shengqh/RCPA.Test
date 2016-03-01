using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  [TestFixture]
  public class TestIsobaricTypeXmlFileReader
  {
    [Test]
    public void TestLoadFromFile()
    {
      var types = IsobaricTypeXmlFileReader.ReadFromFile(@"../../../data/isobaric.xml");
      Assert.AreEqual(5, types.Count);

      var tmt10 = types.First(m => m.Name.Equals("TMT10"));
      Assert.IsNotNull(tmt10);
      Assert.AreEqual(10, tmt10.Channels.Count);

      Assert.AreEqual("I126", tmt10.Channels[0].Name);
      Assert.AreEqual(94.25, tmt10.IsotopicTable[0, 0]);
      Assert.AreEqual(0, tmt10.IsotopicTable[0, 1]);
      Assert.AreEqual(5.75, tmt10.IsotopicTable[0, 2]);

      Assert.AreEqual("I131", tmt10.Channels[9].Name);
      Assert.AreEqual(0.19, tmt10.IsotopicTable[9, 5]);
      Assert.AreEqual(3.01, tmt10.IsotopicTable[9, 7]);
      Assert.AreEqual(94.16, tmt10.IsotopicTable[9, 9]);

      //for (int i = 0; i < tmt10.Channels.Count; i++)
      //{
      //  Console.Write(tmt10.Channels[i].Name);
      //  for (int j = 0; j < tmt10.Channels.Count; j++)
      //  {
      //    Console.Write("\t{0:0.##}", tmt10.IsotopicTable[i, j]);
      //  }
      //  Console.WriteLine();
      //}
    }
  }
}
