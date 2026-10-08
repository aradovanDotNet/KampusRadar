using System.ComponentModel.DataAnnotations;
using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages.Dogadaji;

/// <summary>Kriterij I1 (2 b): dodatna stranica s formom za filtriranje.</summary>
public class FiltriranjeModel : PageModel
{
    private readonly AppDbContext _context;

    public FiltriranjeModel(AppDbContext context)
    {
        _context = context;
    }

    // SupportsGet = true → vrijednosti se povezuju i iz GET zahtjeva (query string)
    [BindProperty(SupportsGet = true)]
    [Display(Name = "Pojam")]
    public string? Pojam { get; set; }

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Kategorija")]
    public KategorijaDogadaja? Kategorija { get; set; }

    [BindProperty(SupportsGet = true)]
    [Range(0, 500, ErrorMessage = "Najviša cijena mora biti između {1} i {2} €.")]
    [Display(Name = "Najviša cijena (€)")]
    public decimal? MaksCijena { get; set; }

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Samo nadolazeći")]
    public bool SamoNadolazeci { get; set; } = true;

    public SelectList Kategorije { get; private set; } = default!;
    public IList<Dogadaj> Rezultati { get; private set; } = [];

    public async Task OnGetAsync()
    {
        // Padajući izbornik: vrijednost = naziv enuma (čitljiv URL), tekst = hrvatski naziv
        Kategorije = new SelectList(
            Enum.GetValues<KategorijaDogadaja>().Select(k => new { Vrijednost = k.ToString(), Tekst = k.Naziv() }),
            "Vrijednost", "Tekst");

        // IQueryable: upit se gradi korak po korak, a izvršava tek kod ToListAsync()
        IQueryable<Dogadaj> upit = _context.Dogadaji.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(Pojam))
        {
            var uzorak = $"%{Pojam.Trim()}%";
            upit = upit.Where(d => EF.Functions.Like(d.Naziv, uzorak)
                                || (d.Opis != null && EF.Functions.Like(d.Opis, uzorak)));
        }

        if (Kategorija.HasValue)
        {
            upit = upit.Where(d => d.Kategorija == Kategorija.Value);
        }

        if (MaksCijena.HasValue && ModelState.IsValid)
        {
            upit = upit.Where(d => d.Cijena <= MaksCijena.Value);
        }

        if (SamoNadolazeci)
        {
            var sada = DateTime.Now;
            upit = upit.Where(d => d.Pocetak >= sada);
        }

        Rezultati = await upit.OrderBy(d => d.Pocetak).ToListAsync();
    }
}
