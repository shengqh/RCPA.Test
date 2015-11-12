using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Seq;

namespace RCPA.Proteomics.Database
{
  [TestFixture]
  public class TestReversedDatabaseBuilder
  {
    [Test]
    public void TestGetReversedSequence()
    {
      var seq = new Sequence("sp|Q12345 sp protein","ABCDE");
      var options = new ReversedDatabaseBuilderOptions()
      {
        DecoyKey = "REVERSED",
        DecoyType = DecoyType.Start
      };

      var builder = new ReversedDatabaseBuilder(options);
      var actual = builder.GetReversedSequence(1, seq);

      Assert.AreEqual("sp|REVERSED_Q12345", actual.Name);
    }
  }
}
