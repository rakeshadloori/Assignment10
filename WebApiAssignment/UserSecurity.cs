using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAssignment
{
    public class UserSecurity
    {
        public static bool Login(string username, string password)
        {
            if(username == "Admin" && password == "Pass")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}