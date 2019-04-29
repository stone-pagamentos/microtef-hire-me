using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class TransactionList
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/Transaction-management/transactionList";

        public static async Task<dynamic> Information()
        {
            var serviceTransactionList = new Services.TransactionList(_httpClient);
            var resultTransactionList = await serviceTransactionList.GetAllTransactionList(requestUri);

            List<Models.TransactionList> transactionList = new List<Models.TransactionList>();

            foreach (var item in resultTransactionList.Data)
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
