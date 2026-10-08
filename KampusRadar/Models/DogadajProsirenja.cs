using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KampusRadar.Models;

/// <summary>Pomoćne metode koje koriste stranice i partiali.</summary>
public static class DogadajProsirenja
{
    /// <summary>Hrvatski naziv kategorije iz [Display(Name = ...)].</summary>
    public static string Naziv(this KategorijaDogadaja kategorija) =>
        typeof(KategorijaDogadaja)
            .GetMember(kategorija.ToString())[0]
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? kategorija.ToString();

    /// <summary>Naziv datoteke ikone u wwwroot/images/kategorije.</summary>
    public static string Slug(this KategorijaDogadaja kategorija) =>
        kategorija.ToString().ToLowerInvariant();

    /// <summary>Cijena za prikaz: "Besplatno" ili "12,50 €" (decimalni zarez iz hr-HR kulture).</summary>
    public static string CijenaZaPrikaz(this Dogadaj dogadaj) =>
        dogadaj.Cijena == 0 ? "Besplatno" : $"{dogadaj.Cijena:N2} €";

    public static bool JeProsao(this Dogadaj dogadaj) => dogadaj.Pocetak < DateTime.Now;
}
