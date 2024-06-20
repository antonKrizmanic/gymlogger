using System.ComponentModel.DataAnnotations;

namespace GymLogger.Shared.Attributes;
public class GuidNotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is Guid guidValue && guidValue != Guid.Empty)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("The GUID cannot be empty.");
        }
    }
}