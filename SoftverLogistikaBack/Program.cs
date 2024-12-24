using FluentValidation;

using Serilog;
using SoftverLogistikaBack.Endpoints;
using SoftverLogistikaBack.Services;
using SoftverLogistikaBack.Validators;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

bool isLoggedIn = false; // Globalno stanje prijave





Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console() // Logovanje u konzolu
    .WriteTo.File("logs/logovi-.txt", rollingInterval: RollingInterval.Day) // Logovanje u dnevni fajl
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:7036", "http://localhost:5173") // URL frontenda
                  .AllowAnyHeader()
                  .AllowAnyMethod();


        });
});

builder.Services.AddSingleton<PosiljkaService>();
builder.Services.AddValidatorsFromAssemblyContaining<PosiljkaValidator>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.MapGet("/api/status", () => Results.Ok("Server je pokrenut"));  //endpoint za slanje odgovora klijentu da je server aktivan



// Endpoint za promenu statusa prijave
app.MapPost("/login", () =>
{
    isLoggedIn = !isLoggedIn; // Menja status prijave
    return Results.Ok(new { IsLoggedIn = isLoggedIn });
});

// Endpoint za proveru statusa prijave
app.MapGet("/login-status", () => Results.Ok(new { IsLoggedIn = isLoggedIn }));

app.Use(async (context, next) =>
{
    // Logovanje ulaznog zahteva
    Log.Information("Primljen {Method} zahtev na {Path}", context.Request.Method, context.Request.Path);

    try
    {
        await next(); // Prosleðivanje zahteva sledeæem middleware-u

        // Logovanje odgovora
        Log.Information("Odgovor za {Path} vraæen sa status kodom {StatusCode}",
            context.Request.Path, context.Response.StatusCode);
    }
    catch (Exception ex)
    {
        // Logovanje grešaka
        Log.Error(ex, "Došlo je do greške pri obradi zahteva za {Path}", context.Request.Path);
        throw; 
    }
});

app.UseCors("AllowFrontend");





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPosiljkaEndpoints();

app.Run();


