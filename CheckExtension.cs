using NUnit.Framework;
using System;
using System.Reflection;

namespace RCPA
{
  public static class CheckExtension
  {
    private static object GetProperty(object AObject, string APropertyName)
    {
      if (AObject == null) return null;
      Type vType = AObject.GetType();
      PropertyInfo vPropertyInfo = vType.GetProperty(APropertyName);
      if (vPropertyInfo == null) return null;
      return vPropertyInfo.GetValue(AObject, null);
    }

    public static void CheckEquals(object source, object target, string name = "")
    {
      if (object.Equals(source, target))
      {
        return;
      }

      var vType = source.GetType();

      var properties = vType.GetProperties();

      if (properties.Length == 0)
      {
        Assert.AreEqual(source, target);
      }
      else
      {
        foreach (var vPropertyInfo in properties)
        {
          if (vPropertyInfo.GetIndexParameters().Length > 0)
          {
            var countProperty = vType.GetProperty("Count");
            if (null != countProperty)
            {
              var count = (int)countProperty.GetValue(source, null);
              for (int i = 0; i < count; i++)
              {
                var v1 = vPropertyInfo.GetValue(source, new object[] { i });
                var v2 = vPropertyInfo.GetValue(target, new object[] { i });
                CheckEquals(v1, v2, name + "." + vPropertyInfo.Name);
              }
              continue;
            }
            else
            {
              //Console.WriteLine(string.Format("Unknown type {0}", name + "." + vPropertyInfo.Name));
              continue;
            }
          }
          else
          {
            var v11 = vPropertyInfo.GetValue(source, null);
            var v22 = vPropertyInfo.GetValue(target, null);

            if (vPropertyInfo.PropertyType == typeof(System.String) ||
              vPropertyInfo.PropertyType == typeof(System.Int32) ||
              vPropertyInfo.PropertyType == typeof(System.Double) ||
              vPropertyInfo.PropertyType == typeof(System.Boolean))
            {
              Assert.AreEqual(v11, v22, string.Format(name + "." + vPropertyInfo.Name + " error : {0} <> {1}", v11, v22));
            }
            else
            {
              CheckEquals(v11, v22, name + "." + vPropertyInfo.Name);
            }
          }
        }
      }
    }
  }
}
