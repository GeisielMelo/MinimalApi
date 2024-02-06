using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MinimalApi.Domain.Models;

namespace MinimalApi.Domain.Validations
{
    public class User_EnsureCorrectPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = validationContext.ObjectInstance as User;
            if (user != null && !string.IsNullOrWhiteSpace(user.Password))
            {
                string passwordRegexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

                if (user.Password.Length < 8) {
                    return new ValidationResult("The password must be at least 8 characters long.");
                }
                
                if (!Regex.IsMatch(user.Password, passwordRegexPattern)) {
                    return new ValidationResult("The password must contain at least one uppercase letter, one lowercase letter, one special character, and one number.");
                }
            }
            return ValidationResult.Success;
        }
    }
}