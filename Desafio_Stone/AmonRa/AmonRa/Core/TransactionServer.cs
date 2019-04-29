using AmonRa.Models;
using AmonRa.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace AmonRa.Core
{
    public class TransactionServer
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string requestUri = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/";


        public async Task<dynamic> SendTransaction(string EscolheuCartao, string INPUT_SENHA, string INPUT_VALOR, string COMBO_TIPO_TRANSACAO, string COMBO_NUMERO_PARCELAS, string HAS_PASSWORD)
        {
            Transaction transaction = null;
            dynamic retornoTransacao = null;

            #region pegar informacoes do cartao

            string info = EscolheuCartao;

            var service = new CardService(_httpClient);
            var resultCard = await service.GetByCardNumber(requestUri + "Card-management/", info);

            #endregion

            if (resultCard == null)
                MessageBox.Show("Erro de comunicação com servidor - GetByCardNumber.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            if (resultCard.Status.Equals(HttpStatusCode.OK))
            {
                // tudo certo, enviar a transação

                #region Get Id Transaction Type

                string cardTypeName = COMBO_TIPO_TRANSACAO;

                var serviceTransactionType = new TransactionTypeService(_httpClient);
                var resultTransactionType = await serviceTransactionType.GetByNameTransactionType(requestUri + "TransactionType-management/", cardTypeName);

                #endregion

                if (resultTransactionType == null)
                    MessageBox.Show("Erro de comunicação com servidor - GetByNameTransactionType.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                if (resultTransactionType.Status.Equals(HttpStatusCode.OK))
                {
                    transaction = new Transaction
                    {
                        Id = Guid.NewGuid(),
                        Amount = Convert.ToDecimal(INPUT_VALOR),
                        IdTransactionType = resultTransactionType.Data.Id,
                        IdCard = resultCard.Data.Id,
                        IdTransactionStatus = Guid.Empty,
                        Number = Convert.ToInt32(COMBO_NUMERO_PARCELAS.Replace(" ", "").Replace("X", "")),
                        TransactionDate = DateTime.Now,
                        Password = Common.StringCipher.Encrypt(INPUT_SENHA.ToString(), "StefanSilva@#@Stone##2019"),
                        HasPassword = HAS_PASSWORD
                    };

                    var serviceTransaction = new TransactionService(_httpClient);
                    var resultTransaction = await serviceTransaction.PostTransaction(requestUri + "Transaction-management/", transaction);

                    if (resultTransaction.Status == HttpStatusCode.OK)
                    {
                        retornoTransacao = new
                        {
                            Status = resultTransaction.Status,
                            Data = resultTransaction.Data
                        };

                        MessageBoxResult result = MessageBox.Show("TRANSAÇÃO REALIZADA COM SUCESSO.",
                                              "Information",
                                              MessageBoxButton.OK);

                    }
                    else
                    {
                        string messageError = string.Empty;

                        List<string> listError = new List<string>();
                        foreach (string error in resultTransaction.Data)
                        {
                            listError.Add(error);
                            messageError = messageError + "\n" + error;
                        }

                        retornoTransacao = new
                        {
                            Status = resultTransaction.Status,
                            Data = listError
                        };

                        MessageBox.Show(messageError, "Error(s)", MessageBoxButton.OK, MessageBoxImage.Error);


                        //Assert.Collection(listError,
                        //    item => Assert.Equal("Cartão bloqueado", item),
                        //    item => Assert.Equal("Senha Incorreta", item),
                        //    item => Assert.Equal("Password error size", item),
                        //    item => Assert.Equal("Saldo insuficiente", item),
                        //    item => Assert.Equal("Transação aprovada", item),
                        //    item => Assert.Equal("Password between 4 and 6 digits", item),
                        //    item => Assert.Equal("The amount must have greater than 10 cents", item),
                        //    item => Assert.Equal("The transacion guid is empty", item),
                        //    item => Assert.Equal("The transaction guid is invalid", item),
                        //    item => Assert.Equal("The transaction guid is invalid and contains 00000000", item),
                        //    item => Assert.Equal("The transacion type guid is empty", item),
                        //    item => Assert.Equal("The transaction type guid is invalid", item),
                        //    item => Assert.Equal("The transaction type guid is invalid and contains 00000000", item),
                        //    item => Assert.Equal("The transacion card guid is empty", item),
                        //    item => Assert.Equal("The transaction card guid is invalid", item),
                        //    item => Assert.Equal("The transaction card guid is invalid and contains 00000000", item)
                        //);
                    }
                }
            }


            // This method runs asynchronously.
            return await Task.Run(() => new {
                Information = retornoTransacao
            });

        }
    }
}
