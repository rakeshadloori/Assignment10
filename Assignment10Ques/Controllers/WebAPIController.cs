using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Assignment10Ques.Controllers
{
    public class WebAPIController : Controller
    {
        // GET: WebAPI
        public ActionResult GetJsonData()
        {
            return View();
        }

        public ActionResult GetJsonController()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56660");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/Emp").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseString = response.Content.ReadAsAsync<List<EmployeeBO.EmpBO>>().Result;
                    return View(responseString);
                }else
                {
                    return View();
                }
            }
        }

        public ActionResult SendObject()
        {
            return View();
        }

        public ActionResult InserObj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InserObj(EmployeeBO.EmpBO emp)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56660");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var res = client.PostAsJsonAsync<EmployeeBO.EmpBO>("api/InsertEmp", emp).Result;


                return View();
            }
        }


        public ActionResult UpdateObj(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56660");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var res = client.GetAsync("api/GetEmpById/"+id).Result;
                if (res.IsSuccessStatusCode)
                {
                    var responseString = res.Content.ReadAsAsync<EmployeeBO.EmpBO>().Result;
                    return View(responseString);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult UpdateObj(EmployeeBO.EmpBO emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56660");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var res = client.PostAsJsonAsync<EmployeeBO.EmpBO>("api/Update", emp).Result;

                return RedirectToAction("GetJsonController");
            }
        }

        public ActionResult DeleteObj(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56660");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var res = client.GetAsync("api/Delete/"+id).Result;

                return RedirectToAction("GetJsonController");
            }
        }
    }
}