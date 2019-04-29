using AmonRa.Core;
using AmonRa.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace AmonRa
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string requestUri = "https://localhost:44338/api/v1/";

        protected string CARTAO_TEM_SENHA = "false";
        protected string EscolheuCartao = string.Empty;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            TransactionServer transServer = new TransactionServer();

            bool todas_informacoes_ok = true;

            try
            {
                // cartao com senha
                if (Convert.ToBoolean(CARTAO_TEM_SENHA))
                {
                    // nao digitou senha
                    if (String.IsNullOrEmpty(INPUT_SENHA.Password.ToString()))
                        todas_informacoes_ok = false;
                }

                // cartao credito, necessario escolher quantidade de parcelas
                if (COMBO_TIPO_TRANSACAO.Text.ToUpper().Equals("CRÉDITO / DÉBITO"))
                    todas_informacoes_ok = false;

                if (COMBO_TIPO_TRANSACAO.Text.ToUpper().Equals("CRÉDITO"))
                {
                    // nao escolher quantidade de parcelas
                    if (COMBO_NUMERO_PARCELAS.Text.ToUpper().Equals("PARCELAS") 
                        || COMBO_NUMERO_PARCELAS.Text.Equals("0"))
                        todas_informacoes_ok = false;
                }

                // nao digitou valor da compra
                if (String.IsNullOrEmpty(INPUT_VALOR.Text))
                    todas_informacoes_ok = false;

                // nao escolheu cartao
                if (String.IsNullOrEmpty(EscolheuCartao))
                    todas_informacoes_ok = false;

                if (!todas_informacoes_ok)
                {
                    MessageBox.Show("Por favor, falta informação.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string parcelas = COMBO_NUMERO_PARCELAS.Text;

                    if (parcelas.ToUpper().Equals("PARCELAS"))
                        parcelas = "0";

                    var resultTransaction = await transServer.SendTransaction(
                          EscolheuCartao,
                          INPUT_SENHA.Password.ToString(),
                          INPUT_VALOR.Text,
                          COMBO_TIPO_TRANSACAO.Text,
                          parcelas,
                          CARTAO_TEM_SENHA
                      );
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Erro de comunicação com servidor - " + ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void GetTransactions()
        {
            #region Get Id Transaction Type

            var serviceTransactionType = new TransactionTypeService(_httpClient);
            var resultTransactionType = await serviceTransactionType.GetAllTransactionTypes(requestUri + "TransactionType-management/");

            #endregion

        }

        public async void MontaComboTransactionTypes()
        {
            var transType = await SendRequestToServer.GetTypeTransactions.InformationCombo();

            if(transType.Count == 0)
            {
                MessageBox.Show("Oh não. \n Parece que o servidor de comunicações Karnak \n precisa de ajuda!!!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

                System.Environment.Exit(1);
            }

            COMBO_TIPO_TRANSACAO.ItemsSource = transType;
        }

        public async void GetTransactionTypes()
        {
            GridTransactionTypes.ItemsSource = await SendRequestToServer.GetTypeTransactions.Information();
        }

        public async void GetCards()
        {
            GridCard.ItemsSource = await SendRequestToServer.GetCard.Information();
        }

        public async void GetCustomers()
        {
            GridCustomer.ItemsSource = await SendRequestToServer.GetCustomer.Information();
        }

        public async void GetCardBrands()
        {
            GridBandeiraCartao.ItemsSource = await SendRequestToServer.GetCardBrand.Information();
        }

        public async void GetCardTypes()
        {
            GridTipoCartao.ItemsSource = await SendRequestToServer.GetCardType.Information();
        }

        public async void GetStatusTransactions()
        {
            GridTransactionStatus.ItemsSource = await SendRequestToServer.GetStatusTransactions.Information();
        }

        public async void GetTransactionList()
        {
            GridListagemTransacoes.ItemsSource = await SendRequestToServer.TransactionList.Information();
        }

        public async void CadastrarCliente()
        {
            var cardType = await SendRequestToServer.GetCardType.InformationCombo();
            cadastro_cliente_tipo_cartao.ItemsSource = cardType;

            var cardBrand = await SendRequestToServer.GetCardBrand.InformationCombo();
            cadastro_cliente_bandeira_cartao.ItemsSource = cardBrand;
        }        

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as RadioButton;

            // ... Display button content as title.
            this.Title = button.Content.ToString();

            // ... Get button content as title and set value to variable.
            EscolheuCartao = this.Title = button.Content.ToString();

            // cartao sem senha
            if (
                EscolheuCartao.Equals("4024007196111367") 
                || EscolheuCartao.Equals("4024207196812834")
                || EscolheuCartao.Equals("4024207142113372")
                || EscolheuCartao.Equals("4024207142153481")
                || EscolheuCartao.Equals("4024207342152763")
                )
            {
                CARTAO_TEM_SENHA = "false";
                LABEL_SENHA.Visibility = Visibility.Hidden;
                INPUT_SENHA.Visibility = Visibility.Hidden;
                INPUT_SENHA.Clear();
            }
            else
            {
                CARTAO_TEM_SENHA = "true";
                LABEL_SENHA.Visibility = Visibility.Visible;
                INPUT_SENHA.Visibility = Visibility.Visible;
                INPUT_SENHA.Clear();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = tab_control.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Cartões":
                    GetCards();
                    break;
                case "Clientes":
                    GetCustomers();
                    break;
                case "Tipo Cartão":
                    GetCardTypes();
                    break;
                case "Bandeira Cartão":
                    GetCardBrands();
                    break;
                case "Status Transação":
                    GetStatusTransactions();
                    break;
                case "Tipo Transação":
                    GetTransactionTypes();
                    break;
                case "Transações":
                    MontaComboTransactionTypes();
                break;
                case "Listagem Transações":
                    GetTransactionList();
                break;
                case "Cadastro de Cliente":
                    CadastrarCliente();
                    break;
                default:
                    break;
            }            
        }

        private async void GridListagemTransacoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (GridListagemTransacoes.SelectedItem != null)
                {
                    if (GridListagemTransacoes.SelectedItem is Models.TransactionList)
                    {
                        var row = (Models.TransactionList)GridListagemTransacoes.SelectedItem;

                        if (row != null)
                        {
                            GridSondagemTransacao.ItemsSource = await SendRequestToServer.Sondagem.Information(row.CardCardNumber);

                            // o ID da tab começa em 0 - automaticamente selecionar a tab Sondagem
                            tab_control.SelectedIndex = 8;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void COMBO_TIPO_TRANSACAO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(COMBO_TIPO_TRANSACAO.Text))
            {
                string opcao = COMBO_TIPO_TRANSACAO.SelectedValue.ToString();

                if (!opcao.Equals("Crédito / Débito"))
                {

                }
                if (opcao.Equals("Crédito"))
                {
                    COMBO_NUMERO_PARCELAS.Visibility = Visibility.Visible;
                }
                if (opcao.Equals("Débito"))
                {
                    COMBO_NUMERO_PARCELAS.Text = "0";
                    COMBO_NUMERO_PARCELAS.Visibility = Visibility.Hidden;
                }
            }
        }

        private async void Cadastro_Cliente_Click(object sender, RoutedEventArgs e)
        {
            bool todas_informacoes_ok = true;

            try
            {
                if (string.IsNullOrEmpty(cadastro_cliente_nome.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (string.IsNullOrEmpty(cadastro_cliente_e_mail.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (string.IsNullOrEmpty(cadastro_cliente_aniversario.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (string.IsNullOrEmpty(cadastro_cliente_tipo_cartao.Text))
                {
                    todas_informacoes_ok = false;
                }
                else
                {
                    string opcaoTipoCartao = cadastro_cliente_tipo_cartao.SelectedValue.ToString();
                    if (opcaoTipoCartao.ToUpper().Equals("CHIP"))
                    {
                        if (string.IsNullOrEmpty(cadastro_cliente_senha.Text))
                        {
                            todas_informacoes_ok = false;
                        }
                    }
                }

                if (string.IsNullOrEmpty(cadastro_cliente_bandeira_cartao.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (string.IsNullOrEmpty(cadastro_cliente_numero_cartao.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (string.IsNullOrEmpty(cadastro_cliente_data_expiracao.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (string.IsNullOrEmpty(cadastro_cliente_limite_cartao.Text))
                {
                    todas_informacoes_ok = false;
                }

                if (!todas_informacoes_ok)
                {
                    MessageBox.Show("Por favor, falta informação.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // precisa pegar os ids...
                    var cardCustomerReturn = await SendRequestToServer.PostCustomer.SendToServer(
                        cadastro_cliente_nome.Text,
                        cadastro_cliente_e_mail.Text,
                        cadastro_cliente_aniversario.Text,
                        cadastro_cliente_tipo_cartao.Text,
                        cadastro_cliente_senha.Text,
                        cadastro_cliente_bandeira_cartao.Text,
                        cadastro_cliente_numero_cartao.Text,
                        cadastro_cliente_data_expiracao.Text,
                        cadastro_cliente_limite_cartao.Text
                    );

                    string erros = string.Empty;

                    // Erro(s) para cadastrar customer
                    if (cardCustomerReturn.StatusCustomer == HttpStatusCode.BadRequest)
                        foreach (var erro in cardCustomerReturn.CustomerData)
                            erros = erros + "\n" + erro;

                    // Erro(s) para cadastrar card
                    if (cardCustomerReturn.StatusCard == HttpStatusCode.BadRequest)
                        foreach (var erro in cardCustomerReturn.CardData)
                            erros = erros + "\n" + erro;

                    if(!string.IsNullOrEmpty(erros))
                    {
                        MessageBox.Show(erros, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                         MessageBox.Show("Cadastro de cliente e cartão \n realizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de comunicação com servidor - " + ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cadastro_cliente_tipo_cartao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string opcao = cadastro_cliente_tipo_cartao.SelectedValue.ToString();

            if (opcao.ToUpper().Equals("CHIP"))
            {
                cadastro_cliente_label_senha.Visibility = Visibility.Visible;
                cadastro_cliente_senha.Visibility = Visibility.Visible;
            }
            else
            {
                cadastro_cliente_label_senha.Visibility = Visibility.Hidden;
                cadastro_cliente_senha.Visibility = Visibility.Hidden;
                cadastro_cliente_senha.Text = "";
            }
        }
    }
}
