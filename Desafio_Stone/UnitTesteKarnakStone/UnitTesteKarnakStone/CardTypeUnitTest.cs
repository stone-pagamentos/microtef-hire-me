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
    public class CardTypeUnitTest
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/cardtype-management/";





        [Fact]
        public async Task Post_CardType_The_Name_Is_Required()
        {
            Guid guid = Guid.NewGuid();

            CardType cardType = new CardType
            {
                Id = guid,
                Name = string.Empty
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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

        [Fact]
        public async Task Post_CardType_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = Guid.NewGuid();

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "1"
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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

        [Fact]
        public async Task Post_CardType_The_Guid_is_empty()
        {
            CardType cardType = new CardType
            {
                Id = Guid.Empty,
                Name = "1"
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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

        [Fact]
        public async Task Post_CardType_The_Guid_is_invalid_and_contains_00000000()
        {
            CardType cardType = new CardType
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "1"
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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

        [Fact]
        public async Task Post_CardType_The_card_type_id_has_already_been_taken_Run_2_Times()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740920FAA");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES..."
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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

        [Fact]
        public async Task Post_CardType_The_card_type_name_has_already_been_taken_Run()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740820FAA");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES..."
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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

        [Fact]
        public async Task Post_CardType_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "TESTES..."
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PostCardType(requestUri, cardType);

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





        [Fact]
        public async Task Put_CardType_The_Name_is_Required()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = string.Empty
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PutCardType(requestUri, cardType);

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
                    item => Assert.Equal("The card type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardType_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "o"
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PutCardType(requestUri, cardType);

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
                    item => Assert.Equal("The card type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardType_The_Guid_is_empty()
        {
            CardType cardType = new CardType
            {
                Id = Guid.Empty,
                Name = "oxx"
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PutCardType(requestUri, cardType);

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
                    item => Assert.Equal("The card type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardType_The_Guid_is_invalid_and_contains_00000000()
        {
            CardType cardType = new CardType
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "oxx"
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PutCardType(requestUri, cardType);

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
                    item => Assert.Equal("The card type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardType_The_card_type_name_has_already_been_taken()
        {
            Guid guid = new Guid("7E210F4F-D12B-4F85-8464-6F8740920FAA");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "TESTES..."
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PutCardType(requestUri, cardType);

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
                    item => Assert.Equal("The card type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_CardType_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            CardType cardType = new CardType
            {
                Id = guid,
                Name = "ALTERADO..."
            };

            var service = new CardTypeService(_httpClient);
            var result = await service.PutCardType(requestUri, cardType);

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
                    item => Assert.Equal("The card type name has already been taken.", item)
                );
            }
        }





        [Fact]        
        public async Task Get_AllCardType()
        {
            var service = new CardTypeService(_httpClient);
            var result = await service.GetAllCardTypes(requestUri);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK"); 
        }





        [Fact]
        public async Task Get_ByNameCardType()
        {
            string info = "ALTERADO...";

            var service = new CardTypeService(_httpClient);
            var result = await service.GetByNameCardType(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_ByIdCardType()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new CardTypeService(_httpClient);
            var result = await service.GetByIdCardType(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_HistorycByIdCardType()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new CardTypeService(_httpClient);
            var result = await service.GetHistorycByIdCardType(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Delete_ByIdCardType_The_Guid_is_empty()
        {
            Guid id = Guid.Empty;

            var service = new CardTypeService(_httpClient);
            var result = await service.DeleteByIdCardType(requestUri, id);

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
        public async Task Delete_ByIdCardType_The_Guid_is_invalid_and_contains_00000000()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            var service = new CardTypeService(_httpClient);
            var result = await service.DeleteByIdCardType(requestUri, id);

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
        public async Task Delete_ByIdCardType_Registro_nao_encontrado()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-1F2340920FAA");

            var service = new CardTypeService(_httpClient);
            var result = await service.DeleteByIdCardType(requestUri, id);

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
        public async Task Delete_ByIdCardType_Valido()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            var service = new CardTypeService(_httpClient);
            var result = await service.DeleteByIdCardType(requestUri, id);

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
