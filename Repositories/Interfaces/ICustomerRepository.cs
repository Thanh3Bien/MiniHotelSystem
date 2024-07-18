using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer IsValidCustomerAccount(string email, string password);
        List<Customer> GetAllCustomer();

        void InsertCustomer(Customer customer); 
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);

        Customer GetCustomerByEmail(string email);

        Customer GetCustomerById(int id);

    }
}
