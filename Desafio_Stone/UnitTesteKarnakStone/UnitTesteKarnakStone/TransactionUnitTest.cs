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
    public class TransactionUnitTest
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/Transaction-management/";





        [Fact(DisplayName = "CARTAO CHIP - COM SENHA")]
        public async Task Post_Transaction_CHIP()
        {
            Guid guid = Guid.NewGuid();




            
            #region Get Id Transaction Type

            string requestUriTransactionType = "https://localhost:44338/api/v1/TransactionType-management/";

            string cardTypeName = "Crédito";

            var serviceTransactionType = new TransactionTypeService(_httpClient);
            var resultTransactionType = await serviceTransactionType.GetByNameTransactionType(requestUriTransactionType, cardTypeName);

            Assert.False(resultTransactionType.Status != HttpStatusCode.OK || resultTransactionType.Data == null, "ERROR - Get Card Type");

            #endregion




            
            #region Get Id Card

            string requestUriCard = "https://localhost:44338/api/v1/Card-management/";

            string cardId = "92336F31-3138-467F-AD8F-461A490923AF";

            var serviceCard = new CardService(_httpClient);
            var resultCard = await serviceCard.GetByIdCard(requestUriCard, cardId);

            Assert.False(resultCard.Status != HttpStatusCode.OK || resultCard.Data == null, "ERROR - Get Card");

            #endregion




            Transaction transaction = new Transaction
            {
                Id = guid,
                Amount = 150,
                IdTransactionType = resultTransactionType.Data.Id,
                IdCard = resultCard.Data.Id,
                IdTransactionStatus = Guid.Empty,
                Number = 3,
                TransactionDate = DateTime.Now,
                Password = Common.StringCipher.Encrypt("985471", "StefanSilva@#@Stone##2019"),
                HasPassword = "true"
            };



            var service = new TransactionService(_httpClient);
            var result = await service.PostTransaction(requestUri, transaction);

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
                    item => Assert.Equal("Cartão bloqueado", item),
                    item => Assert.Equal("Senha Incorreta", item),
                    item => Assert.Equal("Password error size", item),
                    item => Assert.Equal("Saldo insuficiente", item),
                    item => Assert.Equal("Transação aprovada", item),
                    item => Assert.Equal("Password between 4 and 6 digits", item),
                    item => Assert.Equal("The amount must have greater than 10 cents", item),
                    item => Assert.Equal("The transacion guid is empty", item),
                    item => Assert.Equal("The transaction guid is invalid", item),
                    item => Assert.Equal("The transaction guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The transacion type guid is empty", item),
                    item => Assert.Equal("The transaction type guid is invalid", item),
                    item => Assert.Equal("The transaction type guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The transacion card guid is empty", item),
                    item => Assert.Equal("The transaction card guid is invalid", item),
                    item => Assert.Equal("The transaction card guid is invalid and contains 00000000", item)
                );
            }
        }



        [Fact(DisplayName = "CARTAO VENCIDO")]
        public async Task Post_Transaction_Cartao_Vencido()
        {

        }

        [Fact(DisplayName = "CARTAO TARJA - SEM SENHA")]
        public async Task Post_Transaction_TARJA()
        {
            Guid guid = Guid.NewGuid();





            #region Get Id Transaction Type

            string requestUriTransactionType = "https://localhost:44338/api/v1/TransactionType-management/";

            string cardTypeName = "Crédito";

            var serviceTransactionType = new TransactionTypeService(_httpClient);
            var resultTransactionType = await serviceTransactionType.GetByNameTransactionType(requestUriTransactionType, cardTypeName);

            Assert.False(resultTransactionType.Status != HttpStatusCode.OK || resultTransactionType.Data == null, "ERROR - Get Card Type");

            #endregion





            #region Get Id Card

            string requestUriCard = "https://localhost:44338/api/v1/Card-management/";

            string cardId = "92336F31-3138-467F-AD8F-461A490923AF";

            var serviceCard = new CardService(_httpClient);
            var resultCard = await serviceCard.GetByIdCard(requestUriCard, cardId);

            Assert.False(resultCard.Status != HttpStatusCode.OK || resultCard.Data == null, "ERROR - Get Card");

            #endregion




            Transaction transaction = new Transaction
            {
                Id = guid,
                Amount = 150,
                IdTransactionType = resultTransactionType.Data.Id,
                IdCard = resultCard.Data.Id,
                IdTransactionStatus = Guid.Empty,
                Number = 3,
                TransactionDate = DateTime.Now,
                Password = "",
                HasPassword = "false"
            };



            var service = new TransactionService(_httpClient);
            var result = await service.PostTransaction(requestUri, transaction);

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
                    item => Assert.Equal("Cartão bloqueado", item),
                    item => Assert.Equal("Senha Incorreta", item),
                    item => Assert.Equal("Password error size", item),
                    item => Assert.Equal("Saldo insuficiente", item),
                    item => Assert.Equal("Transação aprovada", item),
                    item => Assert.Equal("Password between 4 and 6 digits", item),
                    item => Assert.Equal("The amount must have greater than 10 cents", item),
                    item => Assert.Equal("The transacion guid is empty", item),
                    item => Assert.Equal("The transaction guid is invalid", item),
                    item => Assert.Equal("The transaction guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The transacion type guid is empty", item),
                    item => Assert.Equal("The transaction type guid is invalid", item),
                    item => Assert.Equal("The transaction type guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The transacion card guid is empty", item),
                    item => Assert.Equal("The transaction card guid is invalid", item),
                    item => Assert.Equal("The transaction card guid is invalid and contains 00000000", item)
                );
            }
        }




        //[Fact]
        //public async Task Put_Transaction_Valido()
        //{
        //    Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

        //    Transaction transaction = new Transaction
        //    {
        //        Id = guid,
        //        Name = "ALTERADO..."
        //    };

        //    var service = new TransactionService(_httpClient);
        //    var result = await service.PutTransaction(requestUri, transaction);

        //    if (result.Status == HttpStatusCode.OK)
        //    {
        //        Assert.True(result.Status == HttpStatusCode.OK, "OK");
        //    }
        //    else
        //    {
        //        List<string> listError = new List<string>();
        //        foreach (string error in result.Data)
        //        {
        //            listError.Add(error);
        //        }
        //        Assert.Collection(listError,
        //            item => Assert.Equal("The Name is Required", item),
        //            item => Assert.Equal("The Name must have between 2 and 30 characters", item),
        //            item => Assert.Equal("The Guid is empty", item),
        //            item => Assert.Equal("The Guid is invalid and contains 00000000", item),
        //            item => Assert.Equal("The card type name has already been taken.", item)
        //        );
        //    }
        //}





        [Fact]        
        public async Task Get_AllTransaction()
        {
            var service = new TransactionService(_httpClient);
            var result = await service.GetAllTransactions(requestUri);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK"); 
        }





        [Fact]
        public async Task Get_ByNameTransaction()
        {
            string info = "ALTERADO...";

            var service = new TransactionService(_httpClient);
            var result = await service.GetByNameTransaction(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_ByIdTransaction()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new TransactionService(_httpClient);
            var result = await service.GetByIdTransaction(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_HistorycByIdTransaction()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new TransactionService(_httpClient);
            var result = await service.GetHistorycByIdTransaction(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Delete_ByIdTransaction_Valido()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            var service = new TransactionService(_httpClient);
            var result = await service.DeleteByIdTransaction(requestUri, id);

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
