using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ToDoListApp.Data.DataContext _context;
        private readonly ILogger _logger;

        public DeleteModel (ILogger<DeleteModel> logger, ToDoListApp.Data.DataContext context)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Exercise? Exercise { get; set; }
        public string ErrorMessage { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Exercise = await _context.Exercises
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (Exercise == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            try
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}
