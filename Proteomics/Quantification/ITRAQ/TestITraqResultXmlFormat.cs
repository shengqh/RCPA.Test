using System.Linq;
using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.IO;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Quantification.ITraq
{
  [TestFixture]
  public class TestITraqResultXmlFormat
  {
    private IsobaricItem t1;
    private IsobaricItem t2;
    private IsobaricResult tr;

    public void Setup()
    {
      t1 = new IsobaricItem()
      {
        Experimental = "S1",
        ScanMode = "HCD",
        PlexType = IsobaricType.PLEX4,
        PrecursorPercentage = 0.85,
        Scan = new ScanTime(255, 3.4),
        RawPeaks = new PeakList<Peak>(new Peak[] { new Peak(114.1, 114.1), new Peak(115.1, 115.1), new Peak(116.1, 116.1), new Peak(117.1, 117.1) }.ToList()),
        PeakInIsolationWindow = new PeakList<Peak>(new Peak[] { new Peak(214.1, 114.1), new Peak(215.1, 115.1), new Peak(216.1, 116.1), new Peak(217.1, 117.1) }.ToList())
        {
          Precursor = new PrecursorPeak()
          {
            MasterScan = 1,
            Charge = 2,
            Intensity = 3.0,
            IsolationMass = 1800.1,
            IsolationWidth = 2.0,
            MonoIsotopicMass = 1879.1
          }
        }
      };

      t1[114] = 4.5;
      t1[115] = 5.5;
      t1[116] = 6.5;
      t1[117] = 7.5;

      t2 = new IsobaricItem()
      {
        Experimental = "S2",
        ScanMode = "ETD",
        PlexType = IsobaricType.PLEX8,
        PrecursorPercentage = 0.33,
        Scan = new ScanTime(355, 4.4),
        RawPeaks = new PeakList<Peak>(new Peak[] { new Peak(114.1, 1114.1), new Peak(115.1, 1115.1), new Peak(116.1, 1116.1), new Peak(117.1, 1117.1) }.ToList()),
        PeakInIsolationWindow = new PeakList<Peak>(new Peak[] { new Peak(1214.1, 114.1), new Peak(1215.1, 115.1), new Peak(1216.1, 116.1), new Peak(1217.1, 117.1) }.ToList())
        {
          Precursor = new PrecursorPeak()
          {
            MasterScan = 3,
            Charge = 3,
            Intensity = 4.0,
            IsolationMass = 2800.1,
            IsolationWidth = 3.0,
            MonoIsotopicMass = 2879.1
          }
        }
      };

      t2[113] = 3.5;
      t2[114] = 4.5;
      t2[115] = 5.5;
      t2[116] = 6.5;
      t2[117] = 7.5;
      t2[118] = 8.5;
      t2[119] = 9.5;
      t2[121] = 11.5;

      tr = new IsobaricResult();
      tr.Mode = "PQD";
      tr.Add(t1);
      tr.Add(t2);
    }

    public void RunFormat(IITraqResultFileFormat format)
    {
      Setup();

      var tmpFilename = @"../../../data/temp.xml";
      format.WriteToFile(tmpFilename, tr);

      FileAssert.AreEqual(@"../../../data/ITraqResult.xml", tmpFilename);

      var newtr = format.ReadFromFile(tmpFilename);

      CheckExtension.CheckEquals(tr, newtr);

      format.ReadPeaks = false;
      var newstr2 = format.ReadFromFile(tmpFilename);
      Assert.IsEmpty(newstr2[0].RawPeaks);
      Assert.IsEmpty(newstr2[0].PeakInIsolationWindow);
      Assert.IsEmpty(newstr2[1].RawPeaks);
      Assert.IsEmpty(newstr2[1].PeakInIsolationWindow);

      format.Accept = (m => 355 == m.Scan.Scan);
      var newstr3 = format.ReadFromFile(tmpFilename);
      Assert.AreEqual(1, newstr3.Count);
      Assert.AreEqual(355, newstr3[0].Scan.Scan);

      var lines = File.ReadAllText(tmpFilename);
      ITraqResultXmlFormatReader reader = new ITraqResultXmlFormatReader();
      reader.OpenByContent(lines);
      var item = reader.Next();
      CheckExtension.CheckEquals(tr[0], item);
      item = reader.Next();
      CheckExtension.CheckEquals(tr[1], item);

      File.Delete(tmpFilename);
      if (File.Exists(tmpFilename + ".index"))
      {
        File.Delete(tmpFilename + ".index");
      }
    }

    [Test]
    public void TestFormatFast()
    {
      RunFormat(new ITraqResultXmlFormatFast());
    }

    [Test]
    public void TestFormatRandom()
    {
      Setup();

      var format = new ITraqResultXmlFormatRandomReader();
      format.Open(@"../../../data/ITraqResult.xml");
      CheckExtension.CheckEquals(t2, format.Read("S2", 355));
      CheckExtension.CheckEquals(t1, format.Read("S1", 255));

      Assert.AreEqual(1281, format.ReadXmlBytes("S1", 255).Length);
      Assert.AreEqual(1386, format.ReadXmlBytes("S2", 355).Length);
    }
  }
}
