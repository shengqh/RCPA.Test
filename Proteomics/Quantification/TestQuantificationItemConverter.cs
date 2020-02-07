using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestQuantificationItemConverter
  {
    string datafile = @TestContext.CurrentContext.TestDirectory + "/../../../data//QuantificationItem.txt";

    [Test]
    public void TestRead()
    {
      var ir = new MascotResultTextFormat().ReadFromFile(datafile);
      Assert.AreEqual(true, ir[0][0].GetQuantificationItem().Enabled);
      Assert.AreEqual(8027277.1, ir[0][0].GetQuantificationItem().ReferenceIntensity, 0.1);
      Assert.AreEqual(303918.6, ir[0][0].GetQuantificationItem().SampleIntensity, 0.1);
      Assert.AreEqual(0.7830, ir[0][0].GetQuantificationItem().Correlation, 0.0001);
      Assert.AreEqual(0.0379, ir[0][0].GetQuantificationItem().Ratio, 0.0001);
    }

    [Test]
    public void TestWrite()
    {
      var oldFormat = new MascotResultTextFormat();
      var ir = oldFormat.ReadFromFile(datafile);

      oldFormat.WriteToFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//QuantificationItem.txt", ir);

      var format = new MascotResultTextFormat("\tReference\tPepCount\tUniquePepCount\tCoverPercent\tMW\tPI", oldFormat.PeptideFormat.GetHeader());
      format.InitializeByResult(ir);
      //format.WriteToFile(@TestContext.CurrentContext.TestDirectory + "/../../../data//QuantificationItem2.txt", ir);


      Assert.AreEqual(oldFormat.ProteinFormat.GetHeader(), format.ProteinFormat.GetHeader());
      Assert.AreEqual(oldFormat.PeptideFormat.GetHeader(), format.PeptideFormat.GetHeader());
    }

  }
}
