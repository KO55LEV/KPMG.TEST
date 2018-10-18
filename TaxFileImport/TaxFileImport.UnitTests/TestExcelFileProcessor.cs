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

namespace TaxFileImport.UnitTests
{
    public class TestExcelFileProcessor
    {

        public TestExcelFileProcessor()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<TransactionInput, Transaction>());
            //TODO move to settings
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        

        [Fact]
        public void ValidateExcelFileNoAccount()
        {

            ExcelFile ef;
            var assembly = Assembly.GetExecutingAssembly();
            var fileName = "transactions_no_account.xlsx";
            var resourceName = $"TaxFileImport.UnitTests.TestFiles.{fileName}";
            
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                ef = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
            }

            //Act
            var mockTransactionProcessor = A.Fake<ITransactionProcessor>();
            var excelFileProcessor = new ExcelFileProcessor(mockTransactionProcessor);

            var errorMessages = excelFileProcessor.ValidateExelFile(ef);
            //Assert 
            Assert.True(errorMessages.Count > 0);
            Assert.Equal($"In uploaded file the first column must be Account", errorMessages[0].Message);
        }


        [Fact]
        public void ValidateExcelFileNoDescription()
        {

            ExcelFile ef;
            var assembly = Assembly.GetExecutingAssembly();
            var fileName = "transactions_no_descrip.xlsx";
            var resourceName = $"TaxFileImport.UnitTests.TestFiles.{fileName}";
            
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                ef = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
            }

            //Act
            var mockTransactionProcessor = A.Fake<ITransactionProcessor>();
            var excelFileProcessor = new ExcelFileProcessor(mockTransactionProcessor);

            var errorMessages = excelFileProcessor.ValidateExelFile(ef);
            //Assert 
            Assert.True(errorMessages.Count > 0);
            Assert.Equal($"In uploaded file the second column must be Description", errorMessages[0].Message);
        }

        [Fact]
        public void ValidateExcel20Transactions()
        {

            ExcelFile ef;
            var assembly = Assembly.GetExecutingAssembly();
            var fileName = "20_transactions.xlsx";
            var resourceName = $"TaxFileImport.UnitTests.TestFiles.{fileName}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                ef = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
            }

            //Act
            
            var mockTransactionDataProvider = A.Fake<ITransactionDataProvider>();
            var transactionProcessor = new TransactionProcessor(new Iso4217DataProvider(), mockTransactionDataProvider);
            var excelFileProcessor = new ExcelFileProcessor(transactionProcessor);

            //setup on call DataProvide to data save successfully
            A.CallTo(() => mockTransactionDataProvider.Save(A<Transaction>._)).Returns(1);

            var errorMessages = excelFileProcessor.ProcessExcelFile(ef);
            //Assert 
            Assert.True(errorMessages.Count > 0);

        }

    }
}
