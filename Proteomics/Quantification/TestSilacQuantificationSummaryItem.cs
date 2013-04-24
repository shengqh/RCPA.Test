using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Quantification.SILAC;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestSilacQuantificationSummaryItem
  {
    /*
    [Test]
    public void Test()
    {
      string file = @"..\..\data\sampF1.IGESLADPVK.2.2754-2939.silac";
      SilacQuantificationSummaryItem item = new SilacQuantificationSummaryItemXmlFormat().ReadFromFile(file);

      Console.WriteLine("Before validation");
      item.ObservedEnvelopes.FindAll(m => m.Enabled).ForEach(m => Console.WriteLine(m.Scan));

      item.ValidateScans();

      Console.WriteLine("After validation");
      item.ObservedEnvelopes.FindAll(m => m.Enabled).ForEach(m => Console.WriteLine(m.Scan));

    }
     */ 
  }
}
