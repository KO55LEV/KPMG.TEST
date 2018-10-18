using System;
using System.Collections.Generic;
using System.Text;

namespace TaxFileImport.Core.Model
{
    public class TransactionInput
    {
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public string Amount { get; set; }

        public decimal AmountDecimal => Helper.StrToDecimal(Amount);
    }
}
