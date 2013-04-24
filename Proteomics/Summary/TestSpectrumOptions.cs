using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestSpectrumOptions
  {
    [Test]
    public void Test()
    {
      PeptideFilterOptions option = new PeptideFilterOptions()
      {
         FilterBySequenceLength = true,  MinSequenceLength = 6
      };

      XElement root = new XElement("Root");
      option.Save(root);

      PeptideFilterOptions target = new PeptideFilterOptions();
      target.Load(root);

      Assert.AreEqual(option.FilterBySequenceLength, target.FilterBySequenceLength);
      Assert.AreEqual(option.MinSequenceLength, target.MinSequenceLength);
    }
  }
}
