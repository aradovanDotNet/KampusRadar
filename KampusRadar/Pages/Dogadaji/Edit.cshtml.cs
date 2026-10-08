using KampusRadar.Data;
using KampusRadar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Pages.Dogadaji;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Kažemo EF-u: ovaj objekt postoji u bazi i izmijenjen je → UPDATE
        _context.Attach(Dogadaj).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DogadajPostoji(Dogadaj.Id))
            {
                return NotFound();
            }
            throw;
        }

        TempData["Poruka"] = "Promjene su spremljene.";
        return RedirectToPage("./Details", new { id = Dogadaj.Id });
    }

    private bool DogadajPostoji(int id) => _context.Dogadaji.Any(e => e.Id == id);
}
