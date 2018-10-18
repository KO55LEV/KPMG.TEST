using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using TaxFileImport.Core.Model;

namespace TaxFileImport.Core
{

    public class ExcelProcessorMessage
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public bool IsErrored { get; set; }
    }

    public class ExcelProcessorErrorMessages
    {
        public List<ExcelProcessorMessage> Messages { get; set; }

        public ExcelProcessorErrorMessages()
        {
            Messages = new List<ExcelProcessorMessage>();
        }
    }

    public class ExcelFileProcessor
    {
        private readonly ITransactionProcessor _transactionProcessor;

        public ExcelFileProcessor(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
        }

        public List<ExcelProcessorMessage> ValidateExelFile(ExcelFile ef)
        {
            string errorKey = "FileValidation";
            var errorMessages = new List<ExcelProcessorMessage>();
            if (ef.Worksheets.Count == 0)
            {
                errorMessages.Add(
                    new ExcelProcessorMessage()
                    {
                        Key = errorKey,
                        Message = $"The file doesn't have any worksheet.",
                        IsErrored = true
                    }
                );
            }

            var activeWorksheet = ef.Worksheets.ActiveWorksheet;

            if (activeWorksheet.Columns[0].Cells[0].Value.ToString().ToUpper() != "ACCOUNT")
            {
                errorMessages.Add(
                    new ExcelProcessorMessage()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the first column must be Account",
                        IsErrored = true
        }
                );
            }

            if (activeWorksheet.Columns[1].Cells[0].Value.ToString().ToUpper() != "DESCRIPTION")
            {
                errorMessages.Add(
                    new ExcelProcessorMessage()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the second column must be Description",
                        IsErrored = true
                    }
                );

            }

            if (activeWorksheet.Columns[2].Cells[0].Value.ToString().ToUpper() != "CURRENCY CODE")
            {
                errorMessages.Add(
                    new ExcelProcessorMessage()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the third column must be Currency Code",
                        IsErrored = true
                    }
                );
            }

            if (activeWorksheet.Columns[3].Cells[0].Value.ToString().ToUpper() != "AMOUNT")
            {
                errorMessages.Add(
                    new ExcelProcessorMessage()
                    {
                        Key = errorKey,
                        Message = $"In uploaded file the last column must be Amount",
                        IsErrored = true
                    }
                );
            }

            return errorMessages;
        }

        public List<ExcelProcessorMessage> ProcessExcelFile(ExcelFile ef)
        {
            var errorMessages = new List<ExcelProcessorMessage>();

            var activeWorksheet = ef.Worksheets.ActiveWorksheet;
            string output = string.Empty;
            CellRange range = activeWorksheet.GetUsedCellRange(true);
            var rowProcessed = 0;

            for (int j = range.FirstRowIndex; j <= range.LastRowIndex; j++)
            {
                var transaction = new TransactionInput();

                for (int i = range.FirstColumnIndex; i <= range.LastColumnIndex; i++)
                {

                    ExcelCell cell = range[j - range.FirstRowIndex, i - range.FirstColumnIndex];

                    string cellName = CellRange.RowColumnToPosition(j, i);
                    string cellRow = ExcelRowCollection.RowIndexToName(j);
                    string cellColumn = ExcelColumnCollection.ColumnIndexToName(i);
                    if (cellColumn == "A") transaction.Account = (cell.Value == null)? string.Empty : cell.Value.ToString();
                    if (cellColumn == "B") transaction.Description = (cell.Value == null) ? string.Empty : cell.Value.ToString();
                    if (cellColumn == "C") transaction.CurrencyCode = (cell.Value == null) ? string.Empty : cell.Value.ToString();
                    if (cellColumn == "D") transaction.Amount = (cell.Value == null) ? string.Empty : cell.Value.ToString();

                    output += string.Format("Cell name: {1}{0}Cell row: {2}{0}Cell column: {3}{0}Cell value: {4}{0}",
                        Environment.NewLine, cellName, cellRow, cellColumn, (cell.Value) ?? "Empty");

                }

                rowProcessed++;
                //ignore first transaction as that it caption 
                if (rowProcessed > 1)
                {
  
                    var transactionStatus = _transactionProcessor.Process(transaction);
                    
                    if (transactionStatus.Error)
                    {
                        errorMessages.Add(
                            new ExcelProcessorMessage()
                            {
                                Key = $"Record_{j}",
                                Message = transactionStatus.ErrorMessage,
                                IsErrored = true
                            }
                        );
                    }
                    else
                    {
                        errorMessages.Add(
                            new ExcelProcessorMessage()
                            {
                                Key = $"Record_{j}",
                                Message = "Record successfully processed",
                                IsErrored = false
                            }
                        );
                    }

                }
            }
            return errorMessages;
        }

    }
}
