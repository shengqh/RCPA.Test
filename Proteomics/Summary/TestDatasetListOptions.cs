using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using System.Xml.Linq;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestDatasetListOptions
  {
    [Test]
    public void Test()
    {
      var options = new DatasetListOptions();
      options.Add(new MascotDatasetOptions()
      {
        PathNames = new string[] { "111", "222" }.ToList(),
        FilterByPrecursor = true,
        PrecursorPPMTolerance = 0.5,
        FilterByExpectValue = true,
        FilterByScore = true,
        MinScore = 25,
        MaxExpectValue = 0.01
      });

      options.Add(new SequestDatasetOptions()
      {
        MinDeltaCn = 0.1,
        FilterByDeltaCn = true,
        FilterByPrecursor = true,
        FilterBySpRank = true,
        FilterByXcorr = true,
        PathNames = new string[] { "333", "444" }.ToList(),
        PrecursorPPMTolerance = 0.5,
        MaxSpRank = 4,
        MinXcorr1 = 1.9,
        MinXcorr2 = 2.2,
        MinXcorr3 = 3.75,
        SkipSamePeptideButDifferentModificationSite = true,
        MaxModificationDeltaCn = 0.08
      });

      XElement root = new XElement("Root");
      options.Save(root);

      Console.WriteLine(root);

      var target = new DatasetListOptions();
      target.Load(root);

      Assert.AreEqual(options[0].SearchEngine, target[0].SearchEngine);
      Assert.AreEqual(options[1].SearchEngine, target[1].SearchEngine);
    }
  }
}
