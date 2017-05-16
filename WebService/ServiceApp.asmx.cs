using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EmployeeBO;
using System.Web.Services.Protocols;

namespace WebService
{
    /// <summary>
    /// Summary description for ServiceApp
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceApp : System.Web.Services.WebService
    {

        public class AuthHeader : SoapHeader
        {
            public string Username;
            public string Password;
        }

        public AuthHeader Authentication;

        [SoapHeader("Authentication", Required = true)]
        [WebMethod]
        public Object GetMonth()
        {
            if(Authentication.Username == "Test" && Authentication.Password == "Test") {
                List<Months> Months = new List<Months>() {
                    new Months { Id = 1, Name = "January" },
                    new Months { Id = 2, Name = "Febuary" },
                    new Months { Id = 3, Name = "March" },
                    new Months { Id = 4, Name = "April" },
                    new Months { Id = 5, Name = "May" },
                    new Months { Id = 6, Name = "June" },
                    new Months { Id = 7, Name = "July" },
                    new Months { Id = 8, Name = "August" },
                    new Months { Id = 9, Name = "September" },
                    new Months { Id = 10, Name = "October" },
                    new Months { Id = 11, Name = "November" },
                    new Months { Id = 12, Name = "December" }
                };
                return Months;
            }
            else
            {
                return "Not Authorized";
            }

        }

        [WebMethod]
        public List<Months> GetMonthNoAuthentication()
        {
                List<Months> Months = new List<Months>() {
                    new Months { Id = 1, Name = "January" },
                    new Months { Id = 2, Name = "Febuary" },
                    new Months { Id = 3, Name = "March" },
                    new Months { Id = 4, Name = "April" },
                    new Months { Id = 5, Name = "May" },
                    new Months { Id = 6, Name = "June" },
                    new Months { Id = 7, Name = "July" },
                    new Months { Id = 8, Name = "August" },
                    new Months { Id = 9, Name = "September" },
                    new Months { Id = 10, Name = "October" },
                    new Months { Id = 11, Name = "November" },
                    new Months { Id = 12, Name = "December" }
                };
            return Months;
        }

        [WebMethod]
        public List<EmpBO> GetEmployees()
        {
            List<EmpBO> list = EmployeeRepo.EmpRepo.getEmployeeList();
            return list;
        }

        [WebMethod]
        public List<CountryList> GetCountries()
        {
            List<CountryList> list = new List<CountryList>
            {
                new CountryList {Id = 1, Country= "India" },
                new CountryList {Id = 2, Country= "USA" }
            };

            return list;
        }

        [WebMethod]
        public Decimal Calculte(Calc cal)
        {
            int result = 0;
            switch (cal.Operation)
            {
                case "sum":
                    result = cal.Num1 + cal.Num2;
                    break;
                case "sub":
                    result = cal.Num1 - cal.Num2;
                    break;
                case "sos":
                    result = cal.Num1 * cal.Num1 + cal.Num2* cal.Num2;
                    break;
                case "soc":
                    result = cal.Num1 * cal.Num1 * cal.Num1 + cal.Num2 * cal.Num2 * cal.Num2;
                    break;
                case "sos1":
                    result = cal.Num1 * cal.Num1 - cal.Num2 * cal.Num2;
                    break;
                case "soc1":
                    result = cal.Num1 * cal.Num1 * cal.Num1 - cal.Num2 * cal.Num2 * cal.Num2;
                    break;
                case "default":
                    result = 0;
                    break;
            }

            return result;
        }
    }
}
