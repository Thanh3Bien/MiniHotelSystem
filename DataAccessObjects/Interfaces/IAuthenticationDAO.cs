using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Interfaces
{
    public interface IAuthenticationDAO
    {
        bool IsAdminAccount(string email, string password);
        bool IsCustomerAccount(string email, string password);
    }
}
