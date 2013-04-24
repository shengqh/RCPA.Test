using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestDtasReader
  {
    [Test]
    public void TestAll()
    {
      using (var reader = new DtasReader(@"..\..\data\20030428_4_29L_15.dtas"))
      {
        Assert.AreEqual(731, reader.FileCount);
        for (int i = 0; i < 731; i++)
        {
          Assert.AreEqual(true, reader.HasNext);
          reader.NextContent();
        }
        Assert.AreEqual(false, reader.HasNext);
      }
    }

    [Test]
    [ExpectedException("System.Exception")]
    public void TestException()
    {
      using (var reader = new DtasReader(@"..\..\data\20030428_4_29L_15.dtas"))
      {
        for (int i = 0; i < 731; i++)
        {
          reader.NextContent();
        }

        //expect to throw exception
        reader.NextContent();
      }
    }
  }
}