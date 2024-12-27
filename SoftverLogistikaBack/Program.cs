using FluentValidation;
using Serilog;
using SoftverLogistikaBack.Endpoints;
using SoftverLogistikaBack.Services;
using SoftverLogistikaBack.Validators;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/logovi-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:7036", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddSingleton<PosiljkaService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddValidatorsFromAssemblyContaining<PosiljkaValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/api/status", () => Results.Ok("Server je pokrenut"));

app.UseCors("AllowFrontend");

app.Use(async (context, next) =>
{
    Log.Information("Primljen {Method} zahtev na {Path}", context.Request.Method, context.Request.Path);
    try
    {
        await next();
        Log.Information("Odgovor za {Path} vraæen sa status kodom {StatusCode}",
            context.Request.Path, context.Response.StatusCode);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Došlo je do greške pri obradi zahteva za {Path}", context.Request.Path);
        throw;
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPosiljkaEndpoints();
app.MapAuthEndpoints();

app.Run();
