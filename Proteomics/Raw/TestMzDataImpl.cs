using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Utils;
using System.Runtime.InteropServices;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  [TestFixture]
  public class TestMzDataImpl
  {
    private IRawFile reader = new MzDataImpl();

    [TestFixtureSetUp]
    public void LoadFile()
    {
      reader.Open("../../../data/QTof_itraq.mzData");
    }

    [TestFixtureTearDown]
    public void CloseFile()
    {
      reader.Close();
    }

    [Test]
    public void TestGetNumSpectra()
    {
      Assert.AreEqual(15, reader.GetNumSpectra());
    }

    [Test]
    public void TestGetMsLevel()
    {
      Assert.AreEqual(1, reader.GetMsLevel(1));
      Assert.AreEqual(2, reader.GetMsLevel(3));
    }

    [Test]
    public void TestRTFromScanNum()
    {
      Assert.AreEqual(0.037, reader.ScanToRetentionTime(1));
    }

    [Test]
    public void TestGetPeakList()
    {
      PeakList<Peak> pkl = reader.GetPeakList(1);
      Assert.AreEqual(397, pkl.Count);
      Assert.AreEqual(301.1257, pkl[0].Mz, 0.0001);
    }

    [Test]
    public void TestGetFirstSpectrumNumber()
    {
      Assert.AreEqual(1, reader.GetFirstSpectrumNumber());
    }

    [Test]
    public void TestGetLastSpectrumNumber()
    {
      Assert.AreEqual(15, reader.GetLastSpectrumNumber());
    }

    [Test]
    public void TestIsProfileScanForScanNum()
    {
      Assert.IsFalse(reader.IsProfileScanForScanNum(1));
      Assert.IsTrue(reader.IsProfileScanForScanNum(2));
    }

    [Test]
    public void TestIsCentroidScanForScanNum()
    {
      Assert.IsTrue(reader.IsCentroidScanForScanNum(1));
      Assert.IsFalse(reader.IsCentroidScanForScanNum(2));
    }

    [Test]
    public void TestGetMsType()
    {
      string msType = reader.GetScanMode(3);
      Assert.AreEqual("pqd", msType);
    }
  }
}
