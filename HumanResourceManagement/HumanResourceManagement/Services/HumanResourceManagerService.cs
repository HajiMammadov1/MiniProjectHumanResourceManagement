using HumanResourceManagement.Enums;
using HumanResourceManagement.Interfaces;
using HumanResourceManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HumanResourceManagement.Services
{
    class HumanResourceManagerService : IHumanResourceManagerService
    {
        private Department[] _departments;
        public Department[] Departments => _departments;


        public HumanResourceManagerService()
        {
            _departments = new Department[0];
        }

        
        //This method checks whether entered department already exists or not.If department already exists displays message.
        //If department does not exists,then adds(creates) new departments with using method of Array.Resize()
        public void AddDepartment(string name, int workerLimit, int salaryLimit)
        {
            foreach (Department department in _departments)
            {
                if (department.Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine("This Department already exists.");
                    return;
                }
            }
            Department departaments = new Department(name, workerLimit, salaryLimit);


            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = departaments;

        }
        public Department[] GetDepartments()
        {
            return _departments;
        }

        //This method edits current department
        public void EditDepartment(string oldDepName, string newDepName)
        {
            foreach (Department department in _departments)
            {
                if (oldDepName == department.Name)
                {
                    department.Name = newDepName;
                }
            }

        }

        //This method checks worker limit and salary limit
        public void AddEmployee(string fullname, PositionType position, int salary, Department department)
        {
            if (department.WorkerLimit > department.employees.Length)
            {
                if (department.SalaryLimit > department.CalcSalarySum() + salary)
                {
                    Employee employee = new Employee(fullname, position, salary, department);
                    department.AddEmployee(employee);
                }
                else
                {
                    Console.WriteLine("Salary limit is reached");
                }
            }
            else
            {
                Console.WriteLine("Worker limit is reached");
            }
        }

        //This method removes employee from employees array,with using of Array.Clear() method.
        public void RemoveEmployee(string departmentName, string fullname)
        {
            Department department = null;
            try
            {
                foreach (Department item in _departments)
                {
                    if (item.Name.ToLower() == departmentName.ToLower())
                    {
                        department = item;
                        break;
                    }
                }

                Employee employee = null;

                if (department != null)
                {
                    foreach (var item in department.employees)
                    {
                        if (item.FullName.ToLower() == fullname.ToLower())
                        {
                            employee = item;
                            break;
                        }
                    }
                }
                if (employee != null)
                {
                    int index = Array.IndexOf(department.employees, employee);

                    Array.Clear(department.employees, index, 1);
                }

            }
            catch
            {
                Console.WriteLine("Please,check the opeartion again");
            }
            
            

        }

        //This method checks employee number and assign new salary and position.
        public void EditEmployee(string no, int newSalary, PositionType positiontype)
        {
            foreach (Department department in _departments)
            {
                foreach (var employee in department.employees)
                {
                    if (employee.No==no)
                    {
                        employee.Salary = newSalary;
                        employee.Position = positiontype;
                        Console.WriteLine("Edit were performed");
                        return;
                    }
                }
            }
            Console.WriteLine($" New position: {positiontype} New salary: { newSalary}");
        }
    }
}
