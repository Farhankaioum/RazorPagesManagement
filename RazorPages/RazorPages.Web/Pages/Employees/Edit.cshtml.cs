using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Models;
using RazorPages.Services;

namespace RazorPages.Web
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EditModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Employee { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployeeById(id);
            if(Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(Employee employee)
        {
           Employee = _employeeRepository.Update(employee);
            return RedirectToPage("Index");
        }
    }
}