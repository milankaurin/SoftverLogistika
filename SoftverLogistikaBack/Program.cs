using FluentValidation;
using Serilog;
using SoftverLogistikaBack.Endpoints;
using SoftverLogistikaBack.Services;
using SoftverLogistikaBack.Validators;

var builder = WebApplication.CreateBuilder(args);


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
            policy.WithOrigins("https://localhost:7036") // URL frontenda
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
        throw; // Ponovno bacanje izuzetka
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


