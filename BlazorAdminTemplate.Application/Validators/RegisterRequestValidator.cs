using BlazorAdminTemplate.Application.DTOs.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestValidator()
        {
            //Persomal Information - Required
            RuleFor(x => x.MemberFirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.")
                .Matches(@"^[a-zA-ZæøåÆØÅ\s'-]+$").WithMessage("First name containts invalid characters.");

            RuleFor(x => x.MemberLastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.")
                .Matches(@"^[a-zA-ZæøåÆØÅ\s'-]+$").WithMessage("Last name containts invalid characters.");

            RuleFor(x => x.MemberEmail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email.")
                .MaximumLength(100).WithMessage("Email address is too long");

            RuleFor(x => x.MemberPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be atleast 8 characters long")
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) &&
                    password.Any(char.IsDigit)
                    ).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one digit.");


            // Organisation Information - Required
            RuleFor(x => x.OrganisationMainGUID)
                .NotEmpty().WithMessage("Organisation is required.");

            RuleFor(x => x.OrganisationSubGUID)
                .NotEmpty().WithMessage("Please select a center / location");

            RuleFor(x => x.MemberLanguageGUID)
                .NotEmpty().WithMessage("Please select a language");

            RuleFor(x => x.MemberPaymentGroupGUID)
                .NotEmpty().WithMessage("Please select a membership");


            //Optional Information with validation when provided
            RuleFor(x => x.MemberPhonePrivate)
                .Matches(@"^\+?[0-9\s-]{7,15}$").WithMessage("Private phone number must be a valid format (e.g., +1234567890 or 1234567890).")
                .When(x => !string.IsNullOrEmpty(x.MemberPhonePrivate));

            RuleFor(x => x.MemberPhoneWork)
                .Matches(@"^\+?[0-9\s-]{7,15}$").WithMessage("Work phone number must be a valid format (e.g., +1234567890 or 1234567890).")
                .When(x => !string.IsNullOrEmpty(x.MemberPhoneWork));

            RuleFor(x => x.MemberAddress)
                .MaximumLength(100).WithMessage("Address is too long")
                .When(x => !string.IsNullOrEmpty(x.MemberAddress));

            RuleFor(x => x.MemberCity)
                .MaximumLength(100).WithMessage("City name is too long")
                .Matches(@"^[a-zA-ZæøåÆØÅ\s'-]+$").WithMessage("City name contains invalid characters.")
                .When(x => !string.IsNullOrEmpty(x.MemberCity));

            RuleFor(x => x.MemberZipCode)
                .InclusiveBetween(1000, 9999).WithMessage("Zip code must be a 4-digit number.")
                .When(x => x.MemberZipCode > 0);

            RuleFor(x => x.MemberJobTitle)
                .MaximumLength(100).WithMessage("Job title is too long")
                .When(x => !string.IsNullOrEmpty(x.MemberJobTitle));

            //CPR validation for Danish Personal Identification Number

            RuleFor(x => x.MemberCPR)
                .Matches(@"^\d{6}-?\d{4}$").WithMessage("CPR must be in the format DDMMYY-XXXX or DDMMYYXXXX.")
                .Must(BeValidCPR).WithMessage("Please enter a valid CPR number")
                .When(x => !string.IsNullOrEmpty(x.MemberCPR));

            // Birthday validation
            RuleFor(x => x.MemberBirthday)
                .Must(BeValidDate).WithMessage("Please enter a valid date")
                .Must(BeReasonableAge).WithMessage("Please enter a valid birthday")
                .When(x => x.MemberBirthday.HasValue);

            RuleFor(x => x.MemberComment)
                .MaximumLength(500).WithMessage("Comments must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.MemberComment));
        }


        private bool BeValidDate(DateTime? date)
        {
            // If not provided, skip validation (optional field)
            return !date.HasValue || date.Value != default;
        }

        private bool BeValidCPR(string cpr)
        {
            if (string.IsNullOrWhiteSpace(cpr)) return true;

            var cleanCpr = cpr.Replace("-", "").Trim();

            if (cleanCpr.Length != 10 || !cleanCpr.All(char.IsDigit))
                return false;

            var day = int.Parse(cleanCpr.Substring(0, 2));
            var month = int.Parse(cleanCpr.Substring(2, 2));
            var year = int.Parse(cleanCpr.Substring(4, 2));

            if (day < 1 || day > 31 || month < 1 || month > 12)
                return false;

            return true;
        }

        private bool BeReasonableAge(DateTime? date)
        {
            if (!date.HasValue)
                return true;

            var birthDate = date.Value;
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            return age >= 0 && age <= 120; // Reasonable age range
        }
    }
}
