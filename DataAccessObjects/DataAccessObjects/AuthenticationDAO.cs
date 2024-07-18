using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObjects.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects.DataAccessObjects
{
    public class AuthenticationDAO : IAuthenticationDAO
    {
        private readonly IConfiguration _configuration;
        private readonly CustomerDAO _customerDAO;
        private static AuthenticationDAO instance = null;
        private static readonly object Lock = new object();

        
        public AuthenticationDAO()
        {
            var solutionDir = @"C:\Users\ADMIN\Documents\Summer2024\PRN212\PRN212\2_PRN212\Assignment2";
            var configFilePath = Path.Combine(solutionDir, "FUMiniHotelSystem", "appsettings.json");
            _configuration = new ConfigurationBuilder()
                                .SetBasePath(solutionDir)
                                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                                .Build();
        }

        
        public static AuthenticationDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new AuthenticationDAO();
                    }
                    return instance;
                }
            }
        }

        public bool IsAdminAccount(string email, string password)
        {
            if (_configuration != null)
            {
                var adminSection = _configuration.GetSection("AdminAccount");
                var adminEmail = adminSection["Email"];
                var adminPassword = adminSection["Password"];
                return email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && password.Equals(adminPassword);
            }
            return false;
        }
    
        public bool IsCustomerAccount(string email, string password)
        {
            if(_customerDAO.IsValidCustomerAccount(email, password) != null)
            {
                return true;
            }
            return false;
        }

       
    }
}
