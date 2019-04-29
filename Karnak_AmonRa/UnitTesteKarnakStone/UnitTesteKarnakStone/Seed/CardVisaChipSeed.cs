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
    public class CardVisaChipSeed
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/Card-management/";

        [Fact]
        public async Task Post_Card_Seed()
        {



            
            
            #region Get Id Card Type From Name

            string cardTypeName = "Chip";

            var serviceCardType = new CardService(_httpClient);
            var resultCardType = await serviceCardType.GetCardTypeByName(requestUri, cardTypeName);

            Assert.False(resultCardType.Status != HttpStatusCode.OK || resultCardType.Data == null, "ERROR - Get Card Type");

            #endregion



            
            
            #region Get Id Card Brand From Name

            string cardBrand = "Visa";

            var serviceCardBrand = new CardService(_httpClient);
            var resultCardBrand = await serviceCardBrand.GetCardBrandByName(requestUri, cardBrand);

            Assert.False(resultCardBrand.Status != HttpStatusCode.OK || resultCardBrand.Data == null, "ERROR - Get Brand");


            #endregion



            List<Card> listCard = new List<Card>();

            #region Customer João Cara De José

            string customerName = "João Cara De José";
            var serviceCustomer = new CardService(_httpClient);
            var resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024007196111834",
                ExpirationDate = new DateTime(2022, 04, 25),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 5000,
                LimitAvailable = 5000,
                Attempts = 0,
                Blocked = 1
            });

            #endregion                      

            #region Customer Stefan Robinson da Silva

            customerName = "Stefan Robinson da Silva";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207172152134",
                ExpirationDate = new DateTime(2025, 02, 14),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 17250,
                LimitAvailable = 17250,
                Attempts = 2,
                Blocked = 1
            });

            #endregion

            #region Customer Maria Bastarda Dequem

            customerName = "Maria Bastarda Dequem";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152132",
                ExpirationDate = new DateTime(2024, 09, 17),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 2,
                Blocked = 1
            });

            #endregion

            #region Customer João Sem Sobrenome

            customerName = "João Sem Sobrenome";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152749",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 1,
                Blocked = 1
            });

            #endregion

            #region Customer Delícia Costa Melo

            customerName = "Delícia Costa Melo";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152197",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 3,
                Blocked = 0
            });

            #endregion

            #region Renato Pordeus Furtado

            customerName = "Renato Pordeus Furtado";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152468",
                ExpirationDate = new DateTime(2018, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 3,
                Blocked = 0
            });

            #endregion




            #region Paulinho da Viola

            customerName = "Paulinho da Viola";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152321",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Maria Fernanda

            customerName = "Maria Fernanda";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152322",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Fibiola Pereira

            customerName = "Fibiola Pereira";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024207342152522",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Patricia Fernandes

            customerName = "Patricia Fernandes";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024217342158522",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Josue Silva Pinto

            customerName = "Josue Silva Pinto";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024217762158522",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Joao Pedro Silva

            customerName = "Joao Pedro Silva";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024217768158522",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Natanael Oliveira

            customerName = "Natanael Oliveira";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024217768458512",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
                Attempts = 0,
                Blocked = 1
            });

            #endregion

            #region Fernando Fernandes

            customerName = "Fernando Fernandes";
            serviceCustomer = new CardService(_httpClient);
            resultCustomer = await serviceCustomer.GetCustomerByName(requestUri, customerName);
            Assert.False(resultCustomer.Status != HttpStatusCode.OK || resultCustomer.Data == null, "ERROR - Get Customer");

            listCard.Add(new Card
            {
                Id = Guid.NewGuid(),
                IdCardType = resultCardType.Data.Id,
                IdCustomer = resultCustomer.Data.Id,
                IdBrand = resultCardBrand.Data.Id,
                CardNumber = "4024216768358512",
                ExpirationDate = new DateTime(2022, 08, 11),
                HasPassword = 1,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                Limit = 2250,
                LimitAvailable = 2250,
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
