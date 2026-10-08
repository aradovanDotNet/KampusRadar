using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using KampusRadar.Data;
using KampusRadar.Infrastruktura;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────────────────────────────────────
// 1) SERVISI (Dependency Injection)
// ─────────────────────────────────────────────────────────────────────────────

// Razor Pages + hrvatske poruke za greške pri povezivanju podataka (model binding)
builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.ModelBindingMessageProvider.PostaviHrvatskePoruke());

// EF Core + SQLite. Connection string se čita iz appsettings.json
var connectionString = builder.Configuration.GetConnectionString("KampusRadar")
    ?? throw new InvalidOperationException("Nedostaje connection string 'KampusRadar'.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString)
           // EF Core 9+: početni podaci (seed) – izvršava se pri Migrate() / EnsureCreated()
           .UseSeeding((context, _) => PocetniPodaci.Napuni(context))
           .UseAsyncSeeding((context, _, ct) => PocetniPodaci.NapuniAsync(context, ct)));

// Lokalizacija: hrvatski format datuma i brojeva (decimalni zarez!)
var hrvatski = new CultureInfo("hr-HR");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(hrvatski);
    options.SupportedCultures = [hrvatski];
    options.SupportedUICultures = [hrvatski];
});

// HTML izvor s pravim hrvatskim slovima (č, ć, đ, š, ž) umjesto entiteta &#x10D;
builder.Services.Configure<WebEncoderOptions>(options =>
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));

var app = builder.Build();

// ─────────────────────────────────────────────────────────────────────────────
// 2) BAZA: kreiraj/ažuriraj bazu prema migracijama (+ seed) pri pokretanju
// ─────────────────────────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ─────────────────────────────────────────────────────────────────────────────
// 3) MIDDLEWARE PIPELINE – redoslijed je bitan!
// ─────────────────────────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/StatusKod", "?kod={0}"); // lokalizirane 404 stranice
app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();          // .NET 9+: kompresija i fingerprinting statičkih datoteka
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
