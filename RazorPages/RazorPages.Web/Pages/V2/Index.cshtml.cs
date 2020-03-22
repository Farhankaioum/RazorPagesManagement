using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPages.Models;
using RazorPages.Services.Data;

namespace RazorPages.Web.Pages.V2
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Services.Data.AppDbContext _context;

        public IndexModel(RazorPages.Services.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Employees.ToListAsync();
        }
    }
}
