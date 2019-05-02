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
    public class TransactionStatusSeed
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string requestUri = "https://localhost:44338/api/v1/TransactionStatus-management/";

        [Fact]
        public async Task Post_TransactionStatus_Seed()
        {
            List<TransactionStatus> listTransactionStatus = new List<TransactionStatus>();

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("9B8745FD-BDA2-41D3-8F49-8C893461FBE1"),
                Name = "Valor inválido"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("47748064-E751-4E2C-93DC-E4A8FF2388BC"),
                Name = "Transação negada"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("105DCD64-4498-4B06-8A74-914D1020DBE4"),
                Name = "Transação aprovada"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("D5117C58-2962-4B2A-938E-05698AA76A2B"),
                Name = "Senha inválida"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("FCD03359-22A7-41E3-B9DE-70F8FBA47805"),
                Name = "Senha Incorreta"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("7614057A-AB4B-4447-82D5-EF88A097CBD4"),
                Name = "Erro no tamanho da senha"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("D87358AF-6800-4545-BB11-A6114627DF6A"),
                Name = "Saldo insuficiente"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("A92807BA-A7A8-4BFF-B198-502A26306E53"),
                Name = "Cartão bloqueado"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("61F61682-65B9-424A-BB1B-7B04B3264114"),
                Name = "Aprovado"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("A73D8B79-8985-4CF8-AC2C-5C4AA4B84BA5"),
                Name = "Registro não encontrado"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("E23F6900-CB87-475A-9DBE-E490552967C3"),
                Name = "Senha entre 4 e 6 dítigos"
            });

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("526F6B9A-2248-49AE-8119-56F03C9A9824"),
                Name = "Mínimo de 10 centavos"
            });     

            listTransactionStatus.Add(new TransactionStatus
            {
                Id = new Guid("7614057a-ab4b-4447-82d5-ef88a097cbd9"),
                Name = "Cartão vencido"
            });

            foreach (TransactionStatus cardType in listTransactionStatus)
            {
                var service = new TransactionStatusService(_httpClient);
                var result = await service.PostTransactionStatus(requestUri, cardType);

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
