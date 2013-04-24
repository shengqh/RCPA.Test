using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestAnnotationUtils
  {
    class Annotation : IAnnotation
    {
      public Annotation() { }

      public Annotation(IEnumerable<string> keys)
      {
        foreach (string key in keys)
        {
          annotations[key] = "";
        }
      }

      private Dictionary<string, object> annotations = new Dictionary<string, object>();

      #region IAnnotation Members

      public Dictionary<string, object> Annotations
      {
        get { return annotations; }
      }

      #endregion
    }

    Annotation[] ann = new[]{
        new Annotation(new[]{"B","A","C"}),
        new Annotation(new[]{"C","E", "D"})};

    [Test]
    public void TestGetAnnotationKeys()
    {
      var real = AnnotationUtils.GetAnnotationKeys(ann);

      Assert.AreEqual(5, real.Count);

      Assert.AreEqual(new[] { "A", "B", "C", "D", "E" }, real);
    }

    [Test]
    public void TestGetAnnotationHeader()
    {
      Assert.AreEqual("A\tB\tC\tD\tE", AnnotationUtils.GetAnnotationHeader(ann));
    }

    [Test]
    public void TestSetEnabled()
    {
      Annotation a = new Annotation();

      Assert.IsTrue(a.IsEnabled(true));

      Assert.IsFalse(a.IsEnabled(false));

      a.SetEnabled(true);

      Assert.AreEqual(true.ToString(), a.Annotations[AnnotationUtils.ENABLED_KEY]);

      Assert.IsTrue(a.IsEnabled(false));

      a.Annotations[AnnotationUtils.ENABLED_KEY] = 5;
      Assert.IsTrue(a.IsEnabled(true));
      Assert.IsFalse(a.IsEnabled(false));
    }
  }
}
