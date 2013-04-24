using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Converter;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.ITraq
{
  [TestFixture]
  public class TestITraqQuantificationResultConverter
  {
    private bool exportToConsole = false;

    public void TestReadWrite()
    {
      var ann = new IdentifiedProtein();

      var pqr = new ITraqQuantificationResult();

      pqr.DatasetMap["D1"] = new ITraqQuantificationDatasetItem()
      {
        DatasetName = "D1"
      };

      pqr.DatasetMap["D1"].RatioMap["R114/REF"] = new ITraqQuantificationChannelItem()
      {
        ChannelName = "R114/REF",
        Ratio = 1.5
      };

      pqr.DatasetMap["D1"].RatioMap["R115/REF"] = new ITraqQuantificationChannelItem()
      {
        ChannelName = "R115/REF",
        Ratio = 1.8
      };

      pqr.DatasetMap["D2"] = new ITraqQuantificationDatasetItem()
      {
        DatasetName = "D2"
      };

      pqr.DatasetMap["D2"].RatioMap["R116/REF"] = new ITraqQuantificationChannelItem()
      {
        ChannelName = "R116/REF",
        Ratio = 2.5
      };

      pqr.DatasetMap["D2"].RatioMap["R117/REF"] = new ITraqQuantificationChannelItem()
      {
        ChannelName = "R117/REF",
        Ratio = 3.8
      };


      ann.Annotations[ITraqConsts.ITRAQ_KEY] = pqr;

      var converter = new ITraqQuantificationResultConverter<IAnnotation>();
      List<IPropertyConverter<IAnnotation>> converters = new List<IPropertyConverter<IAnnotation>>();
      converters.Add(converter);
      converters.AddRange(converter.GetRelativeConverter(new IAnnotation[] { ann }.ToList()));
      CompositePropertyConverter<IAnnotation> finalConverter = new CompositePropertyConverter<IAnnotation>(converters, ',');

      if (exportToConsole)
      {
        Console.WriteLine(finalConverter.Name);
      }
      Assert.AreEqual("ITRAQ_COUNT,IR_D1_R114/REF,IR_D1_R115/REF,IR_D2_R116/REF,IR_D2_R117/REF", finalConverter.Name);

      var line1 = finalConverter.GetProperty(ann);
      if (exportToConsole)
      {
        Console.WriteLine(line1);
      }
      Assert.AreEqual("2,1.5000,1.8000,2.5000,3.8000", line1);

      var protein2 = new IdentifiedProtein();
      var finalC = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(finalConverter.Name, ',');
      finalC.SetProperty(protein2, line1);

      var line2 = finalConverter.GetProperty(protein2);
      Assert.AreEqual(line1, line2);
    }
  }
}
