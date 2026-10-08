namespace KampusRadar.Infrastruktura;

/// <summary>Hrvatska množina: 1 događaj, 2 događaja, 5 događaja, 21 događaj...</summary>
public static class Hrvatski
{
    public static string Mnozina(int broj, string jednina, string mnozina)
    {
        var zadnja = broj % 10;
        var zadnjeDvije = broj % 100;
        return zadnja == 1 && zadnjeDvije != 11 ? jednina : mnozina;
    }
}
