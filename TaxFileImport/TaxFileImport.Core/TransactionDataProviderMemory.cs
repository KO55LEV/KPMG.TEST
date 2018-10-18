using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using TaxFileImport.Core.Model;

namespace TaxFileImport.Core
{
    public class TransactionDataProviderMemory : ITransactionDataProvider
    {

        private readonly IMemoryCache _cache;
        private readonly string _allTransactionKey = "allTransactions";
        private List<int> AllTransactions;



        public TransactionDataProviderMemory(IMemoryCache cache)
        {
            _cache = cache;
            AllTransactions = _cache.Get<List<int>>(_allTransactionKey) ?? new List<int>();
        }

        public int Save(Transaction transaction)
        {
            var key = Guid.NewGuid();
            var id = BitConverter.ToInt32(key.ToByteArray(), 0);
            //only positive ids
            if (id < 0) id = -id;
            _cache.Set(id.ToString(), transaction);

            AllTransactions.Add(id);
            _cache.Set(_allTransactionKey, AllTransactions);

            return id;
        }

        public List<Transaction> Get()
        {
            var output = new List<Transaction>();
            foreach (var transactionId in AllTransactions)
            {
                output.Add(Get(transactionId));
            }

            return output;
        }

        public Transaction Get(int transactionId)
        {
            return _cache.Get<Transaction>(transactionId.ToString());
        }

        public void Update(int transactionId, Transaction transaction)
        {
            Delete(transactionId);

            AllTransactions.Add(transactionId);
            _cache.Set(_allTransactionKey, AllTransactions);

            _cache.Set(transactionId.ToString(), transaction);
        }

        public bool Delete(int transactionId)
        {
            
            _cache.Remove(transactionId.ToString());

            AllTransactions.Remove(transactionId);
            _cache.Set(_allTransactionKey, AllTransactions);

            return (Get(transactionId) == null);
        }
    }
}
