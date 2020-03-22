using Microsoft.EntityFrameworkCore;
using RazorPages.Models;
using RazorPages.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RazorPages.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }


        public Employee Add(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public Employee Delete(int id)
        {
           var employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _context.Employees;
            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == dept.Value);

            }

            return query.GroupBy(e => e.Department)
                    .Select(g => new DeptHeadCount
                    {
                        Department = g.Key.Value,
                        Count = g.Count()
                    }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = _context.Employees.FromSqlRaw("select * from Employees");
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees
                .FromSqlRaw<Employee>("spGetEmployeeById {0}", id).ToList().FirstOrDefault();
                
                    
        }

        public IEnumerable<Employee> SearchEmp(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _context.Employees;
            }

            return _context.Employees.Where(e => e.Name.Contains(searchTerm) || e.Email.Contains(searchTerm));
        }

        public Employee Update(Employee updateEmployee)
        {
            var employee = _context.Employees.Attach(updateEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updateEmployee;
        }
    }
}
