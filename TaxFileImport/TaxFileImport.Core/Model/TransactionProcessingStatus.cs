using System;
using System.Collections.Generic;
using System.Text;

namespace TaxFileImport.Core.Model
{
    public class TransactionProcessingStatus
    {
        public TransactionProcessingStatus()
        {
            Error = false;
        }
        public int TransactionId { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
