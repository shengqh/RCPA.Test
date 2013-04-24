using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestDtaOutFilenameConverter
  {
    [Test]
    public void TestDtasOutsFile()
    {
      var file = new DtaOutFilenameConverter("d.e.c.dtas");
      Assert.IsFalse(file.IsZip);
      Assert.IsTrue(file.IsDtasOutsFile);

      Assert.AreEqual("d.e.c", file.PureName);
      Assert.AreEqual("d.e.c.dtas", file.GetDtasFilename());
      Assert.AreEqual("d.e.c.outs", file.GetOutsFilename());
    }

    [Test]
    public void TestDtasOutsZipFile()
    {
      var file = new DtaOutFilenameConverter("d.e.c.dtas.zip");
      Assert.IsTrue(file.IsZip);
      Assert.IsTrue(file.IsDtasOutsFile);

      Assert.AreEqual("d.e.c", file.PureName);
      Assert.AreEqual("d.e.c.dtas.zip", file.GetDtasFilename());
      Assert.AreEqual("d.e.c.outs.zip", file.GetOutsFilename());
    }

    [Test]
    public void TestDtaOutZipFile()
    {
      var file = new DtaOutFilenameConverter("d.e.c.zip");
      Assert.IsTrue(file.IsZip);
      Assert.IsFalse(file.IsDtasOutsFile);

      Assert.AreEqual("d.e.c", file.PureName);
      Assert.AreEqual("d.e.c.zip", file.GetDtasFilename());
      Assert.AreEqual("d.e.c.zip", file.GetOutsFilename());
    }
  }
}
