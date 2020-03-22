using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;
using RazorPages.Services;

namespace RazorPages.Web
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public IEnumerable<Employee> Employees { get; set; }

        public IndexModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { set; get; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Employees = _employeeRepository.SearchEmp(SearchTerm);
            }
            else
            {
                Employees = _employeeRepository.GetAllEmployees();
            }

            
        }
    }
}