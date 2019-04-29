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
    public class CustomerSeed
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/customer-management/";

        [Fact]
        public async Task Post_Customer_Seed()
        {
            List<Customer> listCustomer = new List<Customer>();

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1980, 1, 20, 11, 14, 59),
                    Email = "silva.stefan@gmail.com",
                    Name = "Stefan Robinson da Silva"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1981, 2, 21, 12, 14, 59),
                    Email = "melo.delicia@gmail.com",
                    Name = "Delícia Costa Melo"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1982, 3, 22, 13, 14, 59),
                    Email = "jose.joao@gmail.com",
                    Name = "João Cara De José"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1983, 4, 23, 14, 14, 59),
                    Email = "sobrenome.joao@gmail.com",
                    Name = "João Sem Sobrenome"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1984, 5, 24, 15, 14, 59),
                    Email = "dequem.maria@gmail.com",
                    Name = "Maria Bastarda Dequem"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1985, 6, 25, 16, 14, 59),
                    Email = "bundasseca.otavio@gmail.com",
                    Name = "Otávio Bundasseca"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1986, 7, 26, 17, 14, 59),
                    Email = "furtado.renato@gmail.com",
                    Name = "Renato Pordeus Furtado"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1987, 8, 27, 18, 14, 59),
                    Email = "osso.vitoria@gmail.com",
                    Name = "Vitória Carne e Osso"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1988, 9, 28, 19, 14, 59),
                    Email = "pato.manuel@gmail.com",
                    Name = "Manuel Sola De Sá Pato"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "coitadinho.inocencio@gmail.com",
                    Name = "Inocêncio Coitadinho"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "joao.amaral@gmail.com",
                    Name = "João do Amaral"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "paulo.rufino@gmail.com",
                    Name = "Paulo Rufino"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "paulo.viola@gmail.com",
                    Name = "Paulinho da Viola"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "maria.fernanda@gmail.com",
                    Name = "Maria Fernanda"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "fabiola.pereira@gmail.com",
                    Name = "Fibiola Pereira"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "patricia.fernandes@gmail.com",
                    Name = "Patricia Fernandes"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "josue.pinto@gmail.com",
                    Name = "Josue Silva Pinto"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "joaopedro.silva@gmail.com",
                    Name = "Joao Pedro Silva"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "natanael.oliveira@gmail.com",
                    Name = "Natanael Oliveira"
                }
            );

            listCustomer.Add(
                new Customer
                {
                    Id = Guid.NewGuid(),
                    BirthDate = new DateTime(1989, 10, 29, 20, 14, 59),
                    Email = "fernando.fernandes@gmail.com",
                    Name = "Fernando Fernandes"
                }
            );

            foreach (Customer cardType in listCustomer)
            {
                var service = new CustomerService(_httpClient);
                var result = await service.PostCustomer(requestUri, cardType);

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
