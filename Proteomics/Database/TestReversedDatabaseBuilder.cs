using NUnit.Framework;
using RCPA.Seq;

namespace RCPA.Proteomics.Database
{
  [TestFixture]
  public class TestReversedDatabaseBuilder
  {
    [Test]
    public void TestGetReversedSequence()
    {
      var options = new ReversedDatabaseBuilderOptions()
      {
        DecoyKey = "REVERSED",
      };
      var builder = new ReversedDatabaseBuilder(options);

      var seq = new Sequence("sp|Q12345 sp protein", "ABCDE");

      options.DecoyType = DecoyType.Start;
      Assert.AreEqual("REVERSED_sp|Q12345", builder.GetReversedSequence(1, seq).Name);

      options.DecoyType = DecoyType.Middle;
      Assert.AreEqual("sp|REVERSED_Q12345", builder.GetReversedSequence(1, seq).Name);

    }
  }
}
