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
    public class TransactionTypeSeed
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/TransactionType-management/";

        [Fact]
        public async Task Post_TransactionType_Seed()
        {
            List<TransactionType> listTransactionType = new List<TransactionType>();

            listTransactionType.Add(new TransactionType
            {
                Id = Guid.NewGuid(),
                Name = "Crédito"
            });

            listTransactionType.Add(new TransactionType
            {
                Id = Guid.NewGuid(),
                Name = "Débito"
            });

            foreach (TransactionType cardType in listTransactionType)
            {
                var service = new TransactionTypeService(_httpClient);
                var result = await service.PostTransactionType(requestUri, cardType);

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
                        item => Assert.Equal("The Name is Required", item),
                        item => Assert.Equal("The Name must have between 2 and 30 characters", item),
                        item => Assert.Equal("The Guid is empty", item),
                        item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                        item => Assert.Equal("The card type id has already been taken.", item),
                        item => Assert.Equal("The card type name has already been taken.", item)
                    );
                }
            }
        }
    }
}
