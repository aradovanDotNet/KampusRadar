using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages.Dogadaji;

// Generirano scaffoldingom (Razor Pages using Entity Framework (CRUD)), zatim lokalizirano.
public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Dogadaj> Dogadaji { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Dogadaji = await _context.Dogadaji
            .AsNoTracking()
            .OrderBy(d => d.Pocetak)
            .ToListAsync();
    }
}
