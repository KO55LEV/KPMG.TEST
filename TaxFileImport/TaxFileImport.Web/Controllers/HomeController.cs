using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TaxFileImport.Core;
using TaxFileImport.Web.Models;
using TaxFileImport.Web.Utilities;

namespace TaxFileImport.Web.Controllers
{
    public class HomeController : Controller
    {
        //TODO move this to setup and use DI to inject all this 
        static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        static readonly IIso4217DataProvider _isoProvider = new Iso4217DataProvider();
        static readonly ITransactionDataProvider _transactionDataProvider = new TransactionDataProviderMemory(_cache);
        static readonly ITransactionProcessor _transactionProcessor = new TransactionProcessor(_isoProvider, _transactionDataProvider);

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(FileUpload fileUpload)
        {
            //TODO move this to Startup.CS and load configuraion from settings file. 
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ViewData["UploadMessage"] = null;
            


            var excelFileProcessor = new ExcelFileProcessor(_transactionProcessor);

            if (!ModelState.IsValid)
            {
                return View();
            }

            var excelFile = FileHelpers.GetExcelFile(fileUpload.UploadTaxFile, ModelState);

            if (excelFile.Result != null)
            {
                //validate excel file 
                var errorMessages = excelFileProcessor.ValidateExelFile(excelFile.Result);
                if (errorMessages.Any(m => m.IsErrored))
                {
                    foreach (var message in errorMessages)
                    {
                        ModelState.AddModelError(message.Key,message.Message);
                    }
                    return View();
                }

                //process excel file 
                errorMessages = excelFileProcessor.ProcessExcelFile(excelFile.Result);
                if (errorMessages.Count > 0)
                {
                    foreach (var message in errorMessages.Where(m=>m.IsErrored == true))
                    {
                        ModelState.AddModelError(message.Key, message.Message);
                    }

                    var totalUploadedRecords = errorMessages.Count(m => m.IsErrored == false);
                    ViewData["UploadMessage"] = $"You successfully uploaded {totalUploadedRecords}";

                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("Internal Error",
                    $"Could not process the file, please try again.");
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ViewUploadedData()
        {
            ViewData["Message"] = "Uploaded data";
            ITransactionDataProvider transactionDataProvider = new TransactionDataProviderMemory(_cache);
            return View(transactionDataProvider.Get());
        }

 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
