using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Snp
{
  [TestFixture]
  public class TestMS2Item
  {
    [Test]
    public void TestInitTerminalLoss()
    {
      var item = new MS2Item()
      {
        Charge = 3,
        Precursor = 376.52331,
        Peptide = "-.EHSSL^AYWK.-"
      };

      var aas = new Aminoacids();
      aas['^'].ResetMass(7.017,7.017);

      item.InitTerminalLoss(aas, 6, 2);
      Assert.AreEqual(12, item.TerminalLoss.Count);

      Assert.AreEqual("LAYWK", item.TerminalLoss[3].Sequence);
      Assert.IsTrue(item.TerminalLoss[3].IsNterminal);
      Assert.AreEqual("AYWK", item.TerminalLoss[4].Sequence);
      Assert.IsTrue(item.TerminalLoss[4].IsNterminal);
      Assert.AreEqual(7.017, (item.TerminalLoss[3].Precursor - item.TerminalLoss[4].Precursor) * 3 - aas['L'].MonoMass, 0.001);

      Assert.AreEqual("EHSSL", item.TerminalLoss[9].Sequence);
      Assert.IsFalse(item.TerminalLoss[9].IsNterminal);
      Assert.AreEqual("EHSS", item.TerminalLoss[10].Sequence);
      Assert.IsFalse(item.TerminalLoss[10].IsNterminal);
      Assert.AreEqual(7.017, (item.TerminalLoss[9].Precursor - item.TerminalLoss[10].Precursor) * 3 - aas['L'].MonoMass, 0.001);

      //item.TerminalLoss.ForEach(m => Console.WriteLine(m.Precursor.ToString() + "\t" + m.Sequence));
    }
  }
}
