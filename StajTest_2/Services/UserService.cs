using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajTest_2.Services
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("123") && password.Equals("123");// Pa$$WoRd
        }
    }
}
