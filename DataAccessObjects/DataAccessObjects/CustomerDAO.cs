using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using DataAccessObjects.Interfaces;

namespace DataAccessObjects.DataAccessObjects
{
    public class CustomerDAO : ICustomerDAO
    {
        public static CustomerDAO instance { get; private set; }
        private static object lockObject = new object();
        public CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }
        public Customer IsValidCustomerAccount(string email, string password)
        {
            using FuminiHotelManagementContext db = new FuminiHotelManagementContext();
            return db.Customers.SingleOrDefault(c => c.EmailAddress.Equals(email) && c.Password.Equals(password));
        }

        public List<Customer> GetAllCustomer()
        {
            using (var db = new FuminiHotelManagementContext())
            {
                return db.Customers.ToList();
            }
        }

        public void InsertCustomer(Customer customer)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var existingCustomer = db.Customers.Find(customer.CustomerId);

                    if (existingCustomer != null)
                    {
                        existingCustomer.CustomerId = customer.CustomerId;
                        existingCustomer.CustomerFullName = customer.CustomerFullName;
                        existingCustomer.EmailAddress = customer.EmailAddress;
                        existingCustomer.Telephone = customer.Telephone;
                        existingCustomer.Password = customer.Password;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Customer with ID {customer.CustomerId} not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
            
        }

        public void DeleteCustomer(Customer customer)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var existingCustomer = db.Customers.Find(customer.CustomerId);
                    if (existingCustomer != null)
                    {
                        db.Remove(existingCustomer);
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Customer GetCustomerByEmail(string email)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var existingCustomer = db.Customers.FirstOrDefault(c => c.EmailAddress.Equals(email));
                    if (existingCustomer != null)
                    {
                        return existingCustomer;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                using (var db = new FuminiHotelManagementContext())
                {
                    var existingCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
                    if (existingCustomer != null)
                    {
                        return existingCustomer;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
