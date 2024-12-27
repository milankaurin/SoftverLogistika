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

            app.MapPost("/validate-token", (AuthService authService, Guid token) =>
            {
                return authService.ValidateToken(token)
                    ? Results.Ok("Token je validan.")
                    : Results.BadRequest("Token nije validan.");
            });

        }

    }
}
