using System;
using System.Collections.Generic;
using System.Text;
using TaxFileImport.Core.Model;

namespace TaxFileImport.Core
{
    public interface ITransactionDataProvider
    {
        int Save(Transaction transaction);
        Transaction Get(int transactionId);
        List<Transaction> Get();
        void Update(int transactionId, Transaction transaction);
        bool Delete(int transactionId);
    }
}
