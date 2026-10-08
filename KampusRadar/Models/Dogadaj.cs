using System.ComponentModel.DataAnnotations;

namespace KampusRadar.Models;

/// <summary>
/// Model za I1: 10 svojstava (traži se barem 5) i 6 različitih tipova
/// podataka (traži se barem 4): int, string, enum, DateTime, decimal, bool.
/// </summary>
public class Dogadaj
{
    public int Id { get; set; }                                   // int (primarni ključ)

    [Required(ErrorMessage = "Unesi naziv događaja.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Naziv mora imati između {2} i {1} znakova.")]
    [Display(Name = "Naziv")]
    public string Naziv { get; set; } = string.Empty;             // string

    [StringLength(500, ErrorMessage = "Opis može imati najviše {1} znakova.")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Opis")]
    public string? Opis { get; set; }                             // string (nije obavezan)

    [Required(ErrorMessage = "Odaberi kategoriju.")]
    [Display(Name = "Kategorija")]
    public KategorijaDogadaja Kategorija { get; set; }            // enum

    [Required(ErrorMessage = "Unesi datum i vrijeme početka.")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy. HH:mm}")]
    [Display(Name = "Početak")]
    public DateTime Pocetak { get; set; }                         // DateTime

    [Required(ErrorMessage = "Unesi lokaciju.")]
    [StringLength(80, ErrorMessage = "Lokacija može imati najviše {1} znakova.")]
    [Display(Name = "Lokacija")]
    public string Lokacija { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unesi cijenu (0 = besplatno).")]
    [Range(0, 500, ErrorMessage = "Cijena mora biti između {1} i {2} €.")]
    [DisplayFormat(DataFormatString = "{0:N2} €")]
    [Display(Name = "Cijena (€)")]
    public decimal Cijena { get; set; }                           // decimal

    [Required(ErrorMessage = "Unesi broj mjesta.")]
    [Range(1, 5000, ErrorMessage = "Broj mjesta mora biti između {1} i {2}.")]
    [Display(Name = "Broj mjesta")]
    public int BrojMjesta { get; set; }

    [Display(Name = "Potrebna prijava")]
    public bool PotrebnaPrijava { get; set; }                     // bool
}
