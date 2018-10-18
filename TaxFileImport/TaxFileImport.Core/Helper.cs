using System;
using System.Collections.Generic;
using System.Text;

namespace TaxFileImport.Core
{
    public static class Helper
    {
        public static decimal StrToDecimal(object amount)
        {
            decimal output = 0;
            return !decimal.TryParse(amount.ToString(), out output) ? output : decimal.Parse(amount.ToString());
        }
    }
}
