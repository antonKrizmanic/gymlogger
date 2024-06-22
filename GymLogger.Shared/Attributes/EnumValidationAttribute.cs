using System.ComponentModel.DataAnnotations;

namespace GymLogger.Shared.Attributes;
public class EnumValidationAttribute : ValidationAttribute
{
    private readonly Type _enumType;
    private readonly bool _allowZero;

    public EnumValidationAttribute(Type enumType, bool allowZero = false)
    {
        _enumType = enumType;
        _allowZero = allowZero;
        if (!enumType.IsEnum)
        {
            throw new ArgumentException("Type must be an enum", nameof(enumType));
        }
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (_allowZero && Convert.ToInt32(value) == 0)
        {
            return ValidationResult.Success;
        }
        else if (!_allowZero && Convert.ToInt32(value) == 0)
        {
            return new ValidationResult("Value cannot be zero");
        }

        bool isValid = Enum.IsDefined(_enumType, value);
        if (!isValid)
        {
            return new ValidationResult($"Invalid value for enum {_enumType.Name}");
        }
        return ValidationResult.Success;
    }
}
