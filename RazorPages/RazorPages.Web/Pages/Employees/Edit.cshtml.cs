using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }


        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = _employeeRepository.GetEmployeeById(id.Value);
            }
            else
            {
                Employee = new Employee();
            }
            
            if(Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

       

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    // For checking existing photo, if find then delete
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Employee.PhotoPath = ProcessUploadedFile(Photo); ;
                }

                if (Employee.Id > 0)
                {
                    Employee = _employeeRepository.Update(Employee);
                }
                else
                {
                    Employee = _employeeRepository.Add(Employee);

                }

                return RedirectToPage("Index");
            }

            return Page();
        }


        // For update notification Preferences
        public IActionResult OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
                Message = "Thank you for turning on notifications";
            else
                Message = "You have turned off email notifications";

            Employee = _employeeRepository.GetEmployeeById(id);

            TempData["message"] = Message;

            return RedirectToPage("Details", new {id = id});
        }

        // For compute photo path file name
        private string ProcessUploadedFile(IFormFile Photo)
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var filestream = new FileStream(filePath, FileMode.Create);

                Photo.CopyTo(filestream);
            }

            return uniqueFileName;
        }
    }
}