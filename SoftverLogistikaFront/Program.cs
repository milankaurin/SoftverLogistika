using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SoftverLogistikaFront;
using SoftverLogistikaFront.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registracija HttpClient sa odgovarajuæim BaseAddress za backend API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7261/api/") // Backend API URL
});

// Registracija PosiljkaService servisa za komunikaciju sa API
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PosiljkaService>();


await builder.Build().RunAsync();
