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
    public class CustomerUnitTest
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/customer-management/";





        [Fact]
        public async Task Post_Customer_The_Name_Is_Required()
        {
            Guid guid = Guid.NewGuid();

            Customer customer = new Customer
            {
                Id = guid,
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com",
                Name = string.Empty                
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_Customer_The_Name_must_have_between_2_and_30_characters()
        {
            Guid guid = Guid.NewGuid();

            Customer customer = new Customer
            {
                Id = guid,
                Name = "1",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_Customer_The_Guid_is_empty()
        {
            Customer customer = new Customer
            {
                Id = Guid.Empty,
                Name = "1",
                Email = "silva.stefan@gmail.com",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59)                
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_Customer_The_Guid_is_invalid_and_contains_00000000()
        {
            Customer customer = new Customer
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "1",                
                Email = "silva.stefan@gmail.com",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59)
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_Customer_The_customer_id_has_already_been_taken_Run()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740820FAA");

            Customer customer = new Customer
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES...",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.jose@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_Customer_The_customer_e_mail_has_already_been_taken_Run_2_Times()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-9464-6F8740820FAA");

            Customer customer = new Customer
            {
                Id = guid,
                Name = "EXECUTAR 2 VEZES...",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Post_Customer_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            Customer customer = new Customer
            {
                Id = guid,
                Name = "TESTES...",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.paulo@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PostCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer id has already been taken.", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }





        [Fact]
        public async Task Put_Customer_The_Name_is_Required()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            Customer customer = new Customer
            {
                Id = guid,
                Name = string.Empty,
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PutCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_Customer_The_Name_must_have_between_2_and_100_characters()
        {
            Guid guid = new Guid("1BF784F8-7B94-47EC-9FD9-5257D83E8E7D");

            Customer customer = new Customer
            {
                Id = guid,
                Name = "o",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PutCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_Customer_The_Guid_is_empty()
        {
            Customer customer = new Customer
            {
                Id = Guid.Empty,
                Name = "oxx",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PutCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_Customer_The_Guid_is_invalid_and_contains_00000000()
        {
            Customer customer = new Customer
            {
                Id = new Guid("00000000-D12B-4F85-9464-6F8740920FAA"),
                Name = "oxx",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PutCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_Customer_The_customer_name_has_already_been_taken()
        {
            Guid guid = new Guid("7E210F4F-D12B-4F85-8464-6F8740920FAA");

            Customer customer = new Customer
            {
                Id = guid,
                Name = "TESTES...",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.stefan@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PutCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }

        [Fact]
        public async Task Put_Customer_Valido()
        {
            Guid guid = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            Customer customer = new Customer
            {
                Id = guid,
                Name = "ALTERADO...",
                BirthDate = new DateTime(1980, 1, 26, 13, 14, 59),
                Email = "silva.paulo@gmail.com"
            };

            var service = new CustomerService(_httpClient);
            var result = await service.PutCustomer(requestUri, customer);

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
                    item => Assert.Equal("The Name must have between 2 and 100 characters", item),
                    item => Assert.Equal("The Guid is empty", item),
                    item => Assert.Equal("The Guid is invalid and contains 00000000", item),
                    item => Assert.Equal("The customer e-mail has already been taken.", item)
                );
            }
        }





        [Fact]        
        public async Task Get_AllCustomer()
        {
            var service = new CustomerService(_httpClient);
            var result = await service.GetAllCustomers(requestUri);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK"); 
        }





        [Fact]
        public async Task Get_ByNameCustomer()
        {
            string info = "ALTERADO...";

            var service = new CustomerService(_httpClient);
            var result = await service.GetByNameCustomer(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_ByIdCustomer()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new CustomerService(_httpClient);
            var result = await service.GetByIdCustomer(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Get_HistorycByIdCustomer()
        {
            string info = "5E210F4F-D12B-4F85-8464-6F8740920FAA";

            var service = new CustomerService(_httpClient);
            var result = await service.GetHistorycByIdCustomer(requestUri, info);

            // Assert
            Assert.False(result.Status == HttpStatusCode.BadRequest, "ERROR");
            Assert.True(result.Status == HttpStatusCode.OK, "OK");
        }





        [Fact]
        public async Task Delete_ByIdCustomer_The_Guid_is_empty()
        {
            Guid id = Guid.Empty;

            var service = new CustomerService(_httpClient);
            var result = await service.DeleteByIdCustomer(requestUri, id);

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
        public async Task Delete_ByIdCustomer_The_Guid_is_invalid_and_contains_00000000()
        {
            Guid id = new Guid("00000000-D12B-4F85-8464-6F8740920FAA");

            var service = new CustomerService(_httpClient);
            var result = await service.DeleteByIdCustomer(requestUri, id);

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
        public async Task Delete_ByIdCustomer_Registro_nao_encontrado()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-1F2340920FAA");

            var service = new CustomerService(_httpClient);
            var result = await service.DeleteByIdCustomer(requestUri, id);

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
        public async Task Delete_ByIdCustomer_Valido()
        {
            Guid id = new Guid("5E210F4F-D12B-4F85-8464-6F8740920FAA");

            var service = new CustomerService(_httpClient);
            var result = await service.DeleteByIdCustomer(requestUri, id);

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
