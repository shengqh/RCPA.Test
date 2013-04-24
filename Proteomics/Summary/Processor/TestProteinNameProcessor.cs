using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Summary.Processor
{
  [TestFixture]
  public class TestProteinNameProcessor
  {
    [Test]
    public void TestProcess()
    {
      IIdentifiedProteinGroup group = new IdentifiedProteinGroup();
      group.Add(new IdentifiedProtein("BBBCCC"));
      group.Add(new IdentifiedProtein("AAABBB"));

      IIdentifiedProteinGroup finalGroup;
      
      ///两者都有，两个都保留
      finalGroup = new ProteinNameProcessor(new string[]{"BBB"}).Process(group);
      Assert.AreEqual(2, finalGroup.Count);

      ///两者都没有，两个都保留
      finalGroup = new ProteinNameProcessor(new string[] { "DDD" }).Process(group);
      Assert.AreEqual(2, finalGroup.Count);

      ///只有一个有，保留这一个
      finalGroup = new ProteinNameProcessor(new string[] { "AAA" }).Process(group);
      Assert.AreEqual(1, finalGroup.Count);
      Assert.AreEqual("AAABBB", finalGroup[0].Name);
    }
  }
}
