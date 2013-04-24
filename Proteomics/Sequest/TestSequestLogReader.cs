using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestLogReader
  {
    [Test]
    public void Test()
    {
      Dictionary<string, int> counts = new SequestLogReader().ReadFromFile("../../data/cluster_sequest.log");

      Dictionary<string, int> expectCounts = new Dictionary<string, int>();
      expectCounts["dell01"] = 582;
      expectCounts["dell02"] = 580;
      expectCounts["dell03"] = 588;
      expectCounts["dell04"] = 583;
      expectCounts["dell05"] = 589;
      expectCounts["dell06"] = 585;
      expectCounts["dell07"] = 588;
      expectCounts["dell08"] = 583;
      expectCounts["manager"] = 285;
      expectCounts["sun02"] = 566;
      expectCounts["sun03"] = 574;
      expectCounts["sun04"] = 569;
      expectCounts["sun05"] = 565;
      expectCounts["sun06"] = 567;
      expectCounts["sun07"] = 564;

      foreach (var key in counts.Keys)
      {
        Assert.AreEqual(expectCounts[key], counts[key]);
      }
    }
  }
}
