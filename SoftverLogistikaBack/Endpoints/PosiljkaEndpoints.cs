using FluentValidation;
using DeljeniPodaci;
using Microsoft.AspNetCore.Builder;
using SoftverLogistikaBack.Services;

namespace SoftverLogistikaBack.Endpoints
{
    public static class PosiljkaEndpoints
    {
        public static void MapPosiljkaEndpoints(this WebApplication app)
        {
            app.MapGet("/api/posiljke", (PosiljkaService service) =>
                 Results.Ok(service.GetAll()));


            app.MapGet("/api/posiljke/{id}", (Guid id, PosiljkaService service) =>
            {
                var posiljka = service.GetById(id);
                return posiljka != null ? Results.Ok(posiljka) : Results.NotFound();
            });

            app.MapPost("/api/posiljke", (Posiljka novaPosiljka, IValidator<Posiljka> validator, PosiljkaService service) =>
            {
                var validationResult = validator.Validate(novaPosiljka);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);

                var createdPosiljka = service.Create(novaPosiljka);
                return Results.Created($"/api/posiljke/{createdPosiljka.Id}", createdPosiljka);
            });

            app.MapPut("/api/posiljke/{id}", (Guid id, Posiljka izmenjenaPosiljka, IValidator<Posiljka> validator, PosiljkaService service) =>
            {
                var validationResult = validator.Validate(izmenjenaPosiljka);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);

                var updated = service.Update(id, izmenjenaPosiljka);
                return updated ? Results.Ok() : Results.NotFound();
            });

            app.MapDelete("/api/posiljke/{id}", (Guid id, PosiljkaService service) =>
            {
                var deleted = service.Delete(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });

        }


    }
}
