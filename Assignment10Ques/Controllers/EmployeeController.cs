using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeRepo;
using EmployeeBO;
using System.Data;
using System.Web.Script.Serialization;

namespace Assignment10Ques.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee Multiple Tables
        public ActionResult Index()
        {
            EmpViewModel emp = EmpRepo.getEmployees();
            return View(emp);
        }

        // GET: Employee Table
        public ActionResult Index1()
        {
            DataTable dt = EmpRepo.getEmployee();
            return View(dt);
        }

        // GET: DataReader
        public ActionResult Index3()
        {
            List<EmpBO> emp = EmpRepo.getEmployeeList();
            return View(emp);
        }

        // GET: DataReader Multiple Tables
        public ActionResult Index4()
        {
            EmpViewModel emp = EmpRepo.getEmployeesList();
            return View(emp);
        }

        // Scalara Value
        public ActionResult Index5()
        {
            return View();
        }

        // Get Scalar value (Primary Key) after inserting record
        [HttpPost]
        public ActionResult Index6(EmpBO emp)
        {
            int id = EmpRepo.getScalarValue(emp);
            ViewBag.id = id;
            return View(emp);
        }

        // Insert data get PK and use pk to update FK table
        public ActionResult Index8()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index8(EmpViewModel1 emp)
        {
            int pk = EmpRepo.insertIntoTwoTables(emp);
            return RedirectToAction("Index");
        }

        // Bool: Find Person
        public ActionResult Index9()
        {
            ViewBag.emp = TempData["emp"];
            ViewBag.bool1 = TempData["val"];
            return View();
        }
        [HttpPost]
        public ActionResult Index9(string emp)
        {
            string id = EmpRepo.findEmployee(emp);
            TempData["emp"] = emp;
            TempData["val"] = id;
            return RedirectToAction("Index9");
        }

        //Multiple insert
        public ActionResult Index10()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index10(List<EmpBO> emp)
        {
            EmpRepo.MultipleInsert(emp);
            return RedirectToAction("Index1");
        }

        //Delete user if conditions
        public ActionResult Index11()
        {
            ViewBag.res = TempData["user"];
            ViewBag.id = TempData["id"];
            return View();
        }

        [HttpPost]
        public ActionResult Index11(int id)
        {
            string res = EmpRepo.checkUser(id);
            TempData["id"] = id;
            TempData["user"] = res;
            return RedirectToAction("Index11");
            
        }

        //Update Salary
        public ActionResult Index12()
        {
            ViewBag.res = TempData["res"];
            return View();
        }

        [HttpPost]
        public ActionResult Index13()
        {
            string res = EmpRepo.updateSalary();
            TempData["res"] = res;
            return RedirectToAction("Index12");
        }

        // Get User Info if exists
        public ActionResult Index14()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index14(string Name)
        {

            Object ob = EmpRepo.UserInfo(Name);
            ViewBag.Name = Name;
            if(ob is EmpBO)
            {
                return View(ob);
            }
            else
            {
                //EmpBO ob1 = new EmpBO();
                ViewBag.Msg = ob;
                return View();
            }
            
        }

        public ActionResult Index15()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index15(EmpBO emp)
        {
            string res = EmpRepo.UDT(emp);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public string Index16(EmpBO emp)
        {

            EmpRepo.getScalarValue(emp);
            string res = "Created a New Record !";
            return res;
        }

        public JsonResult Index17()
        {
            List<EmpBO> emp = EmpRepo.getEmployeeList();
            
            return Json(emp, JsonRequestBehavior.AllowGet);
        }


        public ActionResult test()
        {
            return View();
        }
    }
}