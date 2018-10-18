using System;
using System.Collections.Generic;
using System.Text;

namespace TaxFileImport.Core
{
    public interface IIso4217DataProvider
    {
        bool ValidateCode(string iso);
    }
}
