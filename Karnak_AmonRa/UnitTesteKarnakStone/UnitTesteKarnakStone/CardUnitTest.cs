using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnitTesteKarnakStone.Models;
using UnitTesteKarnakStone.Services;
using Xunit;

namespace UnitTesteKarnakStone
{
    public class CardUnitTest
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/Card-management/";





        [Fact]
        public async Task Post_Card_Valido()
        {
            Guid guid = Guid.NewGuid();


           
            
            
            #region Get Id Card Type From Name

            string cardTypeName = "Chip";

            var serviceCardType = new CardService(_httpClient);
            var resultCardType = await serviceCardType.GetCardTypeByName(requestUri, cardTypeName);

            Assert.False(resultCardType.Status != HttpStatusCode.OK || resultCardType.Data == null, "ERROR - Get Card Type");

            #endregion





            #region Get Id Customer From Name

            string customerName = "Stefan Robinson da Silva";

            var serviceCustomer = new CardService(_httpClient);
            var resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);

            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            #endregion





            #region Get Id Card Brand From Name

            string cardBrand = "Visa";

            var serviceCardBrand = new CardService(_httpClient);
            var resultCardBrand = await serviceCardBrand.GetCardBrandByName(requestUri, cardBrand);

            Assert.False(resultCardBrand.Status != HttpStatusCode.OK || resultCardBrand.Data == null, "ERROR - Get Brand");


            #endregion




            #region Create Card

            Card card = new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024007196111834",
                ExpirationDate = new DateTime(2022, 04, 25),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 15000,
                LimitAvailable = 15000,
                Attempts = 0,
                Blocked = 0
            };

            var serviceCard = new CardService(_httpClient);
            var result = await serviceCard.PostCard(requestUri, card);

            if (result.Status == HttpStatusCode.OK)
            {
                Assert.True(result.Status == HttpStatusCode.OK, "OK");
            }
            else
            {
                List<string> listError = new List<string>();
                foreach (string error in result.Data)
                {
                    listError.Add(error);
                }
                Assert.Collection(listError,
                    item => Assert.Equal("The card number has already been taken.", item),
                    item => Assert.Equal("The card id has already been taken.", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The guid is invalid to card type", item),
                    item => Assert.Equal("The guid is invalid to customer", item),
                    item => Assert.Equal("The guid is invalid to brand", item), 
                    item => Assert.Equal("The card number must have between 12 and 19 digits", item),
                    item => Assert.Equal("The expiration date must have greater than today", item),
                    item => Assert.Equal("The password length must have between 4 and 6 digits", item),
                    item => Assert.Equal("The password is invalid", item),
                    item => Assert.Equal("The limit must have greater than zero", item)
                );
            }

            #endregion







        }





        [Fact]        
        public async Task Get_AllCard()
        {
            var service = new CardService(_httpClient);
            var result = await service.GetAllCards(requestUri);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK"); 
        }





        [Fact]
        public async Task Get_ByIdCard()
        {
            string info = "8F9B6C5A-CF0C-426A-A18A-071667E51E77";

            var service = new CardService(_httpClient);
            var result = await service.GetByIdCard(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_ByCardNumber()
        {
            string info = "4024007196111834";

            var service = new CardService(_httpClient);
            var result = await service.GetByCardNumber(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_HistorycByIdCard()
        {
            string info = "8F9B6C5A-CF0C-426A-A18A-071667E51E77";

            var service = new CardService(_httpClient);
            var result = await service.GetHistorycByIdCard(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Delete_ByIdCard_Valido()
        {
            Guid id = new Guid("8F9B6C5A-CF0C-426A-A18A-071667E51E77");

            var service = new CardService(_httpClient);
            var result = await service.DeleteByIdCard(requestUri, id);

            // Assert
            if (result.Status == HttpStatusCode.OK)
            {
                Assert.True(result.Status == HttpStatusCode.OK, "OK");
            }
            else
            {
                List<string> listError = new List<string>();
                foreach (string error in result.Data)
                {
                    listError.Add(error);
                }
                Assert.Collection(listError,
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("Registro não encontrado", item)
                );
            }
        }


    }
}
