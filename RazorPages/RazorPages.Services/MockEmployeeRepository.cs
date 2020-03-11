using RazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorPages.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@pragimtech.com", PhotoPath="mary.jpg" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@pragimtech.com", PhotoPath="john.jpg" },
                new Employee() { Id = 3, Name = "Sara", Department = Dept.IT, Email = "sara@pragimtech.com", PhotoPath="sara.png" },
                new Employee() { Id = 4, Name = "David", Department = Dept.Payroll, Email = "david@pragimtech.com" }
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _employeeList.FirstOrDefault(c => c.Id == id);
            return employee;
        }

        public Employee Update(Employee updateEmployee)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == updateEmployee.Id);
            if (employee != null)
            {
                employee.Name = updateEmployee.Name;
                employee.Email = updateEmployee.Email;
                employee.Department = updateEmployee.Department;
                employee.PhotoPath = updateEmployee.PhotoPath;

            }

            return employee;
        }
    }
}
