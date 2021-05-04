using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyForum.Core.Helpers;

namespace MyForum.Core.Validators
{
    public class CharactersNotAllowedValidator : ValidationAttribute
    {
        private readonly char[] charactersNotAllowed;

        public CharactersNotAllowedValidator(params char[] charactersNotAllowed)
        {
            this.charactersNotAllowed = charactersNotAllowed;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueToString = (string)value;

            return (valueToString != null && valueToString.Any(c => charactersNotAllowed.Contains(c)))
        ? new ValidationResult(ValidationMessages.CharactersValidatorMessage(charactersNotAllowed))
        : ValidationResult.Success;
        }
    }
}