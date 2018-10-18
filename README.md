# KPMG.TEST


## Getting Started

Solution has 3 projects. Web project - MVC application with 3 pages.  Default page to upload file. To see uploaded data navigate to "ViewUploadedData". 
Currently data is stored in memory but created dummy classes for future implementation for Entity Framework or SQL/MySQL data providers. 

Iso loaded from json data file through iso data provider (could be changed to any iso provider)

Unit tests are in project TaxFileImport.UnitTests

Project written on .NET Core.   + VS 2017. 

Should be run without any preparations. 

The EXCEL file should be in format: 
```
Account|Description|Currency Code|Amount
```




## Test Data

* [XLSX Example with 20 Transactions](https://github.com/KO55LEV/KPMG.TEST/blob/master/TaxFileImport/TaxFileImport.UnitTests/TestFiles/20_transactions.xlsx) - Example to upload xlsx file. Used in testing
* [Excel example with wrong Account column](https://github.com/KO55LEV/KPMG.TEST/blob/master/TaxFileImport/TaxFileImport.UnitTests/TestFiles/transactions_no_account.xlsx) - Example where Account column not normaly formated
* [Excel example with wrong Description column](https://github.com/KO55LEV/KPMG.TEST/blob/master/TaxFileImport/TaxFileImport.UnitTests/TestFiles/transactions_no_descrip.xlsx) - Example where Description column not normaly formated

