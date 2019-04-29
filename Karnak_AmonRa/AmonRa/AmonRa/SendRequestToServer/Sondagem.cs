using AmonRa.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class Sondagem
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = "https://localhost:44338/api/v1/Transaction-management/SondagemTransacoes?cardNumber=";

        public static async Task<dynamic> Information(string cardNumber)
        {
            var serviceSondagem = new Services.SondagemService(_httpClient);
            var resultSondagem = await serviceSondagem.Sondagem(requestUri + cardNumber);

            List<Models.TransactionList> transactionList = new List<Models.TransactionList>();

            foreach (var item in resultSondagem.Data)
            {
                transactionList.Add(new Models.TransactionList
                {
                    // Transaction
                    TransactionId = item.TransactionId,
                    TransactionCardId = item.TransactionCardId,
                    TransactionAmount = item.TransactionAmount,
                    TransactionNumber = item.TransactionNumber,
                    TransactionDate = item.TransactionDate,

                    // Transaction Type
                    TransactionTypeId = item.TransactionTypeId,
                    TransactionTypeName = item.TransactionTypeName,

                    // Transaction Status
                    TransactionStatusId = item.TransactionStatusId,
                    TransactionStatusName = item.TransactionStatusName,

                    // Card Brand
                    CardBrandId = item.CardBrandId,
                    CardBrandName = item.CardBrandName,

                    // Card Type
                    CardTypeId = item.CardTypeId,
                    CardTypeName = item.CardTypeName,

                    // Customer
                    CustomerId = item.CustomerId,
                    CustomerName = item.CustomerName,
                    CustomerBirthDate = item.CustomerBirthDate,
                    CustomerEmail = item.CustomerEmail,

                    // Card
                    CardId = item.CardId,
                    CardCardNumber = item.CardCardNumber,
                    CardExpirationDate = item.CardExpirationDate,
                    CardHasPassword = item.CardHasPassword,
                    CardPassword = item.CardPassword,
                    CardLimit = item.CardLimit,
                    CardLimitAvailable = item.CardLimitAvailable,
                    CardAttempts = item.CardAttempts,
                    CardBlocked = item.CardBlocked

                });
            }

            return transactionList;
        }
    }
}
