using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SoftverLogistikaFront;
using SoftverLogistikaFront.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7261/api/") // Backend API URL
};

// Registracija HttpClient sa odgovarajuæim BaseAddress za backend API
builder.Services.AddScoped(sp => httpClient);

await CekajBackEnd(httpClient);



// Registracija PosiljkaService servisa za komunikaciju sa API
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PosiljkaService>();


await builder.Build().RunAsync();

// Obzirom da se ponekad preko visual studia frontend projekat otvori pre backenda, dolazi do problema da se ne ucitaju podaci. Na ovaj nacin front pokusava da svake sekunde da povuce podatke sa backa sve dok ne uspe u tome. Cisto u svrhe poboljsanja korisnickog iskustva
static async Task CekajBackEnd(HttpClient httpClient)
{
    Console.WriteLine("Provera dostupnosti backend-a...");
    while (true)
    {
        try
        {
           
            var response = await httpClient.GetAsync("status");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Backend je dostupan.");
                break;
            }
        }
        catch
        {
            Console.WriteLine("Backend nije dostupan. Pokušavam ponovo za 1 sekundu...");
        }

        
        await Task.Delay(1000);
    }
}