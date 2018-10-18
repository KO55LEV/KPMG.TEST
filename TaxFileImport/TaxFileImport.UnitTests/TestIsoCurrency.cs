using AutoMapper;
using TaxFileImport.Core;
using TaxFileImport.Core.Model;
using Xunit;

namespace TaxFileImport.UnitTests
{

 

    public class TestIsoCurrency
    {
        

        [Fact]
        public void CheckEmptyIso4217()
        {
            //Arrange 
            var iso = string.Empty;
            //Act
            var testIsoService = new Iso4217DataProvider();
            var result = testIsoService.ValidateCode(iso);

            //Assert 
            Assert.False(result);
        }

        [Fact]
        public void CheckWrongIso4217()
        {
            //Arrange 
            var iso = "GBS";
            //Act
            var testIsoService = new Iso4217DataProvider();
            var result = testIsoService.ValidateCode(iso);

            //Assert 
            Assert.False(result);
        }

        [Fact]
        public void CheckCorrectIso4217()
        {
            //Arrange 
            var iso = "GBP";
            //Act
            var testIsoService = new Iso4217DataProvider();
            var result = testIsoService.ValidateCode(iso);

            //Assert 
            Assert.True(result);
        }
    }
}
