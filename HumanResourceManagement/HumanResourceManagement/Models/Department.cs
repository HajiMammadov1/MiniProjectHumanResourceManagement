using HumanResourceManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResourceManagement.Models
{
    class Department
    {
        public double CalcSalaryAverage()
        {
            int sum = 0;
            foreach(var item in employees)
            {
                sum += item.Salary;
            }
            double calcavarage = sum / employees.Length;
            return calcavarage;
        }
        public double CalcSalarySum ()
        {
            int sum = 0;
            foreach (var item in employees)
            {
                sum += item.Salary;
            }
         
            return sum;
        }

        public Department(string name,int workerLimit,int salaryLimit)
        {
            
            Name = name;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
           
            employees = new Employee[0];
        }
        

        public string Name;
        public int WorkerLimit;
        public int SalaryLimit;
        public Employee[] employees;

        public void AddEmployee (Employee employee)
        {
            Array.Resize(ref employees, employees.Length + 1);
            employees[employees.Length - 1] = employee;
        }

    }
}
