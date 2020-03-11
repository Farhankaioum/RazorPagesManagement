using RazorPages.Models;
using System;
using System.Collections.Generic;

namespace RazorPages.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        Employee Update(Employee updateEmployee);
        Employee Add(Employee newEmployee);
        Employee Delete(int id);
    }
}
