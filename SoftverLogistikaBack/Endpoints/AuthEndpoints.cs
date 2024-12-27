using Microsoft.AspNetCore.Builder;
using SoftverLogistikaBack.Services;

namespace SoftverLogistikaBack.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/login", (AuthService authService) =>
            {
                var token = authService.GenerateToken();
                return Results.Ok(new { Token = token });
            });

            app.MapPost("/logout", async (HttpContext context, AuthService authService) =>
            {
                var body = await context.Request.ReadFromJsonAsync<Guid>();
                if (authService.ValidateToken(body))
                {
                    authService.RemoveToken(body);
                    return Results.Ok("Odjavljeni ste.");
                }
                return Results.BadRequest("Neispravan token.");
            });



            app.MapPost("/validate-token", async (AuthService authService, HttpContext context) =>
            {
                var body = await context.Request.ReadFromJsonAsync<ValidateTokenRequest>();

                if (body == null || !Guid.TryParse(body.Token, out var token))
                {
                    Console.WriteLine("Neispravan zahtev: token nije pronađen.");
                    return Results.BadRequest("Neispravan token.");
                }

                Console.WriteLine($"Primljen token za validaciju: {token}");

                if (authService.ValidateToken(token))
                {
                    Console.WriteLine("Token je validan.");
                    return Results.Ok("Token je validan.");
                }

                Console.WriteLine("Token nije validan.");
                return Results.BadRequest("Neispravan token.");
            });
        }
        private class ValidateTokenRequest
        {
            public string Token { get; set; } = string.Empty;
        }

    }

}



