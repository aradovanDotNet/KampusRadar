using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Dogadaj> Uskoro { get; private set; } = [];
    public int BrojNadolazecih { get; private set; }

    public async Task OnGetAsync()
    {
        var sada = DateTime.Now;

        Uskoro = await _context.Dogadaji
            .AsNoTracking()
            .Where(d => d.Pocetak >= sada)
            .OrderBy(d => d.Pocetak)
            .Take(3)
            .ToListAsync();

        BrojNadolazecih = await _context.Dogadaji.CountAsync(d => d.Pocetak >= sada);
    }
}
