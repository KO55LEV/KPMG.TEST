using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TaxFileImport.Core
{
    public class IsoCode
    {
        public string AlphabeticCode { get; set; }
        public string Currency { get; set; }
        public string Entity { get; set; }
    }

 

    public class Iso4217DataProvider : IIso4217DataProvider
    {
        private static List<IsoCode> _isoCodes;
        public Iso4217DataProvider()
        {
            LoadIsoCodes();
        }

        private void LoadIsoCodes()
        {
            var jsonData = string.Empty;

            if (_isoCodes == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "TaxFileImport.Core.Resources.iso4214.json";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonData = reader.ReadToEnd();
                }

                _isoCodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IsoCode>>(jsonData);
            }
        }

        public bool ValidateCode(string iso)
        {
            return _isoCodes.Any(x => x.AlphabeticCode == iso.ToUpper());
        }
    }
}
