using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EmployeeBO
{
    //public class EmpViewModel
    //{
    //    public EmpBO EmpBO { get; set; }
    //    public EmpDetailsBO EmpDetailsBO { get; set; }
    //}

    public class EmpViewModel
    {
        public List<EmpBO> EmpBO { get; set; }
        public List<EmpDetailsBO> EmpDetailsBO { get; set; }
    }

    public class EmpViewModel1
    {
        public EmpBO EmpBO { get; set; }
        public EmpDetailsBO EmpDetailsBO { get; set; }
    }
}
