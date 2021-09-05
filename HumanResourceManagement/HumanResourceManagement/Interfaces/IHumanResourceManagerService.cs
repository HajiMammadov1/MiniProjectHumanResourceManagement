using HumanResourceManagement.Enums;
using HumanResourceManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanResourceManagement.Interfaces
{
    interface IHumanResourceManagerService
    {
         Department[] Departments { get; }

        
        void AddDepartment(string name,int workerLimit,int salaryLimit);
        Department[] GetDepartments();
        void EditDepartment(string oldDepName,string newDepName);
        void AddEmployee(string fullname,PositionType position,int salary,Department department);
        void RemoveEmployee(string departmentName,string no);
        void EditEmployee(string no,int salary,PositionType position);

    }
}
