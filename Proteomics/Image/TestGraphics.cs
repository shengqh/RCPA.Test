using NUnit.Framework;
using System;
using System.Drawing;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestGraphics
  {
    public void MyTestWidth()
    {
      Bitmap b = new Bitmap(400, 300);
      Graphics g = Graphics.FromImage(b);
      Font f = new Font(FontFamily.GenericSansSerif, 9);

      for (int i = 1; i < 10; i++)
      {
        var str = StringUtils.RepeatChar('A', i);
        var width = g.MeasureString(str, f).Width;
        Console.WriteLine("{0}, {1:0.0}", i, width);
      }
    }
  }
}
