using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Isotopic
{
  [TestFixture]
  public class TestInterfernceOptimization
  {
    [Test]
    public void Test()
    {
      string[][] array = { new string[] { "张三", "李四", }, new string[] { "打", "拿", "亲" }, new string[] { "鱼", "猫", "美女" } };
      List<List<string>> lst = (from a in array select a.ToList()).ToList();

      List<List<string>> res = InterfernceOptimization.GenerateCandidates(lst);
      var resstrs = (from r in res select r.Merge("")).ToArray();

      Assert.AreEqual(18, resstrs.Length);
      Assert.AreEqual("张三打鱼", resstrs.First());
      Assert.AreEqual("李四亲美女", resstrs.Last());
    }
  }
}
