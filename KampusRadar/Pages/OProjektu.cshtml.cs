using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KampusRadar.Pages;

public class OProjektuModel : PageModel
{
    public record Kriterij(string Opis, int Bodovi, string Lokacija);

    public IReadOnlyList<Kriterij> Kriteriji { get; } =
    [
        new("Potpuna izmjena defaultnog dizajna", 3, "wwwroot/css/kampus.css, Pages/Shared/_Layout.cshtml"),
        new("Vlastita CSS datoteka (barem 2 pravila)", 1, "wwwroot/css/kampus.css"),
        new("Vlastita JS datoteka (barem 1 funkcija)", 1, "wwwroot/js/kampus.js, wwwroot/js/validacija-hr.js"),
        new("Lokalne slike", 1, "wwwroot/images/ (logo.svg, hero-kampus.webp, kategorije/*.svg)"),
        new("Entity Framework + baza podataka", 2, "Data/AppDbContext.cs, Migrations/, appsettings.json"),
        new("Layout s barem dva partiala", 2, "Pages/Shared/_Layout.cshtml + _Navigacija, _StatusPoruka, _Podnozje"),
        new("Model s barem 5 svojstava", 1, "Models/Dogadaj.cs (10 svojstava)"),
        new("Model s barem 4 tipa podataka", 1, "Models/Dogadaj.cs (int, string, enum, DateTime, decimal, bool)"),
        new("Pregled, unos, izmjena i brisanje (CRUD)", 3, "Pages/Dogadaji/Index, Create, Edit, Details, Delete"),
        new("Stranica s formom za filtriranje", 2, "Pages/Dogadaji/Filtriranje.cshtml(.cs)"),
        new("Stranica sa sortiranim podacima", 1, "Pages/Dogadaji/Sortirano.cshtml(.cs)"),
        new("Izbornik u layoutu", 1, "Pages/Shared/_Navigacija.cshtml"),
        new("Lokalizacija na hrvatski", 1, "Program.cs (hr-HR), Models (Display, ErrorMessage), Infrastruktura/HrvatskePoruke.cs")
    ];

    public void OnGet()
    {
    }
}
