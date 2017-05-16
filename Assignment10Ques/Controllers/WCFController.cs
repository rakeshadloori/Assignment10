using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel;
using Assignment10Ques.ServiceReference2;

namespace Assignment10Ques.Controllers
{
    public class WCFController : Controller
    {
        // GET: WCF
        public ActionResult Index()
        {
            return View();
        }

        public string FaultCont()
        {
            ServiceReference2.Service1Client sc = new ServiceReference2.Service1Client();
            try
            {
                var res = sc.FaultCont();
                return res.ToString();
            }
            catch(FaultException<ExecClass> ex)
            {
                return ex.Detail.ErrorMessage+" "+ex.Detail.ErrorCode;
            }
        }

        public int MethodOverLoading()
        {
            ServiceReference2.Service1Client sc = new ServiceReference2.Service1Client();
            int res = sc.Method1(1, 2);
            int res1 = sc.Method2(1, 2, 3);
            return res + res1;
        }
    }
}