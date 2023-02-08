using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoListApp.Models;

namespace ToDoListApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ToDoListApp.Data.DataContext _context;

        public CreateModel(ToDoListApp.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Exercise Exercise { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _context.Exercises.Add(Exercise);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
