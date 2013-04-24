using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestSequestDatasetOptions
  {
    [Test]
    public void Test()
    {
      var option = new MascotDatasetOptions()
      {
        PathNames = new string[] { "111", "222" }.ToList(),
        FilterByPrecursor = true,
        PrecursorPPMTolerance = 0.5,
        FilterByExpectValue = true,
        FilterByScore = true,
        MinScore = 25,
        MaxExpectValue = 0.01
      };

      XElement root = new XElement("Root");
      option.Save(root);

      //Console.WriteLine(root);

      var target = new MascotDatasetOptions();
      target.Load(root);

      Assert.AreEqual(option.PathNames.ToArray().ToString(), target.PathNames.ToArray().ToString());
      Assert.AreEqual(option.FilterByPrecursor, target.FilterByPrecursor);
      Assert.AreEqual(option.PrecursorPPMTolerance, target.PrecursorPPMTolerance);
      Assert.AreEqual(option.FilterByExpectValue, target.FilterByExpectValue);
      Assert.AreEqual(option.FilterByScore, target.FilterByScore);
      Assert.AreEqual(option.MinScore, target.MinScore);
      Assert.AreEqual(option.MaxExpectValue, target.MaxExpectValue);
    }
  }
}
