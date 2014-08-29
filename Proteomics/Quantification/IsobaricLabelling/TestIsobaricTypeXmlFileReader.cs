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
      var types = IsobaricTypeXmlFileReader.ReadFromFile(@"../../data/isobaric.xml");
      Assert.AreEqual(4, types.Count);

      var tmt10 = types.First(m => m.Name.Equals("TMT10"));
      Assert.IsNotNull(tmt10);

      for (int i = 0; i < tmt10.Channels.Count; i++)
      {
        Console.Write(tmt10.Channels[i].Name);
        for (int j = 0; j < tmt10.Channels.Count; j++)
        {
          Console.Write("\t{0:0.##}", tmt10.IsotopicTable[i, j]);
        }
        Console.WriteLine();
      }
    }
  }
}
