using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Database
{
  [TestFixture]
  public class TestUniprotXmlFormatRandomReader
  {
    [Test]
    public void TestRead()
    {
      var reader = new UniprotXmlFormatRandomReader();
      reader.Open(@"../../../data/P04114.xml");
      var entry = reader.Read("APOB_HUMAN");
      Assert.IsNotNull(entry);

      Assert.AreEqual(56, entry.SequenceVariants.Count);
      Assert.AreEqual("In dbSNP:rs1367117.", entry.SequenceVariants[0].Description);
      Assert.AreEqual("VAR_016184", entry.SequenceVariants[0].ID);
      Assert.AreEqual("3 13 14 15", entry.SequenceVariants[0].Evidence);
      Assert.AreEqual("T", entry.SequenceVariants[0].Original);
      Assert.AreEqual("I", entry.SequenceVariants[0].Variation);
      Assert.AreEqual(98, entry.SequenceVariants[0].Position);

      Assert.AreEqual(48, entry.SequenceConflicts.Count);
      Assert.AreEqual("In Ref. 5; AAB60718/CAA28420.", entry.SequenceConflicts[0].Description);
      Assert.AreEqual("5", entry.SequenceConflicts[0].Evidence);
      Assert.AreEqual(" ", entry.SequenceConflicts[0].Original);
      Assert.AreEqual(" ", entry.SequenceConflicts[0].Variation);
      Assert.AreEqual(11, entry.SequenceConflicts[0].BeginPosition);
      Assert.AreEqual(13, entry.SequenceConflicts[0].EndPosition);
      Assert.AreEqual("L", entry.SequenceConflicts[1].Original);
      Assert.AreEqual("V", entry.SequenceConflicts[1].Variation);
      Assert.AreEqual(329, entry.SequenceConflicts[1].BeginPosition);
      Assert.AreEqual(329, entry.SequenceConflicts[1].EndPosition);
    }
  }
}
