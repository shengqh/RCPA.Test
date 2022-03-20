using NUnit.Framework;
using System.Collections.Generic;

namespace RCPA.Proteomics.Mascot
{
  [TestFixture]
  public class TestMascotDatToMzIdentConverter : MascotDatToMzIdentConverter
  {
    public TestMascotDatToMzIdentConverter() : base(new MascotDatToMzIdentConverterOptions()
    {
      TitleFormat = "DTA",
      InputFiles = new List<string>(new string[] { @"H:\shengquanhu\projects\rcpa\TurboRaw2Mgf\TMT6\mascot\searching\top10.dat" })
    })
    { }

    [Test]
    public void TestProcess()
    {
      Process();
    }
  }
}