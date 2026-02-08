using BlazorAdminTemplate.Application.DTOs.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAdminTemplate.Application.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequestDTO>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) &&
                    password.Any(char.IsDigit)
                ).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one digit.")
                .Must((model, newPassword) =>
                    string.IsNullOrEmpty(model.CurrentPassword) ||
                    newPassword != model.CurrentPassword
                ).WithMessage("New password must be different from current password.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Please confirm your new password.")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
        }
    }
}