using System;
using System.Collections.Generic;
using TaxFileImport.Core.Model;

namespace TaxFileImport.Core
{
    public class TransactionDataProviderSql : ITransactionDataProvider
    {
        public int Save(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction Get(int transactionId)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> Get()
        {
            throw new NotImplementedException();
        }

        public void Update(int transactionId, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
