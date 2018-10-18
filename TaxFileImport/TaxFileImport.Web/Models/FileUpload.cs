using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TaxFileImport.Web.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "Tax File")]
        public IFormFile UploadTaxFile { get; set; }
    }
}
