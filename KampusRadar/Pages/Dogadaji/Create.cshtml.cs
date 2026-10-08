using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KampusRadar.Pages.Dogadaji;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Dogadaj Dogadaj { get; set; } = default!;

    public IActionResult OnGet()
    {
        // Razumne početne vrijednosti umjesto 01.01.0001.
        Dogadaj = new Dogadaj
        {
            Pocetak = DateTime.Today.AddDays(7).AddHours(18),
            BrojMjesta = 30
        };
        return Page();
    }

    // Zaštita od overpostinga: https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();      // vrati obrazac s porukama o greškama
        }

        _context.Dogadaji.Add(Dogadaj);
        await _context.SaveChangesAsync();

        TempData["Poruka"] = $"Događaj „{Dogadaj.Naziv}” je dodan.";
        return RedirectToPage("./Index");   // Post-Redirect-Get
    }
}
