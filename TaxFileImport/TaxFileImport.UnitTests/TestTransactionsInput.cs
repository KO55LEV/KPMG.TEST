using System;
using System.Text.RegularExpressions;
using AutoMapper;
using TaxFileImport.Core;
using Xunit;
using TaxFileImport.Core.Model;
using FakeItEasy;

namespace TaxFileImport.UnitTests
{
    public class TestTransactionsInput
    {

        //public TestTransactionsInput()
        //{
        //    Mapper.Reset();
        //    Mapper.Initialize(cfg => cfg.CreateMap<TransactionInput, Transaction>());
        //}

        

        [Fact]
        public void ValidateRequireAccountTransaction()
        {
            //Arrange 
            var underTestTransaction = new TransactionInput()
            {
                Account = "",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcessor(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.Validate(underTestTransaction);

            //Assert 
            Assert.False(result);
        }

        [Fact]
        public void ValidateRequireDescripTransaction()
        {
            //Arrange 
            var underTestTransaction = new TransactionInput()
            {
                Account = "Account",
                Description = "",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            //Act
            var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcessor(new Iso4217DataProvider(), mockTransactionDataProvider);
            var result = testTransaction.Validate(underTestTransaction);

            //Assert 
            Assert.False(result);
        }

        [Fact]
        public void ValidateWrongAmountTransaction()
        {
            //Arrange 
            var underTestTransaction = new TransactionInput()
            {
                Account = "Account",
                Description = "Descip",
                CurrencyCode = "GBP",
                Amount = "FIVE"
            };
            //Act
            var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcessor(new Iso4217DataProvider(), mockTransactionDataProvider);
            var result = testTransaction.Validate(underTestTransaction);

            //Assert 
            Assert.False(result);
        }


        [Fact]
        public void ValidateWrongIsoTransaction()
        {
            //Arrange 
            var underTestTransaction = new TransactionInput()
            {
                Account = "Account",
                Description = "Descip",
                CurrencyCode = "GBE",
                Amount = "5"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcessor(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.Validate(underTestTransaction);

            //Assert 
            Assert.False(result);
        }

        [Fact]
        public void ValidateCorrectTransaction()
        {
            //Arrange 
            var underTestTransaction = new TransactionInput()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            //Act
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcessor(new Iso4217DataProvider(), transactionDataProvider);
            var result = testTransaction.Validate(underTestTransaction);

            //Assert 
            Assert.True(result);
        }

        [Fact]
        public void ValidateFailedSavingTransaction()
        {
            //Arrange 
            var testTransactionInput = new TransactionInput()
            {
                Account = "Account",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            //Act


            var transation = Mapper.Map<Transaction>(testTransactionInput);
            ITransactionDataProvider transactionDataProvider = A.Fake<ITransactionDataProvider>();
            A.CallTo(() => transactionDataProvider.Save(transation)).Returns(0);
            var transactionProcessor = new TransactionProcessor(new Iso4217DataProvider(), transactionDataProvider);
            var result = transactionProcessor.Process(testTransactionInput);

            //Assert 
            Assert.True(result.Error);
        }



        [Fact]
        public void SaveWrongTransaction()
        {
            //Arrange 
            var underTestTransaction = new TransactionInput()
            {
                Account = "",
                Description = "Descrip",
                CurrencyCode = "GBP",
                Amount = "5"
            };
            //Act
            var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            var testTransaction = new TransactionProcessor(new Iso4217DataProvider() , mockTransactionDataProvider);
            var result = testTransaction.Process(underTestTransaction);

            //Assert 
            Assert.True(result.Error);
        }


      


    }
}
