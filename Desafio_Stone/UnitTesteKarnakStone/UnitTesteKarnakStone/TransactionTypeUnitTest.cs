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
    public class TransactionTypeUnitTest
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/TransactionType-management/";





        [Fact]
        public async Task Post_TransactionType_The_Name_Is_Required()
        {
            Guid guid = Guid.NewGuid();

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = string.Empty
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_TransactionType_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = Guid.NewGuid();

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "1"
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_TransactionType_The_Guid_is_empty()
        {
            TransactionType transactionType = new TransactionType
            {
                Id = Guid.Empty,
                Name = "1"
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_TransactionType_The_Guid_is_invalid_and_contains_00000000()
        {
            TransactionType transactionType = new TransactionType
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "1"
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_TransactionType_The_transaction_type_id_has_already_been_taken_Run_2_Times()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740920FAA");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES..."
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_TransactionType_The_transaction_type_name_has_already_been_taken()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740820FAA");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES..."
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_TransactionType_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "TESTES..."
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PostTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type id has already been taken.", item),
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }





        [Fact]
        public async Task Put_TransactionType_The_Name_is_Required()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = string.Empty
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PutTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_TransactionType_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "o"
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PutTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_TransactionType_The_Guid_is_empty()
        {
            TransactionType transactionType = new TransactionType
            {
                Id = Guid.Empty,
                Name = "oxx"
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PutTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_TransactionType_The_Guid_is_invalid_and_contains_00000000()
        {
            TransactionType transactionType = new TransactionType
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "oxx"
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PutTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_TransactionType_The_transaction_type_name_has_already_been_taken()
        {
            Guid guid = new Guid("6E210F4F-D12B-4F85-8464-6F8740920FAA");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "TESTES..."
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PutTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_TransactionType_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            TransactionType transactionType = new TransactionType
            {
                Id = guid,
                Name = "ALTERADO..."
            };

            var service = new TransactionTypeService(_httpClient);
            var result = await service.PutTransactionType(requestUri, transactionType);

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
                    item => Assert.Equal("The transaction type name has already been taken.", item)
                );
            }
        }





        [Fact]        
        public async Task Get_AllTransactionType()
        {
            var service = new TransactionTypeService(_httpClient);
            var result = await service.GetAllTransactionTypes(requestUri);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK"); 
        }





        [Fact]
        public async Task Get_ByNameTransactionType()
        {
            string info = "ALTERADO...";

            var service = new TransactionTypeService(_httpClient);
            var result = await service.GetByNameTransactionType(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_ByIdTransactionType()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new TransactionTypeService(_httpClient);
            var result = await service.GetByIdTransactionType(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_HistorycByIdTransactionType()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new TransactionTypeService(_httpClient);
            var result = await service.GetHistorycByIdTransactionType(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Delete_ByIdTransactionType_The_Guid_is_empty()
        {
            Guid id = Guid.Empty;

            var service = new TransactionTypeService(_httpClient);
            var result = await service.DeleteByIdTransactionType(requestUri, id);

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
        public async Task Delete_ByIdTransactionType_The_Guid_is_invalid_and_contains_00000000()
        {
            Guid id = new Guid("00000000-D12B-4F85-8464-6F8740920FAA");

            var service = new TransactionTypeService(_httpClient);
            var result = await service.DeleteByIdTransactionType(requestUri, id);

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
        public async Task Delete_ByIdTransactionType_Registro_nao_encontrado()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-1F2340920FAA");

            var service = new TransactionTypeService(_httpClient);
            var result = await service.DeleteByIdTransactionType(requestUri, id);

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
        public async Task Delete_ByIdTransactionType_Valido()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            var service = new TransactionTypeService(_httpClient);
            var result = await service.DeleteByIdTransactionType(requestUri, id);

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
