
using Assignment10Ques.ServiceReference1;
using EmployeeBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment10Ques.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            ServiceReference1.ServiceAppSoapClient sp = new ServiceReference1.ServiceAppSoapClient();

            ServiceReference1.AuthHeader user = new AuthHeader
            {
                Username = "Teasds123t",
                Password = "Test"
            };

            //List<ServiceReference1.Months> res = (List<ServiceReference1.Months>) sp.Calander(user).ToList();

            var res = sp.GetMonth(user);
            if(res is string)
            {
                List<ServiceReference1.Months> months = new List<ServiceReference1.Months>();
                ViewBag.Response = res;
                return View(months);
                
            }
            else
            {
                return View(res);
            }
           
            
        }

        public ActionResult Index2()
        {
            ServiceReference1.ServiceAppSoapClient sp = new ServiceReference1.ServiceAppSoapClient();

            var res1 = sp.GetMonthNoAuthentication();

            List<ServiceReference1.Months> res = (List<ServiceReference1.Months>)sp.GetMonthNoAuthentication().ToList();

            return View(res);

        }

        public ActionResult Index3()
        {
            ServiceReference1.ServiceAppSoapClient sp = new ServiceReference1.ServiceAppSoapClient();
            List<ServiceReference1.EmpBO> list =  sp.GetEmployees().ToList();
            return View(list);
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult Index5()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index5(ServiceReference1.Calc cal)
        {
            
            ServiceReference1.ServiceAppSoapClient sp = new ServiceReference1.ServiceAppSoapClient();
            Decimal list = sp.Calculte(cal);
            ViewBag.Res = list;
            return View(cal);
        }
    }
}