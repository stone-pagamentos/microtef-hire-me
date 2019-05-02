using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnitTesteKarnakStone.Models;
using UnitTesteKarnakStone.Services;
using Xunit;

namespace UnitTesteKarnakStone.Seed
{
    public class CardMasterTarjaSeed
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/Card-management/";

        [Fact]
        public async Task Post_Card_Seed()
        {





            #region Get Id Card Type From Name

            string cardTypeName = "Tarja";

            var serviceCardType = new CardService(_httpClient);
            var resultCardType = await serviceCardType.GetCardTypeByName(requestUri, cardTypeName);

            Assert.False(resultCardType.Status != HttpStatusCode.OK || resultCardType.Data == null, "ERROR - Get Card Type");

            #endregion





            #region Get Id Card Brand From Name

            string cardBrand = "Master";

            var serviceCardBrand = new CardService(_httpClient);
            var resultCardBrand = await serviceCardBrand.GetCardBrandByName(requestUri, cardBrand);

            Assert.False(resultCardBrand.Status != HttpStatusCode.OK || resultCardBrand.Data == null, "ERROR - Get Brand");


            #endregion



            List<Card> listCard = new List<Card>();

            #region Customer Manuel Sola De Sá Pato

            string customerName = "Manuel Sola De Sá Pato";
            var serviceCustomer = new CardService(_httpClient);
            var resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024007196111367",
                ExpirationDate = new DateTime(2022, 04, 25),
                HasPassword = 0,
                Password = "",
                Limit = 5000,
                LimitAvailable = 5000,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Inocêncio Coitadinho

            customerName = "Inocêncio Coitadinho";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152763",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 0,
                Password = "",
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region João do Amaral

            customerName = "João do Amaral";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207142153481",
                ExpirationDate = new DateTime(2019, 12, 11),
                HasPassword = 0,
                Password = "",
                Limit = 22500,
                LimitAvailable = 22500,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Paulo Rufino

            customerName = "Paulo Rufino";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207142113372",
                ExpirationDate = new DateTime(2027, 01, 11),
                HasPassword = 0,
                Password = "",
                Limit = 33500,
                LimitAvailable = 33500,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            foreach (Card card in listCard)
            {
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
            }
        }
    }
}
