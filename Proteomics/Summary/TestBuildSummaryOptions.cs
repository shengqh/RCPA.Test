using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestBuildSummaryOptions
  {
    [Test]
    public void Test()
    {
      var option = new BuildSummaryOptions();
      option.LoadFromFile("../../data/BuildSummary.param");
    }
  }
}
