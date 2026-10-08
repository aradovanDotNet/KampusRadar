using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages.Dogadaji;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Dogadaj Dogadaj { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dogadaj = await _context.Dogadaji.FirstOrDefaultAsync(m => m.Id == id);
        if (dogadaj == null)
        {
            return NotFound();
        }

        Dogadaj = dogadaj;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var dogadaj = await _context.Dogadaji.FindAsync(id);
        if (dogadaj != null)
        {
            _context.Dogadaji.Remove(dogadaj);
            await _context.SaveChangesAsync();
            TempData["Poruka"] = $"Događaj „{dogadaj.Naziv}” je obrisan.";
        }

        return RedirectToPage("./Index");
    }
}
