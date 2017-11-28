using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EmployeeBO
{
    //test
    public class EmpBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Salary { get; set; }
    }

    public class EmpDetailsBO
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int Phone { get; set; }
        public  string Email { get; set; }
        public int Skills { get; set; }
    }

    public class Months
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountryList
    {
        public int Id { get; set; }
        public string Country { get; set; }
    }

    public class Calc
    {
        public int Id { get; set; }
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public string Operation { get; set; }
    }

}
