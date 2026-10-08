using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace KampusRadar.Infrastruktura;

/// <summary>
/// Lokalizacija ugrađenih poruka model bindinga (npr. "The value 'abc' is not valid").
/// Poruke iz atributa ([Required], [Range]...) lokaliziramo izravno u modelu (ErrorMessage).
/// </summary>
public static class HrvatskePoruke
{
    public static void PostaviHrvatskePoruke(this DefaultModelBindingMessageProvider poruke)
    {
        poruke.SetValueMustBeANumberAccessor(polje => $"Polje {polje} mora biti broj.");
        poruke.SetValueMustNotBeNullAccessor(polje => $"Polje {polje} je obavezno.");
        poruke.SetAttemptedValueIsInvalidAccessor((vrijednost, polje) => $"Vrijednost '{vrijednost}' nije ispravna za polje {polje}.");
        poruke.SetUnknownValueIsInvalidAccessor(polje => $"Unesena vrijednost nije ispravna za polje {polje}.");
        poruke.SetValueIsInvalidAccessor(vrijednost => $"Vrijednost '{vrijednost}' nije ispravna.");
        poruke.SetMissingBindRequiredValueAccessor(polje => $"Nedostaje vrijednost za polje {polje}.");
        poruke.SetNonPropertyValueMustBeANumberAccessor(() => "Vrijednost mora biti broj.");
    }
}
