using HumanResourceManagement.Enums;
using HumanResourceManagement.Services;
using System;

namespace HumanResourceManagement
{
    class Program
    {

        static void Main(string[] args)
        {
            HumanResourceManagerService humanService = new HumanResourceManagerService();

            do
            {
                Console.WriteLine("1-Show the list of the departments ");
                Console.WriteLine("2-Create new department ");
                Console.WriteLine("3-Edit department ");
                Console.WriteLine("4-Show the list of the employees ");
                Console.WriteLine("5-Show the list of the employees in the department ");
                Console.WriteLine("6-Add new employee ");
                Console.WriteLine("7-Edit employee ");
                Console.WriteLine("8-Remove employee from department ");
                Console.WriteLine("9-Quit");

                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowDepartments(ref humanService);
                        break;
                    case "2":
                        AddDepartment(ref humanService);
                        break;
                    case "3":
                        EditDepartment(ref humanService);
                        break;
                    case "4":
                        ShowEmployees(ref humanService);
                        break;
                    case "5":
                        ShowEmployeesOfDepartment(ref humanService);
                        break;
                    case "6":
                        AddEmployee(ref humanService);
                        break;
                    case "7":
                        EditEmployee(ref humanService);
                        break;
                    case "8":
                        RemoveEmmployeeFromDepartment(ref humanService);
                        break;
                    case "9":
                        return;
                    default:
                        Console.WriteLine("There is no such an operation,please try again");
                        break;
                }

            } while (true);

            
        }
        //In this method we check whether entered department name matches requirement of task(department name must be at least 2 characters) or not.
        //And we check limit of worker,which must be at least 1.
        //Then we check salary limit,which must be at least 250.
        static void AddDepartment(ref HumanResourceManagerService humanService)
        {
            Console.WriteLine("Enter department name");
            DepartamentNameStart:
            string departmentName = Console.ReadLine();
            try
            {
                if (departmentName.Length < 2)
                {
                    Console.WriteLine("Department name can't be less than 2 characters");
                    goto DepartamentNameStart;
                }
                Console.WriteLine("Assign the worker limit");
            WorkerLImitStart:
                int worker = int.Parse(Console.ReadLine());

                if (worker < 1)
                {
                    Console.WriteLine("Worker limit must be at least 1");
                    goto WorkerLImitStart;
                }
                Console.WriteLine("Enter salary limiti");
            SalaryLimitStart:
                int limit = int.Parse(Console.ReadLine());
                if (limit < 250)
                {
                    Console.WriteLine("Salary can't be less than 250");
                    Console.WriteLine("The employee can not be added to the department.");
                    goto SalaryLimitStart;
                }

                Console.WriteLine("Operations were performed");

                humanService.AddDepartment(departmentName, worker, limit);
            }
            catch
            {
                Console.WriteLine("Please,check the operation");
            }
        }

        //This method requires department name and employee name
        private static void RemoveEmmployeeFromDepartment(ref HumanResourceManagerService humanService)
        {
            Console.WriteLine("Enter department");
            string departmentName = Console.ReadLine();

            Console.WriteLine("Enter the name of the employee");
            string employeeName = Console.ReadLine();

           humanService.RemoveEmployee(departmentName, employeeName);
        }

        //This method checks salary,requieres from user to enter number of the employee,select new position and new salary.
        private static void EditEmployee(ref HumanResourceManagerService humanService)
        {
            Console.WriteLine("Enter the number of the employee");
            string nomre = Console.ReadLine();
            foreach(var item in humanService.Departments)
            {
                foreach(var item1 in item.employees)
                {
                    if (nomre == item1.No)
                    {
                        Console.WriteLine("Entered number is right.There is such a employee");
                        Console.WriteLine("Enter new salary");

                        EditStart:
                        int newSalary = int.Parse(Console.ReadLine());
                        if (newSalary >= 250)
                        {
                            Console.WriteLine("Select new position");
                            string[] positionName = Enum.GetNames(typeof(PositionType));
                            for (int i = 0; i < positionName.Length; i++)
                            {
                                Console.WriteLine($"{i + 1}-{positionName[i]}");
                            }

                            string typeStr;
                            int typeNum;
                            do
                            {
                                Console.WriteLine("Select");
                                typeStr = Console.ReadLine();
                            }
                            while (!int.TryParse(typeStr, out typeNum) || typeNum < 0 || typeNum > positionName.Length);
                            PositionType positionType = (PositionType)(typeNum);
                            humanService.EditEmployee(nomre, newSalary, positionType);

                        }
                        else
                        {
                            Console.WriteLine("Salary can't be less than 250");
                            goto EditStart;
                        }
                    }
                
                } 
            }
        }

