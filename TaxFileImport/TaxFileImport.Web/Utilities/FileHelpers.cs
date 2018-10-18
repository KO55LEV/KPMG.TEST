using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaxFileImport.Core;
using TaxFileImport.Core.Model;
using TaxFileImport.Web.Models;

namespace TaxFileImport.Web.Utilities
{

    public class FileHelpers
    {
     
        public async static Task<ExcelFile> GetExcelFile(
            IFormFile formFile, ModelStateDictionary modelState)
        {
            var fieldDisplayName = string.Empty;


            MemberInfo property =
                typeof(FileUpload).GetProperty(formFile.Name.Substring(
                    formFile.Name.IndexOf(".") + 1));

            if (property != null)
            {
                var displayAttribute =
                    property.GetCustomAttribute(typeof(DisplayAttribute))
                        as DisplayAttribute;

                if (displayAttribute != null)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }


            var fileName = WebUtility.HtmlEncode(
                Path.GetFileName(formFile.FileName));

            if (formFile.ContentType.ToLower() != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName} file ({fileName}) must be a .xlsx file.");
            }

            // Check the file length 
            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName} file ({fileName}) is empty.");
            }
            else if (formFile.Length > 20971520)
            {
                modelState.AddModelError(formFile.Name,
                    $"The {fieldDisplayName} file ({fileName}) exceeds 20 MB.");
            }
            else
            {
                try
                {
                    string fileContents;

                    //load file 
                    ExcelFile ef;
                    using (var reader = formFile.OpenReadStream())
                    {
                        ef = ExcelFile.Load(reader, LoadOptions.XlsxDefault);
                    }

                    return ef;

                }
                catch (Exception ex)
                {
                    modelState.AddModelError(formFile.Name,
                        $"The {fieldDisplayName} file ({fileName}) upload failed. " +
                        $"Error: {ex.Message}");
                    // Log the exception
                }
            }

            return null;
        }

       

    
    }

}