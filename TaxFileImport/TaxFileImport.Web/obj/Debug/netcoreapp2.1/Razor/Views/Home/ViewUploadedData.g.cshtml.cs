#pragma checksum "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e6aeefa30656f8a45e52e4fe6c2c264ea4bb0853"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ViewUploadedData), @"mvc.1.0.view", @"/Views/Home/ViewUploadedData.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/ViewUploadedData.cshtml", typeof(AspNetCore.Views_Home_ViewUploadedData))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\_ViewImports.cshtml"
using TaxFileImport.Web;

#line default
#line hidden
#line 2 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\_ViewImports.cshtml"
using TaxFileImport.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e6aeefa30656f8a45e52e4fe6c2c264ea4bb0853", @"/Views/Home/ViewUploadedData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7c68b91a98b0ba6563334ddcd83bdadc011daf8a", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ViewUploadedData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<TaxFileImport.Core.Model.Transaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(51, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 4 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml"
 foreach (var transaction in Model)
{

#line default
#line hidden
            BeginContext(95, 31, true);
            WriteLiteral("<div class=\"alert-success\">\r\n  ");
            EndContext();
            BeginContext(127, 49, false);
#line 7 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml"
Write(Html.DisplayFor(modelItem => transaction.Account));

#line default
#line hidden
            EndContext();
            BeginContext(176, 4, true);
            WriteLiteral("\r\n  ");
            EndContext();
            BeginContext(181, 53, false);
#line 8 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml"
Write(Html.DisplayFor(modelItem => transaction.Description));

#line default
#line hidden
            EndContext();
            BeginContext(234, 4, true);
            WriteLiteral("\r\n  ");
            EndContext();
            BeginContext(239, 54, false);
#line 9 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml"
Write(Html.DisplayFor(modelItem => transaction.CurrencyCode));

#line default
#line hidden
            EndContext();
            BeginContext(293, 4, true);
            WriteLiteral("\r\n  ");
            EndContext();
            BeginContext(298, 48, false);
#line 10 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml"
Write(Html.DisplayFor(modelItem => transaction.Amount));

#line default
#line hidden
            EndContext();
            BeginContext(346, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
#line 12 "C:\Dev\TestProjects\kpmg\TaxFileImport\TaxFileImport.Web\Views\Home\ViewUploadedData.cshtml"

}

#line default
#line hidden
            BeginContext(361, 9, true);
            WriteLiteral("\r\n       ");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<TaxFileImport.Core.Model.Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591