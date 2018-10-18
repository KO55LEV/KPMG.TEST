using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaxFileImport.Core.Model;

namespace TaxFileImport.Core
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly IIso4217DataProvider _currencyProvider;
        private readonly ITransactionDataProvider _transactionDataProvider;

        public TransactionProcessor(IIso4217DataProvider currencyProvider, ITransactionDataProvider transactionDataProvider)
        {
            _currencyProvider = currencyProvider;
            _transactionDataProvider = transactionDataProvider;
        }
        public bool Validate(TransactionInput transation)
        {
            if (string.IsNullOrEmpty(transation.Account)) return false;
            if (string.IsNullOrEmpty(transation.Description)) return false;
            if (string.IsNullOrEmpty(transation.Amount)) return false;

            decimal output = 0;
            if (!decimal.TryParse(transation.Amount, out output)) return false;

            var validate = _currencyProvider.ValidateCode(transation.CurrencyCode);
            return validate;
        }

        public TransactionProcessingStatus Process(TransactionInput transationInput)
        {
            var result = new TransactionProcessingStatus();
            if (!Validate(transationInput))
            {
                result.Error = true;
                result.ErrorMessage =
                    $"Cound not process trasnsaction ( Account:{transationInput.Account} , Desciption:{transationInput.Description}, Currency:{transationInput.CurrencyCode}, Amount {transationInput.Amount} )";
                return result;
            }
            var transation = Mapper.Map<Transaction>(transationInput);
            var transactionId = _transactionDataProvider.Save(transation);
            if (transactionId > 0)
            {
                result.TransactionId = transactionId;
                return result;
            }
            else
            {
                result.Error = true;
                result.ErrorMessage =
                    $"Error in saving transaction Account:{transationInput.Account} , Desciption:{transationInput.Description}, Currency:{transationInput.CurrencyCode}, Amount {transationInput.Amount} ";
                return result;
            }
        }
    }
}
