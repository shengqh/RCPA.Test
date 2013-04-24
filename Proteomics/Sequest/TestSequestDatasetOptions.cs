using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestDatasetOptions
  {
    [Test]
    public void Test()
    {
      var option = new SequestDatasetOptions()
      {
         MinDeltaCn = 0.1,
         FilterByDeltaCn = true,  
         FilterByPrecursor = true, 
         FilterBySpRank = true, 
         FilterByXcorr = true, 
         PathNames = new string[]{"111","222"}.ToList(), 
         PrecursorPPMTolerance = 0.5, 
         MaxSpRank = 4, 
         MinXcorr1 = 1.9, 
         MinXcorr2 = 2.2, 
         MinXcorr3 = 3.75,
         SkipSamePeptideButDifferentModificationSite = true,
         MaxModificationDeltaCn = 0.08
      };

      XElement root = new XElement("Root");
      option.Save(root);

      //Console.WriteLine(root);

      var target = new SequestDatasetOptions();
      target.Load(root);

      Assert.AreEqual(option.MinDeltaCn, target.MinDeltaCn);
      Assert.AreEqual(option.FilterByDeltaCn, target.FilterByDeltaCn);
      Assert.AreEqual(option.FilterByPrecursor, target.FilterByPrecursor);
      Assert.AreEqual(option.FilterBySpRank, target.FilterBySpRank);
      Assert.AreEqual(option.FilterByXcorr, target.FilterByXcorr);
      Assert.AreEqual(option.PathNames.ToArray().ToString(), target.PathNames.ToArray().ToString());
      Assert.AreEqual(option.PrecursorPPMTolerance, target.PrecursorPPMTolerance);
      Assert.AreEqual(option.MaxSpRank, target.MaxSpRank);
      Assert.AreEqual(option.MinXcorr1, target.MinXcorr1);
      Assert.AreEqual(option.MinXcorr2, target.MinXcorr2);
      Assert.AreEqual(option.MinXcorr3, target.MinXcorr3);
    }
  }
}