        //This method checks salary,displays current position and requires from to select one of those positions,then adds new employee.
        private static void AddEmployee(ref HumanResourceManagerService humanService)
        {
            Console.WriteLine("Enter fullname of employee");

            string name = Console.ReadLine();

            Console.WriteLine("Enter salary");
            int salary = int.Parse(Console.ReadLine());
            try
            {
                if (salary > 250)
                {
                    //Console.WriteLine("Salary amount is sufficient");
                }
                else
                {
                    Console.WriteLine("Salary amount isn't sufficient. " + "Enter right amount of salary!");
                }

                Console.WriteLine("Select position:");

                string[] positionName = Enum.GetNames(typeof(PositionType));
                for (int i = 0; i < positionName.Length; i++)
                {
                    Console.WriteLine($"{i + 1}-{positionName[i]}");


                }

                string typeStr;
                int typeNum;
                do
                {
                    Console.WriteLine("Select");
                    typeStr = Console.ReadLine();
                }
                while (!int.TryParse(typeStr, out typeNum) || typeNum < 0 || typeNum > positionName.Length);
                PositionType positionType = (PositionType)typeNum;

                Console.WriteLine("To which department do you want to add employee?");
                string elavedep = Console.ReadLine();

                foreach (var item in humanService.Departments)
                {
                    if (item.Name == elavedep)
                    {
                        humanService.AddEmployee(name, positionType, salary, item);
                        return;
                    }
                }
                Console.WriteLine("There is no such a department");
            }
            catch
            {
                Console.WriteLine("Please,check the operation");
            }
           
        }

        //This method requires department from user,checks entered department,then checks employee and then shows employees of the entered department.
        private static void ShowEmployeesOfDepartment(ref HumanResourceManagerService humanService)
        {
            Console.WriteLine("Which department of employees do you want to see?");
            string depemployee = Console.ReadLine();
            foreach (var item in humanService.Departments)
            {
                if (item.Name==depemployee)
                {
                    foreach (var item1 in item.employees)
                    {
                        if (item1 != null)
                        {
                          Console.WriteLine($"Number:{item1.No} Fullname:{item1.FullName} Position: {item1.Position} Salary: {item1.Salary}");
                        }
                    }
                    return;
                }
            }
            Console.WriteLine("There is no such a department"); 
        }

        //This method checks requirement on entered old and new department name.
        //And then edits name,assigns new department name
        private static void EditDepartment(ref HumanResourceManagerService humanService)
        {
            
            Console.WriteLine("Enter the old department name");
            oldDepStart:
            string oldDepName=Console.ReadLine();
            if (oldDepName.Length < 2)
            {
                Console.WriteLine("Department name can't be less than 2 characters");
                goto oldDepStart;
            }
            Console.WriteLine("Enter new department name");
            newDepStart:
            string newDepName = Console.ReadLine();
            if (newDepName.Length < 2)
            {
                Console.WriteLine("Department name can't be less than 2 characters");
                goto newDepStart;
            }
            Console.WriteLine("Edits were performed");

            foreach(var item in humanService.Departments)
            {

                foreach(var item1 in item.employees)
                {
                    item1.No = item1.No.Replace(item1.No.Substring(0, 2), newDepName.Substring(0, 2));
                    item1.DepartmentName = newDepName;
                }
            }
            humanService.EditDepartment(oldDepName, newDepName);
        }

        //This method checks whether is there any department in the system or not,if yes shows department name,number of employees,average salary of the employee in the department.
        private static void ShowDepartments(ref HumanResourceManagerService humanService)
        {

          try
            { 
             
            if (humanService.Departments.Length == 0)
            {
                Console.WriteLine("There is no department");
                Console.WriteLine("Please add new department and add new employee.Select option 2,then option 6 for adding new employee.");
                return;

            }
            else
            {
                Console.WriteLine($"Departments: ");
                foreach (var item in humanService.GetDepartments())
                {

                    Console.WriteLine($"Department name: {item.Name} The number of employees: { item.employees.Length} Average salary of the employees: {item.CalcSalaryAverage()}");
                    
                }
            }

            }
           catch
               {
                Console.WriteLine("Please,add new employee");
               }
            
            
        }

        //This method checks employees in the departments,then shows their numbers,fullnames,positions and salaries.
        private static void ShowEmployees(ref HumanResourceManagerService service )
        {
            foreach (var item in service.Departments)
            {
                foreach (var item1 in item.employees)
                {
                    if (item1 != null)
                    {
                         Console.WriteLine($"Number:{item1.No} Fullname:{item1.FullName} Position: {item1.Position} Salary: {item1.Salary}");
                    }
                }
            }
        }
    }
}
