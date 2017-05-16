using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeBO;
using WebApiAssignment.Models;
using System.Web.Http.Cors;

namespace WebApiAssignment.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmpController : ApiController
    {

        // GET: api/Emp
        //[BasicAuthentication]
        public List<EmpBO> GetEmpBOes()
        {
            var list =  EmployeeRepo.EmpRepo.getEmployeeList();
            return list;
        }

        [Route("api/GetEmpById/{id}")]
        [HttpGet]
        public EmpBO EmpDetails(int id)
        {
            EmpBO emp = EmployeeRepo.EmpRepo.GetUserInfoById(id);
            return emp;
        }

        [Route("api/InsertEmp")]
        public string InsertEmp(EmpBO emp)
        {
            EmployeeRepo.EmpRepo.InsertEmp(emp);
            return "Inserted";
        }

        [Route("api/Update")]
        public string UpdateEmp(EmpBO emp)
        {
            EmployeeRepo.EmpRepo.UpdateEmp(emp);
            return "asd";
        }

        [HttpGet]
        [Route("api/Delete/{id}")]
        public string RemEmp(int id)
        {
            EmployeeRepo.EmpRepo.DeleteEmpById(id);
            return "Deleted";
        }

    }
}