using AmonRa.Models;
using AmonRa.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class GetStatusTransactions
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/TransactionStatus-management/";

        public static async Task<dynamic> Information()
        {
            var serviceTransactionStatus = new TransactionStatusService(_httpClient);
            var resultTransactionStatus = await serviceTransactionStatus.GetAllTransactionStatus(requestUri);

            List<TransactionStatus> statusTrans = new List<TransactionStatus>();

            foreach (var item in resultTransactionStatus.Data)
            {
                statusTrans.Add(new TransactionStatus { Id = item.Id, Name = item.Name });
            }

            return statusTrans;
        }
    }
}
