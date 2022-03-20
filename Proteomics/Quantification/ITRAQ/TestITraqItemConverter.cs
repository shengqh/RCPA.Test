using NUnit.Framework;
using RCPA.Converter;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  [TestFixture]
  public class TestITraqItemConverter
  {
    private bool exportToConsole = false;

    public void TestReadWrite()
    {
      CheckPlexType(IsobaricType.PLEX4,
        "ITRAQ_TYPE,I114,I115,I116,I117,IValid,IValidProbability",
        "PLEX4,114.0,115.0,116.0,117.0,True,5.000000E-004");

      CheckPlexType(IsobaricType.PLEX8,
        "ITRAQ_TYPE,I113,I114,I115,I116,I117,I118,I119,I121,IValid,IValidProbability",
        "PLEX8,113.0,114.0,115.0,116.0,117.0,118.0,119.0,121.0,True,5.000000E-004");
    }

    private void CheckPlexType(IsobaricType plexType, string expectHeader, string expectValue)
    {
      var ann = new IdentifiedSpectrum();

      var pqr = new IsobaricItem()
      {
        PlexType = plexType,
        Valid = true,
        ValidProbability = 0.0005
      };

      var refItems = plexType.GetDefinition().Items;
      foreach (var item in refItems)
      {
        pqr[item.Index] = item.Mass;
      }

      ann.SetIsobaricItem(pqr);

      //从实例构建converter
      var converter = new ITraqItemPlexConverter<IAnnotation>();
      List<IPropertyConverter<IAnnotation>> converters = new List<IPropertyConverter<IAnnotation>>();
      converters.Add(converter);
      converters.AddRange(converter.GetRelativeConverter(new IAnnotation[] { ann }.ToList()));
      CompositePropertyConverter<IAnnotation> finalConverter = new CompositePropertyConverter<IAnnotation>(converters, ',');

      if (exportToConsole)
      {
        Console.WriteLine(finalConverter.Name);
      }
      Assert.AreEqual(expectHeader, finalConverter.Name);

      var line1 = finalConverter.GetProperty(ann);
      if (exportToConsole)
      {
        Console.WriteLine(line1);
      }
      Assert.AreEqual(expectValue, line1);

      var protein2 = new IdentifiedSpectrum();

      //从factory根据header构建converter
      var finalC = IdentifiedSpectrumPropertyConverterFactory.GetInstance().GetConverters(finalConverter.Name, ',');
      finalC.SetProperty(protein2, line1);

      var line2 = finalConverter.GetProperty(protein2);
      Assert.AreEqual(line1, line2);
    }
  }
}
