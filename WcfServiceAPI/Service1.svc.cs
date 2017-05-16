using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EmployeeBO;

namespace WcfServiceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.




    public class Service1 : IService1
    {
        public List<EmpBo1> DataCont()
        {
            var list = EmployeeRepo.EmpRepo.getEmployeeList();
            List<EmpBo1> list1 = (from l in list
                         select new EmpBo1
                         {
                             Id = l.Id,
                             Name = l.Name,
                             Age = l.Age,
                             Salary = l.Salary,
                             JoiningDate = l.JoiningDate,
                         }).ToList<EmpBo1>();

            return list1;
        }

        public string DataCont1()
        {
            return "Hi";
        }

        public Object FaultCont()
        {
            try
            {
                int y = 0;
                int res = 2 / y;
                return res;
            }
            catch(Exception ex)
            {
                ExecClass ex1 = new ExecClass();
                ex1.ErrorCode = 123;
                ex1.ErrorMessage = "Count out of range";
                throw new FaultException<ExecClass>(ex1, ex.ToString());
            }
        }

        public List<EmpBO> GetData()
        {
            List<EmpBO> list = EmployeeRepo.EmpRepo.getEmployeeList();
            return list;
        }

        public List<EmpBO> GetDataJson()
        {
            List<EmpBO> list = EmployeeRepo.EmpRepo.getEmployeeList();
            return list;
        }

        public int OverloadindMethod(int param1, int param2)
        {
            int res = param1 + param2;
            return res;
        }

        public int OverloadindMethod(int param1, int param2, int param3)
        {
            int res = param1 + param2 + param3;
            return res;
        }
    }
}
