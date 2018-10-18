using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoMapper;
using TaxFileImport.Core;
using Xunit;
using TaxFileImport.Core.Model;
using FakeItEasy;
using GemBox.Spreadsheet;
using Microsoft.Extensions.Caching.Memory;

namespace TaxFileImport.UnitTests
{
    public class MemoryDataProviderTest
    {

        //public MemoryDataProviderTest()
        //{
        //    Mapper.Reset();
        //    Mapper.Initialize(cfg => cfg.CreateMap<TransactionInput, Transaction>());
        //}
        

        [Fact]
        public void SaveTest()
        {
            //Arrange 
            var testTransactionInput = new Transaction()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = 5
            };

            //Act
            //var mockMemoryCache = A.Fake<IMemoryCache>();
            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var transactionDataProviderMemory = new TransactionDataProviderMemory(cache);
            var transactionId = transactionDataProviderMemory.Save(testTransactionInput);
            //Assert 
            Assert.True(transactionId > 0);

        }


        [Fact]
        public void IntegrationTestSaveGetUpdateDelete()
        {
            //Arrange 
            var testTransaction = new Transaction()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = 5
            };

            //Act

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var transactionDataProviderMemory = new TransactionDataProviderMemory(cache);
            var transactionId = transactionDataProviderMemory.Save(testTransaction);
            //is saved ?   
            Assert.True(transactionId > 0);

            //is returned ? 
            var underTestTransaction = transactionDataProviderMemory.Get(transactionId);

            //Is Same Transaction
            Assert.Equal(testTransaction.Amount , underTestTransaction.Amount);
            Assert.Equal(testTransaction.Account, underTestTransaction.Account);
            Assert.Equal(testTransaction.CurrencyCode, underTestTransaction.CurrencyCode);
            Assert.Equal(testTransaction.Description, underTestTransaction.Description);

            //update
            testTransaction.Account = "Account2";
            testTransaction.Description = "Descrip2";
            testTransaction.CurrencyCode = "USD";
            testTransaction.Amount = 10;
            transactionDataProviderMemory.Update(transactionId , testTransaction);

            var updatedTransaction = transactionDataProviderMemory.Get(transactionId);

            Assert.Equal(testTransaction.Amount, updatedTransaction.Amount);
            Assert.Equal(testTransaction.Account, updatedTransaction.Account);
            Assert.Equal(testTransaction.CurrencyCode, updatedTransaction.CurrencyCode);
            Assert.Equal(testTransaction.Description, updatedTransaction.Description);

            //is deleted? 
            Assert.True(transactionDataProviderMemory.Delete(transactionId));

        }


        [Fact]
        public void IntegrationGetAllTest()
        {
            //Arrange 
            var testTransaction1 = new Transaction()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = 5
            };

            var testTransaction2 = new Transaction()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = 5
            };

            var testTransaction3 = new Transaction()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = 5
            };

            //Act

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            var transactionDataProviderMemory = new TransactionDataProviderMemory(cache);
            var transactionId1 = transactionDataProviderMemory.Save(testTransaction1);
            Assert.True(transactionId1 > 0);
            var transactionId2 = transactionDataProviderMemory.Save(testTransaction2);
            Assert.True(transactionId2 > 0);
            var transactionId3 = transactionDataProviderMemory.Save(testTransaction3);
            Assert.True(transactionId3 > 0);

            var allTransactions = transactionDataProviderMemory.Get();
            Assert.True(allTransactions.Count == 3);

            //delete one random transaction
            transactionDataProviderMemory.Delete(111222333);
            //delete one transaction 
            transactionDataProviderMemory.Delete(transactionId2);

            allTransactions = transactionDataProviderMemory.Get();
            Assert.True(allTransactions.Count == 2);

        }

    }
}
