using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessObjects.Interfaces
{
    public interface ICustomerDAO
    {
        public Customer IsValidCustomerAccount(string email, string password);
        List<Customer> GetAllCustomer();

        Customer GetCustomerByEmail(string email);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);

        Customer GetCustomerById(int id);

    }
}
