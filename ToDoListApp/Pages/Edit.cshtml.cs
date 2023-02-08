using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoListApp.Models;

namespace ToDoListApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly ToDoListApp.Data.DataContext _context;

        public EditModel(ToDoListApp.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Exercise? Exercise { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exercise = await _context.Exercises.FindAsync(id);

            if (Exercise == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var exerciseToUpdate = await _context.Exercises.FindAsync(id);

            if (exerciseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Exercise>(
                exerciseToUpdate,
                "exercise",
                e => e.Title, e => e.Done, e => e.Descriptions))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
