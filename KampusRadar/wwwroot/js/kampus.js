// ============================================================================
// KampusRadar – vlastita JavaScript datoteka (kriterij I1: 1 b)
// Uključena u layoutu: <script src="~/js/kampus.js" asp-append-version="true">
// ============================================================================

"use strict";

/**
 * Prebacuje svijetlu/tamnu temu i pamti odabir u localStorageu.
 * Bootstrap 5.3 čita atribut data-bs-theme, a naš CSS mijenja boje (tokene).
 */
function promijeniTemu() {
    const html = document.documentElement;
    const nova = html.getAttribute("data-bs-theme") === "dark" ? "light" : "dark";
    html.setAttribute("data-bs-theme", nova);
    localStorage.setItem("kampus-tema", nova);
    osvjeziGumbTeme();
}

/** Usklađuje tekst i aria-pressed gumba s trenutnom temom. */
function osvjeziGumbTeme() {
    const gumb = document.querySelector("[data-promijeni-temu]");
    if (!gumb) return;
    const tamna = document.documentElement.getAttribute("data-bs-theme") === "dark";
    gumb.setAttribute("aria-pressed", String(tamna));
    gumb.querySelector(".gumb-tema-tekst").textContent = tamna ? "Svijetla tema" : "Tamna tema";
}

/**
 * Za svaki element s data-pocetak ispisuje relativno vrijeme na hrvatskom:
 * "danas", "sutra", "za 4 dana", "prije 5 dana"...
 * Intl.RelativeTimeFormat sam zna hrvatsku gramatiku (dan / dana).
 */
function prikaziOdbrojavanja() {
    const format = new Intl.RelativeTimeFormat("hr", { numeric: "auto" });
    const danas = new Date();
    danas.setHours(0, 0, 0, 0);

    document.querySelectorAll("[data-pocetak]").forEach((element) => {
        const pocetak = new Date(element.dataset.pocetak);
        const danPocetka = new Date(pocetak);
        danPocetka.setHours(0, 0, 0, 0);

        const razlikaDana = Math.round((danPocetka - danas) / 86_400_000);
        element.textContent = razlikaDana < 0
            ? "Održano"
            : format.format(razlikaDana, "day");
    });
}

/**
 * Brojač znakova ispod textarea s atributom data-brojac="500".
 */
function ukljuciBrojacZnakova(polje) {
    const najvise = Number(polje.dataset.brojac);
    const brojac = document.createElement("small");
    brojac.className = "brojac";
    brojac.setAttribute("aria-live", "polite");
    polje.insertAdjacentElement("afterend", brojac);

    const osvjezi = () => {
        const duljina = polje.value.length;
        brojac.textContent = `${duljina} / ${najvise} znakova`;
        brojac.classList.toggle("brojac--puno", duljina > najvise);
    };

    polje.addEventListener("input", osvjezi);
    osvjezi();
}

/** Zatvara obavijest nakon spremanja/brisanja (partial _StatusPoruka). */
function zatvoriObavijest(obavijest) {
    obavijest.classList.add("obavijest--skriva");
    setTimeout(() => obavijest.remove(), 300);
}

// ---------------------------------------------------------------------------
// Povezivanje funkcija s elementima na stranici
// ---------------------------------------------------------------------------
document.querySelector("[data-promijeni-temu]")?.addEventListener("click", promijeniTemu);
osvjeziGumbTeme();

prikaziOdbrojavanja();

document.querySelectorAll("textarea[data-brojac]").forEach(ukljuciBrojacZnakova);

document.querySelectorAll("[data-obavijest]").forEach((obavijest) => {
    obavijest.querySelector("[data-zatvori-obavijest]")
        ?.addEventListener("click", () => zatvoriObavijest(obavijest));
    setTimeout(() => zatvoriObavijest(obavijest), 6000);
});
