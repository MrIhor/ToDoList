using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ToDoListApp.Data.DataContext _context;

    public IndexModel(ILogger<IndexModel> logger,
        ToDoListApp.Data.DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public IList<Exercise> Exercise { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Exercise = await _context.Exercises.ToListAsync();
    }
}

