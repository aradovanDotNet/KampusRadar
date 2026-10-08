using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KampusRadar.Pages;

/// <summary>
/// Lokalizirana stranica za HTTP status kodove (404, 400...).
/// Uključena u Program.cs: app.UseStatusCodePagesWithReExecute("/StatusKod", "?kod={0}")
/// </summary>
[IgnoreAntiforgeryToken]
public class StatusKodModel : PageModel
{
    public int Kod { get; private set; }
    public string Naslov { get; private set; } = "";
    public string Opis { get; private set; } = "";

    public void OnGet(int kod)
    {
        Kod = kod;
        (Naslov, Opis) = kod switch
        {
            404 => ("Stranica nije pronađena", "Adresa ne postoji ili je događaj obrisan."),
            400 => ("Neispravan zahtjev", "Provjeri adresu i pokušaj ponovno."),
            _   => ("Nešto nije u redu", $"Poslužitelj je vratio status {kod}.")
        };
    }
}
