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
    public class CardBrandUnitTest
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/CardBrand-management/";





        [Fact]
        public async Task Post_CardBrand_The_Name_Is_Required()
        {
            Guid guid = Guid.NewGuid();

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = string.Empty
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_CardBrand_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = Guid.NewGuid();

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "1"
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_CardBrand_The_Guid_is_empty()
        {
            CardBrand cardBrand = new CardBrand
            {
                Id = Guid.Empty,
                Name = "1"
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_CardBrand_The_Guid_is_invalid_and_contains_00000000()
        {
            CardBrand cardBrand = new CardBrand
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "1"
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_CardBrand_The_card_brand_id_has_already_been_taken_Run_2_Times()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740920FAA");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES..."
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_CardBrand_The_card_brand_name_has_already_been_taken_Run()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740820FAA");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES..."
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_CardBrand_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "TESTES..."
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PostCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand id has already been taken.", item),
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }





        [Fact]
        public async Task Put_CardBrand_The_Name_is_Required()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = string.Empty
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PutCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardBrand_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "o"
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PutCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardBrand_The_Guid_is_empty()
        {
            CardBrand cardBrand = new CardBrand
            {
                Id = Guid.Empty,
                Name = "oxx"
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PutCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardBrand_The_Guid_is_invalid_and_contains_00000000()
        {
            CardBrand cardBrand = new CardBrand
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "oxx"
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PutCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardBrand_The_card_brand_name_has_already_been_taken()
        {
            Guid guid = new Guid("7E210F4F-D12B-4F85-8464-6F8740920FAA");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "TESTES..."
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PutCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardBrand_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            CardBrand cardBrand = new CardBrand
            {
                Id = guid,
                Name = "ALTERADO..."
            };

            var service = new CardBrandService(_httpClient);
            var result = await service.PutCardBrand(requestUri, cardBrand);

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
                    item => Assert.Equal("The card brand name has already been taken.", item)
                );
            }
        }





        [Fact]        
        public async Task Get_AllCardBrand()
        {
            var service = new CardBrandService(_httpClient);
            var result = await service.GetAllCardBrands(requestUri);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK"); 
        }





        [Fact]
        public async Task Get_ByNameCardBrand()
        {
            string info = "ALTERADO...";

            var service = new CardBrandService(_httpClient);
            var result = await service.GetByNameCardBrand(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_ByIdCardBrand()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new CardBrandService(_httpClient);
            var result = await service.GetByIdCardBrand(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_HistorycByIdCardBrand()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new CardBrandService(_httpClient);
            var result = await service.GetHistorycByIdCardBrand(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Delete_ByIdCardBrand_The_Guid_is_empty()
        {
            Guid id = Guid.Empty;

            var service = new CardBrandService(_httpClient);
            var result = await service.DeleteByIdCardBrand(requestUri, id);

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

        [Fact]
        public async Task Delete_ByIdCardBrand_The_Guid_is_invalid_and_contains_00000000()
        {
            Guid id = new Guid("00000000-D12B-4F85-8464-6F8740920FAA");

            var service = new CardBrandService(_httpClient);
            var result = await service.DeleteByIdCardBrand(requestUri, id);

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

        [Fact]
        public async Task Delete_ByIdCardBrand_Registro_nao_encontrado()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-1F2340920FAA");

            var service = new CardBrandService(_httpClient);
            var result = await service.DeleteByIdCardBrand(requestUri, id);

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

        [Fact]
        public async Task Delete_ByIdCardBrand_Valido()
        {
            Guid id = new Guid("5e210f4f-d12b-4f85-8464-6f8740920faa");

            var service = new CardBrandService(_httpClient);
            var result = await service.DeleteByIdCardBrand(requestUri, id);

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
