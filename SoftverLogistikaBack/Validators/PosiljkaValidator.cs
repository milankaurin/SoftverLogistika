using SoftverLogistikaBack.Models;
using FluentValidation;
using DeljeniPodaci;

namespace SoftverLogistikaBack.Validators
{
    public class PosiljkaValidator : AbstractValidator<Posiljka>
    {
        public PosiljkaValidator()
        {
            RuleFor(p => p.Naziv)
                .NotEmpty().WithMessage("Naziv je obavezan.")
                .MinimumLength(3).WithMessage("Naziv mora imati najmanje 3 karaktera.");

            RuleFor(p => p.DatumIsporuke)
                .GreaterThan(DateTime.Now)
                .When(p => p.DatumIsporuke.HasValue)
                .WithMessage("Datum isporuke mora biti u budućnosti.");

           

        }
    }
}
