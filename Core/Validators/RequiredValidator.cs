using System.ComponentModel.DataAnnotations;
using MyForum.Core.Helpers;

namespace MyForum.Core.Validators
{
    public class RequiredValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        => value == null
        ? new ValidationResult(ValidationMessages.RequiredValidation)
        : ValidationResult.Success;
    }
}