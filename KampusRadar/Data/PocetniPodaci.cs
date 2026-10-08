using KampusRadar.Models;
using Microsoft.EntityFrameworkCore;

namespace KampusRadar.Data;

/// <summary>
/// Početni (demo) podaci. Poziva ih EF Core preko UseSeeding/UseAsyncSeeding
/// (vidi Program.cs) kad se baza kreira ili migrira. Datumi su relativni
/// u odnosu na današnji dan pa događaji uvijek izgledaju "svježe".
/// </summary>
public static class PocetniPodaci
{
    public static void Napuni(DbContext context)
    {
        var dogadaji = context.Set<Dogadaj>();
        if (dogadaji.Any())
        {
            return; // podaci već postoje – ne diramo ih
        }

        dogadaji.AddRange(Generiraj());
        context.SaveChanges();
    }

    public static async Task NapuniAsync(DbContext context, CancellationToken ct)
    {
        var dogadaji = context.Set<Dogadaj>();
        if (await dogadaji.AnyAsync(ct))
        {
            return;
        }

        dogadaji.AddRange(Generiraj());
        await context.SaveChangesAsync(ct);
    }

    private static List<Dogadaj> Generiraj()
    {
        var danas = DateTime.Today;

        return
        [
            new()
            {
                Naziv = "Radionica: Git bez straha",
                Opis = "Commit, branch, merge i rješavanje konflikata na pravom projektu. Ponesi laptop.",
                Kategorija = KategorijaDogadaja.Radionica,
                Pocetak = danas.AddDays(2).AddHours(16),
                Lokacija = "Učionica 12",
                Cijena = 0m, BrojMjesta = 25, PotrebnaPrijava = true
            },
            new()
            {
                Naziv = "LAN party: CS2 turnir",
                Opis = "Timovi od pet igrača, eliminacijski sustav. Nagrade za tri najbolja tima.",
                Kategorija = KategorijaDogadaja.Gaming,
                Pocetak = danas.AddDays(4).AddHours(18),
                Lokacija = "Studentski centar, dvorana B",
                Cijena = 3m, BrojMjesta = 40, PotrebnaPrijava = true
            },
            new()
            {
                Naziv = "Predavanje: .NET 10 ispod haube",
                Opis = "Što se zapravo događa od HTTP zahtjeva do HTML odgovora u ASP.NET Coreu.",
                Kategorija = KategorijaDogadaja.Predavanje,
                Pocetak = danas.AddDays(6).AddHours(12),
                Lokacija = "Velika predavaonica",
                Cijena = 0m, BrojMjesta = 120, PotrebnaPrijava = false
            },
            new()
            {
                Naziv = "Hackathon: AI za lokalnu zajednicu",
                Opis = "24 sata, timovi do četiri osobe, mentori iz industrije. Hrana i kava osigurani.",
                Kategorija = KategorijaDogadaja.Hackathon,
                Pocetak = danas.AddDays(9).AddHours(9),
                Lokacija = "Računalni laboratorij L2",
                Cijena = 0m, BrojMjesta = 60, PotrebnaPrijava = true
            },
            new()
            {
                Naziv = "Studentski futsal kup",
                Opis = "Turnir studentskih ekipa. Prijava ekipe do petka.",
                Kategorija = KategorijaDogadaja.Sport,
                Pocetak = danas.AddDays(11).AddHours(19),
                Lokacija = "Sportska dvorana",
                Cijena = 2m, BrojMjesta = 80, PotrebnaPrijava = true
            },
            new()
            {
                Naziv = "Karijerni sajam IT tvrtki",
                Opis = "Upoznaj tvrtke koje traže studente za praksu i prvi posao. Ponesi životopis.",
                Kategorija = KategorijaDogadaja.Karijere,
                Pocetak = danas.AddDays(14).AddHours(10),
                Lokacija = "Atrij",
                Cijena = 0m, BrojMjesta = 300, PotrebnaPrijava = false
            },
            new()
            {
                Naziv = "Radionica: Docker u 90 minuta",
                Opis = "Od Dockerfilea do pokrenutog kontejnera – priprema za ishod I4.",
                Kategorija = KategorijaDogadaja.Radionica,
                Pocetak = danas.AddDays(17).AddHours(15),
                Lokacija = "Računalni laboratorij L1",
                Cijena = 0m, BrojMjesta = 20, PotrebnaPrijava = true
            },
            new()
            {
                Naziv = "Brucošijada",
                Opis = "Najveća studentska zabava semestra. Ulaznice na ulazu.",
                Kategorija = KategorijaDogadaja.Zabava,
                Pocetak = danas.AddDays(20).AddHours(21),
                Lokacija = "Klub Kampus",
                Cijena = 8.5m, BrojMjesta = 400, PotrebnaPrijava = false
            },
            new()
            {
                Naziv = "Retro gaming večer",
                Opis = "Konzole iz devedesetih, turnir u Tetrisu i puno nostalgije.",
                Kategorija = KategorijaDogadaja.Gaming,
                Pocetak = danas.AddDays(23).AddHours(19),
                Lokacija = "Studentski klub",
                Cijena = 0m, BrojMjesta = 50, PotrebnaPrijava = false
            },
            new()
            {
                Naziv = "Hackathon: Zeleni kampus",
                Opis = "Aplikacije za praćenje potrošnje energije i otpada na kampusu.",
                Kategorija = KategorijaDogadaja.Hackathon,
                Pocetak = danas.AddDays(30).AddHours(9),
                Lokacija = "Računalni laboratorij L2",
                Cijena = 5m, BrojMjesta = 40, PotrebnaPrijava = true
            },
            new()
            {
                Naziv = "Dan otvorenih vrata",
                Opis = "Predstavljanje studijskih programa budućim studentima.",
                Kategorija = KategorijaDogadaja.Predavanje,
                Pocetak = danas.AddDays(-5).AddHours(11),
                Lokacija = "Atrij",
                Cijena = 0m, BrojMjesta = 200, PotrebnaPrijava = false
            }
        ];
    }
}
