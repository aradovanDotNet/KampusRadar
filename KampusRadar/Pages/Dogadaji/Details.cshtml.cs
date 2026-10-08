using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages.Dogadaji;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Dogadaj Dogadaj { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dogadaj = await _context.Dogadaji.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        if (dogadaj == null)
        {
            return NotFound();
        }

        Dogadaj = dogadaj;
        return Page();
    }
}
