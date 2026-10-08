# KampusRadar – demo projekt za ishod I1 (ASP.NET Core Razor Pages, .NET 10)

Oglasna ploča studentskih događanja: hackathoni, radionice, turniri, predavanja.
Projekt pokriva **svih 13 kriterija ishoda I1** (20 bodova) i služi kao primjer
uz predavanje „Razor Pages u .NET 10”.

## Pokretanje

**Visual Studio 2026**
1. Otvori `KampusRadar.slnx` (File → Open → Project/Solution).
2. Pričekaj da se NuGet paketi vrate (Restore).
3. Pritisni **F5** (ili Ctrl+F5 bez debuggera).

**Visual Studio Code / terminal** (.NET 10 SDK)
```bash
cd KampusRadar
dotnet watch        # pokreće aplikaciju i osvježava je pri svakoj promjeni koda
```

Pri prvom pokretanju aplikacija sama stvara SQLite bazu `kampusradar.db`
(`Database.Migrate()` u `Program.cs`) i upisuje 11 početnih događaja
(`UseSeeding` → `Data/PocetniPodaci.cs`). Datumi su relativni u odnosu na
današnji dan. Za „reset” podataka obriši `kampusradar.db` i ponovno pokreni.

Ako preglednik upozori na HTTPS certifikat: `dotnet dev-certs https --trust`
(ili pokreni profil **http**).

## Gdje je što (kriteriji I1)

| Kriterij | Bodovi | Datoteke |
|---|---|---|
| Potpuna izmjena defaultnog dizajna | 3 | `wwwroot/css/kampus.css`, `Pages/Shared/_Layout.cshtml` |
| Vlastita CSS datoteka (≥ 2 pravila) | 1 | `wwwroot/css/kampus.css` |
| Vlastita JS datoteka (≥ 1 funkcija) | 1 | `wwwroot/js/kampus.js`, `wwwroot/js/validacija-hr.js` |
| Lokalne slike | 1 | `wwwroot/images/` |
| Entity Framework + baza | 2 | `Data/AppDbContext.cs`, `Migrations/`, `appsettings.json` |
| Layout s ≥ 2 partiala | 2 | `_Layout.cshtml` → `_Navigacija`, `_StatusPoruka`, `_Podnozje` |
| Model s ≥ 5 svojstava | 1 | `Models/Dogadaj.cs` (10 svojstava) |
| Model s ≥ 4 tipa podataka | 1 | `Models/Dogadaj.cs` (int, string, enum, DateTime, decimal, bool) |
| CRUD stranice | 3 | `Pages/Dogadaji/Index, Create, Edit, Details, Delete` |
| Forma za filtriranje | 2 | `Pages/Dogadaji/Filtriranje.cshtml(.cs)` |
| Sortirani prikaz svih podataka | 1 | `Pages/Dogadaji/Sortirano.cshtml(.cs)` |
| Izbornik u layoutu | 1 | `Pages/Shared/_Navigacija.cshtml` |
| Lokalizacija na hrvatski | 1 | `Program.cs` (hr-HR), `Models/*` (Display, ErrorMessage), `Infrastruktura/HrvatskePoruke.cs` |

Ista tablica prikazana je i u samoj aplikaciji na stranici **O projektu**.

## Migracije (kad promijeniš model)

| Visual Studio 2026 – Package Manager Console | Terminal (.NET CLI) |
|---|---|
| `Add-Migration NazivPromjene` | `dotnet ef migrations add NazivPromjene` |
| `Update-Database` | `dotnet ef database update` |

Za CLI je potreban alat: `dotnet tool install --global dotnet-ef`.

## Dvije zamke koje projekt rješava

1. **SQLite i `decimal`** – SQLite nema decimal tip pa EF Core ne može uspoređivati
   ni sortirati cijenu u bazi. Rješenje u `AppDbContext.OnModelCreating`:
   `.Property(d => d.Cijena).HasConversion<double>()`.
2. **Decimalni zarez u obrascima** – jQuery Validation po defaultu prihvaća samo
   točku. Rješenje: `wwwroot/js/validacija-hr.js` (učitava se u `_ValidationScriptsPartial`).

## Rješavanje problema

- **`PendingModelChangesWarning` pri pokretanju** – model i migracije nisu usklađeni.
  Dodaj novu migraciju (`Add-Migration ...`) ili, ako tek počinješ, obriši mapu
  `Migrations` i `kampusradar.db` pa napravi `Add-Migration InicijalnaMigracija`.
- **Paketi se ne vraćaju** – provjeri da je instaliran .NET 10 SDK (`dotnet --list-sdks`).

## Licence resursa

Fontovi Public Sans i Big Shoulders Display (SIL Open Font License, `wwwroot/fonts/`),
ikone kategorija Tabler Icons (MIT), Bootstrap 5.3 i jQuery (MIT).
