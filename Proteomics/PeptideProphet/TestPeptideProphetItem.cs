using System.Collections.Generic;
using NUnit.Framework;

namespace RCPA.Proteomics.PeptideProphet
{
  [TestFixture]
  public class TestPeptideProphetItem
  {
    [Test]
    public void TestSort()
    {
      var items = new List<SequestPeptideProphetItem>();

      var item1 = new SequestPeptideProphetItem();
      item1.PeptideProphetProbability = 0.1;
      items.Add(item1);

      var item2 = new SequestPeptideProphetItem();
      item2.PeptideProphetProbability = 0.2;
      items.Add(item2);

      items.Sort();

      Assert.AreEqual(0.2, items[0].PeptideProphetProbability);
      Assert.AreEqual(0.1, items[1].PeptideProphetProbability);
    }
  }
}