using NUnit.Framework;
using RCPA.Converter;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  [TestFixture]
  public class TestSilacProteinQuantificationResultConverter
  {
    public void TestReadWrite()
    {
      var ann = new IdentifiedProtein();

      var pqr = new ProteinQuantificationResult();

      pqr.Items["D1"] = new QuantificationItem()
      {
        Enabled = true,
        Ratio = 1.5,
        SampleIntensity = 150,
        ReferenceIntensity = 100,
        Correlation = 0.9,
        ScanCount = 55,
        Filename = "test1.silac"
      };

      pqr.Items["D2"] = new QuantificationItem()
      {
        Enabled = false,
        Ratio = 3,
        SampleIntensity = 250,
        ReferenceIntensity = 200,
        Correlation = 0.8,
        ScanCount = 77,
        Filename = "test2.silac"
      };

      ann.Annotations[SilacQuantificationConstants.SILAC_KEY] = pqr;

      var converter = new SilacProteinQuantificationResultConverter2<IAnnotation>();
      List<IPropertyConverter<IAnnotation>> converters = new List<IPropertyConverter<IAnnotation>>();
      converters.Add(converter);
      converters.AddRange(converter.GetRelativeConverter(new IAnnotation[] { ann }.ToList()));
      CompositePropertyConverter<IAnnotation> finalConverter = new CompositePropertyConverter<IAnnotation>(converters, ',');

      Assert.AreEqual("S_COUNT,SE_D1,SR_D1,SRC_D1,SSI_D1,SRI_D1,SE_D2,SR_D2,SRC_D2,SSI_D2,SRI_D2", finalConverter.Name);

      var line1 = finalConverter.GetProperty(ann);
      Assert.AreEqual("2,True,1.5000,0.9000,150.0,100.0,False,3.0000,0.8000,250.0,200.0", line1);

      var protein2 = new IdentifiedProtein();
      var finalC = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(finalConverter.Name, ',');
      finalC.SetProperty(protein2, line1);

      var line2 = finalConverter.GetProperty(protein2);
      Assert.AreEqual(line1, line2);
    }
  }
}
