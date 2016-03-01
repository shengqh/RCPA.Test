using NUnit.Framework;
using RCPA.Utils;
using System;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  [TestFixture]
  public class TestMzXmlImpl
  {
    [Test]
    public void TestBase()
    {
      string base64str = "QxM/DkB24vJDMyt6QBRgeENbdvc/mMj7Q10uzEIfyuRDbrslP9AqwEOEgl5Ak2SaQ4yBTkQNhrhDjOpQPx9wFUONXNg/l3fzQ5Vl1EJSOfRDo3wUQB9u6kOqhexCGSLYQ6x8wD/UEBtDs3AxQOwt2kPIl2BAgrdfQ897DULgzv9Dz8fdP5zw5A==";

      bool isBigEndian = true;

      byte[] dataBytes = System.Convert.FromBase64String(base64str);

      double[] dataMz = MathUtils.Byte32ToDoubleList(34, isBigEndian, dataBytes);

      PeakList<Peak> pkl = MzxmlHelper.Base64ToPeakList(base64str);

      Assert.AreEqual(dataMz.Length, pkl.Count * 2);

      for (int i = 0; i < pkl.Count; i++)
      {
        Assert.AreEqual(dataMz[i * 2], pkl[i].Mz, 0.001);
        Assert.AreEqual(dataMz[i * 2 + 1], pkl[i].Intensity, 0.001);
      }
    }

    private MzXMLImpl2 reader = new MzXMLImpl2();

    [TestFixtureSetUp]
    public void LoadFile()
    {
      reader.Open("../../../data/YAGmem1.mzXML");
    }

    [TestFixtureTearDown]
    public void CloseFile()
    {
      reader.Close();
    }

    [Test]
    public void TestGetNumSpectra()
    {
      Assert.AreEqual(2707, reader.GetNumSpectra());
    }

    [Test]
    public void TestGetMsLevel()
    {
      //scan 1 and scan 2 is slibing node
      Assert.AreEqual(1, reader.GetMsLevel(1));
      Assert.AreEqual(2, reader.GetMsLevel(2));

      //scan 3 and scan 4 is parent-child
      Assert.AreEqual(1, reader.GetMsLevel(3));
      Assert.AreEqual(2, reader.GetMsLevel(4));
    }

    [Test]
    public void TestRTFromScanNum()
    {
      Assert.AreEqual(2.02, reader.ScanToRetentionTime(2), 0.01);
    }

    [Test]
    public void TestGetPeakList()
    {
      PeakList<Peak> pkl = reader.GetPeakList(2);
      Assert.AreEqual(19, pkl.Count);
      Assert.AreEqual(149.0103, pkl[0].Mz, 0.0001);
      Assert.AreEqual(11918.0, pkl[0].Intensity, 0.1);
    }

    [Test]
    public void TestGetPrecursorPeak()
    {
      Peak peak = reader.GetPrecursorPeak(2);
      Assert.AreEqual(445.057739, peak.Mz, 0.000001);
      Assert.AreEqual(6390976.000000, peak.Intensity, 0.000001);
    }

    [Test]
    public void TestGetFirstSpectrumNumber()
    {
      Assert.AreEqual(1, reader.GetFirstSpectrumNumber());
    }

    [Test]
    public void TestGetLastSpectrumNumber()
    {
      Assert.AreEqual(2707, reader.GetLastSpectrumNumber());
    }

    [Test]
    public void TestIsProfileScanForScanNum()
    {
      Assert.IsFalse(reader.IsProfileScanForScanNum(1));
      Assert.IsFalse(reader.IsProfileScanForScanNum(2));
    }

    [Test]
    public void TestIsCentroidScanForScanNum()
    {
      Assert.IsTrue(reader.IsCentroidScanForScanNum(1));
      Assert.IsTrue(reader.IsCentroidScanForScanNum(2));
    }
  }
}