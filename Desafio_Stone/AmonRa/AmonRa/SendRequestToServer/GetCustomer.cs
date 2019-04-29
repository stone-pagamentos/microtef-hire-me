using AmonRa.Models;
using AmonRa.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmonRa.SendRequestToServer
{
    public class GetCustomer
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static string requestUri = ConfigurationManager.AppSettings["url_servidor_comunicacoes"] + "/customer-management/";

        public static async Task<dynamic> Information()
        {
            var serviceCliente = new CustomerService(_httpClient);
            var resultCliente = await serviceCliente.GetAllCustomers(requestUri);

            List<Customer> customer = new List<Customer>();

            foreach (var item in resultCliente.Data)
            {
                customer.Add(new Customer { Id = item.Id, Name = item.Name, BirthDate = item.BirthDate, Email = item.Email });
            }

            return customer;
        }

        public static async Task<dynamic> InformationCombo()
        {
            var serviceCliente = new CustomerService(_httpClient);
            var resultCliente = await serviceCliente.GetAllCustomers(requestUri);

            List<string> customer = new List<string>();

            foreach (var item in resultCliente.Data)
            {
                customer.Add(item.Name);
            }

            return customer;
        }

        public static async Task<dynamic> GetCustomerByName(string name)
        {
            var serviceCustomer = new CustomerService(_httpClient);
            var resultCustomer = await serviceCustomer.GetByNameCustomer(requestUri, name);

            return resultCustomer;
        }

        public static async Task<dynamic> PostCustomer(Customer customer)
        {
            var serviceCustomer = new CustomerService(_httpClient);
            var resultCustomer = await serviceCustomer.PostCustomer(requestUri, customer);

            return resultCustomer;
        }

        public static async Task<dynamic> DeleteCustomer(Guid id)
        {
            var serviceCustomer = new CustomerService(_httpClient);
            var resultCustomer = await serviceCustomer.DeleteByIdCustomer(requestUri, id);

            return resultCustomer;
        }
    }
}
