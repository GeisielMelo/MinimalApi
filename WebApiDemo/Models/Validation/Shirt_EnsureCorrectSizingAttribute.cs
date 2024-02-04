using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models.Validation
{
    public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var shirt = validationContext.ObjectInstance as Shirt;

            if (shirt != null && !string.IsNullOrWhiteSpace(shirt.Gender))
            {
                if (shirt.Gender.Equals("men", StringComparison.OrdinalIgnoreCase) && (shirt.Size < 8 || shirt.Size > 20))
                {
                    return new ValidationResult("For men's shirts, the sizes has to be 8 ~ 15.");
                }
                else if (shirt.Gender.Equals("women", StringComparison.OrdinalIgnoreCase) && (shirt.Size < 6 || shirt.Size > 15))
                {
                    return new ValidationResult("For men's shirts, the sizes has to be 6 ~ 15.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
