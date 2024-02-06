using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MinimalApi.Domain.Models;

namespace MinimalApi.Domain.Validations
{
    public class User_EnsureCorrectEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as User;
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                string emailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(user.Email, emailRegexPattern)) {
                    return new ValidationResult("Invalid e-mail address.");
                }
            }
            return ValidationResult.Success;
        }
    }
}