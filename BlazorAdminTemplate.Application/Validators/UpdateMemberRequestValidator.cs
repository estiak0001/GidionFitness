using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAdminTemplate.Application.DTOs.Members;
using FluentValidation;


namespace BlazorAdminTemplate.Application.Validators
{
    public class UpdateMemberRequestValidator : AbstractValidator<StaffUpdateMemberRequestDTO>
    {
       public UpdateMemberRequestValidator() 
       {
            RuleFor(x => x.MemberFirstName)
                   .NotEmpty().WithMessage("Fornavn er påkrævet")
                   .Length(1, 50).WithMessage("Fornavn skal være mellem 1 og 50 tegn");

            RuleFor(x => x.MemberLastName)
                    .NotEmpty().WithMessage("Efternavn er påkrævet")
                    .Length(1, 50).WithMessage("Efternavn skal være mellem 1 og 50 tegn");

            RuleFor(x => x.MemberEmail)
                    .NotEmpty().WithMessage("Email er påkrævet")
                    .EmailAddress().WithMessage("Indtast venligst en gyldig email adresse");

            RuleFor(x => x.MemberPhonePrivate)
                    .Matches(@"^\+?[0-9\s-]{7,15}$").WithMessage("Privat telefon skal være et gyldigt telefonnummer")
                    .When(x => !string.IsNullOrEmpty(x.MemberPhonePrivate));

            RuleFor(x => x.MemberPhoneWork)
                    .Matches(@"^\+?[0-9\s-]{7,15}$").WithMessage("Arbejdstelefon skal være et gyldigt telefonnummer")
                    .When(x => !string.IsNullOrEmpty(x.MemberPhoneWork));

            RuleFor(x => x.MemberCPR)
                    .Matches(@"^\d{6}-?\d{4}$").WithMessage("CPR skal være i formatet DDMMYY-XXXX")
                    .When(x => !string.IsNullOrEmpty(x.MemberCPR));

            RuleFor(x => x.MemberZipCode)
                    .InclusiveBetween(1000, 9999).WithMessage("Postnummer skal være mellem 1000 og 9999")
                    .When(x => x.MemberZipCode > 0);

            RuleFor(x => x.NewPassword)
                    .MinimumLength(8).WithMessage("Ny adgangskode skal være mindst 8 tegn")
                    .When(x => !string.IsNullOrEmpty(x.NewPassword));

            RuleFor(x => x.CurrentPassword)
                    .NotEmpty().WithMessage("Nuværende adgangskode er påkrævet for at ændre adgangskode")
                    .When(x => !string.IsNullOrEmpty(x.NewPassword));
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<StaffUpdateMemberRequestDTO>.CreateWithOptions((StaffUpdateMemberRequestDTO)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
