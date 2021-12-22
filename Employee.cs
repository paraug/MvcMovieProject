using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmpName { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}