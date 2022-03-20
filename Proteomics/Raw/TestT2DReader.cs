using NUnit.Framework;
using System;

namespace RCPA.Proteomics.Raw
{
  [TestFixture]
  public class TestT2DReader
  {
    public void TestRead()
    {
      var reader = new T2DReader(@TestContext.CurrentContext.TestDirectory + "/../../../data//E01_MSMS_1329.8326_5.t2d");
      reader.parse();
      Console.WriteLine(reader.spectrumHeader.ToString());
    }
  }
}
