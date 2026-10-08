using System.ComponentModel.DataAnnotations;

namespace KampusRadar.Models;

/// <summary>
/// Enum je peti tip podatka u modelu. U bazi se sprema kao INTEGER,
/// a [Display] određuje hrvatski naziv koji korisnik vidi.
/// </summary>
public enum KategorijaDogadaja
{
    [Display(Name = "Hackathon")]  Hackathon,
    [Display(Name = "Gaming")]     Gaming,
    [Display(Name = "Radionica")]  Radionica,
    [Display(Name = "Predavanje")] Predavanje,
    [Display(Name = "Karijere")]   Karijere,
    [Display(Name = "Sport")]      Sport,
    [Display(Name = "Zabava")]     Zabava
}
