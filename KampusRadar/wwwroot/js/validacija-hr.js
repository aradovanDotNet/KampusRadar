// ============================================================================
// jQuery Validation + hrvatski decimalni zarez
// ----------------------------------------------------------------------------
// Problem: klijentska validacija po defaultu prihvaća samo točku (12.50), a
// poslužitelj s kulturom hr-HR očekuje zarez (12,50). Bez ove datoteke obrazac
// odbija ispravan unos "12,50" porukom "Polje mora biti broj".
// Učitava se u _ValidationScriptsPartial NAKON jquery.validate.js.
// ============================================================================

"use strict";

(function ($) {
    if (!$ || !$.validator) return;

    const uBroj = (vrijednost) => parseFloat(String(vrijednost).replace(/\s/g, "").replace(",", "."));

    // Broj: dopušta "12", "12,5", "12,50" i "12.50"
    $.validator.methods.number = function (vrijednost, element) {
        return this.optional(element) || /^-?\d+([.,]\d+)?$/.test(String(vrijednost).trim());
    };

    // Raspon ([Range] atribut) mora usporediti broj sa zarezom
    $.validator.methods.range = function (vrijednost, element, param) {
        const broj = uBroj(vrijednost);
        return this.optional(element) || (broj >= param[0] && broj <= param[1]);
    };
})(window.jQuery);
