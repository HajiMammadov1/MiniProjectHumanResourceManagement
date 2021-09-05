using HumanResourceManagement.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResourceManagement.Models
{
    class Employee
    {
        public Employee(string fullname,PositionType position,int salary,Department department)
        {
            _no++;
            FullName = fullname;
            Position = position;
            Salary = salary;
            No = department.Name.Substring(0, 2).ToUpper()+_no;
            
        }

        private static int _no = 1000;
        public string No;
        public string FullName;
        public int Salary;
        public string DepartmentName;
        public PositionType Position;

        public override string ToString()
        {
            return $"{No} {FullName} {Position} {Salary}";
        }
    }
}
