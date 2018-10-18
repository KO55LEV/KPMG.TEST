using System;
using System.Collections.Generic;
using System.Text;
using TaxFileImport.Core.Model;

namespace TaxFileImport.Core
{
    public interface ITransactionProcessor
    {
        bool Validate(TransactionInput transactionInput);
        TransactionProcessingStatus Process(TransactionInput transactionInput);
    }
}
