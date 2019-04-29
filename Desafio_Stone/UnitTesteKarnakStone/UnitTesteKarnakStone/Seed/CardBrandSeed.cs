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
    public class CardBrandSeed
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/CardBrand-management/";

        [Fact]
        public async Task Post_CardBrand_Seed()
        {
            List<CardBrand> listCardBrand = new List<CardBrand>();

            listCardBrand.Add(new CardBrand
            {
                Id = Guid.NewGuid(),
                Name = "Visa"
            });

            listCardBrand.Add(new CardBrand
            {
                Id = Guid.NewGuid(),
                Name = "Master"
            });

            listCardBrand.Add(new CardBrand
            {
                Id = Guid.NewGuid(),
                Name = "Amex"
            });

            foreach (CardBrand cardType in listCardBrand)
            {
                var service = new CardBrandService(_httpClient);
                var result = await service.PostCardBrand(requestUri, cardType);

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
