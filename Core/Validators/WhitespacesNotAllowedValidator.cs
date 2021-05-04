using System.ComponentModel.DataAnnotations;
using MyForum.Core.Extensions;
using MyForum.Core.Helpers;

namespace MyForum.Core.Validators
{
    public class WhitespacesNotAllowedValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueToString = (string)value;
            return valueToString.HasWhitespaces()
            ? new ValidationResult(ValidationMessages.WhitespaceValidation)
            : ValidationResult.Success;
        }
    }
}