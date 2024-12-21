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

            // Pravilo za obavezan datum isporuke kada je status Isporučeno
            RuleFor(p => p.DatumIsporuke)
                .NotEmpty()
                .When(p => p.Status == StatusPosiljke.Isporuceno)
                .WithMessage("Datum isporuke je obavezan za pošiljke koje su isporučene.");

            // Pravilo da datum isporuke za Isporučeno nije u budućnosti
            RuleFor(p => p.DatumIsporuke)
                .LessThanOrEqualTo(DateTime.Now)
                .When(p => p.Status == StatusPosiljke.Isporuceno && p.DatumIsporuke.HasValue)
                .WithMessage("Datum isporuke za isporučene pošiljke ne može biti u budućnosti.");

            // Pravilo da datum isporuke za Na putu ili U skladištu mora biti u budućnosti
            RuleFor(p => p.DatumIsporuke)
                .GreaterThan(DateTime.Now)
                .When(p => p.DatumIsporuke.HasValue && (p.Status == StatusPosiljke.NaPutu || p.Status == StatusPosiljke.USkladistu))
                .WithMessage("Datum isporuke za pošiljke koje su na putu ili u skladištu mora biti u budućnosti.");



        }
    }
}
