using NUnit.Framework;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Swath
{
  [TestFixture]
  public class MzXmlSwathWindowsProcessorTest
  {
    [Test]
    public void TestWithWindow()
    {
      var input = "<scan num=\"2\"> <precursorMz windowWideness=\"25.0\" precursorIntensity=\"0\" activationMethod=\"CID\">412.5</precursorMz> <peaks>";
      var expect = "<scan num=\"2\"> <precursorMz windowWideness=\"30.0\" precursorIntensity=\"0\" activationMethod=\"CID\">400.5</precursorMz> <peaks>";
      var actual = MzXmlSwathWindowsProcessor.ReplaceWindow(input, 400.5, 30.0, " windowWideness=\"25.0\" precursorIntensity=\"0\" activationMethod=\"CID\"");
      Assert.AreEqual(expect ,actual);
    }

    [Test]
    public void TestWithoutWindow()
    {
      var input = "<scan num=\"2\"> <precursorMz precursorIntensity=\"0\" activationMethod=\"CID\">412.5</precursorMz> <peaks>";
      var expect = "<scan num=\"2\"> <precursorMz windowWideness=\"30.0\" precursorIntensity=\"0\" activationMethod=\"CID\">400.5</precursorMz> <peaks>";
      var actual = MzXmlSwathWindowsProcessor.ReplaceWindow(input, 400.5, 30.0, " precursorIntensity=\"0\" activationMethod=\"CID\"");
      Assert.AreEqual(expect, actual);
    }
  }
}
