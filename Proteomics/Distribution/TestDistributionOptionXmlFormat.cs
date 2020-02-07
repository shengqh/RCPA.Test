using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using RCPA.Utils;

namespace RCPA.Proteomics.Distribution
{
  [TestFixture]
  public class TestDistributionOptionXmlFormat
  {
    private string filePath = @TestContext.CurrentContext.TestDirectory + "/../../../data//Distribution.statistic.xml";
    [Test]
    public void TestLoad()
    {
      DistributionOptionXmlFormat format = new DistributionOptionXmlFormat();

      var actual = format.ReadFromFile(filePath);

      Assert.AreEqual(@"\\192.168.88.248\work\NieSong\YEAST_SAX_ONLINE\buildsummary_G1_S_G2_4\G1_S_G2_4_merged.noredundant", actual.SourceFileName);

      Assert.AreEqual(DistributionType.Protein, actual.DistributionType);

      Assert.AreEqual("ABUNDANCE", actual.ClassificationPrinciple);

      Assert.AreEqual(PeptideFilterType.PeptideCount, actual.FilterType);

      Assert.AreEqual(1, actual.FilterFrom);

      Assert.AreEqual(10, actual.FilterTo);

      Assert.AreEqual(2, actual.FilterStep);

      Assert.AreEqual(false, actual.ModifiedPeptideOnly);

      Assert.AreEqual("STY", actual.ModifiedPeptide);

      Assert.AreEqual(3, actual.ClassificationSet.Count);

      Assert.AreEqual(new string[] { "G1", "G2", "S" }, actual.ClassificationSet.Keys.ToArray());

      Assert.AreEqual(36, actual.ClassificationSet["G1"].Count);
      Assert.AreEqual("YEAST_0610_G1_SAX_080811_01", actual.ClassificationSet["G1"][0]);

      Assert.IsTrue(actual.ClassifiedByTag);
    }

    [Test]
    public void TestSave()
    {
      DistributionOptionXmlFormat format = new DistributionOptionXmlFormat();

      var actual = format.ReadFromFile(filePath);

      string tmpFile = filePath + ".tmp";

      format.WriteToFile(tmpFile, actual);

      AssertUtils.AssertFileEqual(tmpFile, filePath);

      File.Delete(tmpFile);
    }
  }
}
