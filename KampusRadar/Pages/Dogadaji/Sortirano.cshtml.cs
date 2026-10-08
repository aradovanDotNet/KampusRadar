using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages.Dogadaji;

/// <summary>Kriterij I1 (1 b): stranica koja prikazuje sve podatke sortirane prema ključu.</summary>
public class SortiranoModel : PageModel
{
    private readonly AppDbContext _context;

    public SortiranoModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string Poredak { get; set; } = "datum";

    [BindProperty(SupportsGet = true)]
    public bool Silazno { get; set; }

    public IList<Dogadaj> Dogadaji { get; private set; } = [];

    public string NazivPoretka => Poredak switch
    {
        "naziv" => "naziv",
        "kategorija" => "kategorija",
        "cijena" => "cijena",
        _ => "datum početka"
    };

    public string AriaSort(string kljuc) =>
        Poredak == kljuc ? (Silazno ? "descending" : "ascending") : "none";

    public async Task OnGetAsync()
    {
        IQueryable<Dogadaj> upit = _context.Dogadaji.AsNoTracking();

        upit = Poredak switch
        {
            "naziv"      => Silazno ? upit.OrderByDescending(d => d.Naziv)      : upit.OrderBy(d => d.Naziv),
            "kategorija" => Silazno ? upit.OrderByDescending(d => d.Kategorija) : upit.OrderBy(d => d.Kategorija),
            "cijena"     => Silazno ? upit.OrderByDescending(d => d.Cijena)     : upit.OrderBy(d => d.Cijena),
            _            => Silazno ? upit.OrderByDescending(d => d.Pocetak)    : upit.OrderBy(d => d.Pocetak),
        };

        Dogadaji = await upit.ToListAsync();
    }
}
