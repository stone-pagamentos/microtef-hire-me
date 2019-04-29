using AmonRa.Models;
using AmonRa.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class GetTypeTransactions
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = "https://localhost:44338/api/v1/TransactionType-management/";

        public static async Task<dynamic> Information()
        {
            var serviceTransactionType = new TransactionTypeService(_httpClient);
            var resultTransactionType = await serviceTransactionType.GetAllTransactionTypes(requestUri);

            List<TransactionType> statusTrans = new List<TransactionType>();

            foreach (var item in resultTransactionType.Data)
            {
                statusTrans.Add(new TransactionType { Id = item.Id, Name = item.Name });
            }

            return statusTrans;
        }

        public static async Task<List<string>> InformationCombo()
        {
            List<string> typesTrans = new List<string>();

            try
            {
                var serviceTransactionType = new TransactionTypeService(_httpClient);
                var resultTransactionType = await serviceTransactionType.GetAllTransactionTypes(requestUri);

                typesTrans.Add("CRÉDITO / DÉBITO");

                foreach (var item in resultTransactionType.Data)
                {
                    typesTrans.Add(item.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro servidor - " + ex.Message);
            }

            return typesTrans;
        }
    }
}
