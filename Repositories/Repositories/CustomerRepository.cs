using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.DataAccessObjects;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        

        public List<Customer> GetAllCustomer() => CustomerDAO.Instance.GetAllCustomer();

        public Customer GetCustomerByEmail(string email) => CustomerDAO.Instance.GetCustomerByEmail(email);
        public void InsertCustomer(Customer customer)
        {
            CustomerDAO.Instance.InsertCustomer(customer);
        }


        public Customer IsValidCustomerAccount(string email, string password)
        {
            return CustomerDAO.Instance.IsValidCustomerAccount(email, password);
        }

        public void UpdateCustomer(Customer customer)
        {
            CustomerDAO.Instance.UpdateCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            CustomerDAO.Instance.DeleteCustomer(customer);
        }

        public Customer GetCustomerById(int id) => CustomerDAO.Instance.GetCustomerById(id);
    }
}
