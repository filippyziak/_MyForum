using System.ComponentModel.DataAnnotations;
using MyForum.Core.Helpers;

namespace MyForum.Core.Validators
{
    public class StringLengthValidator : ValidationAttribute
    {
        public int MaxLength { get; }
        private int minLength;
        public int MinLength
        {
            get => minLength >= 0 ? minLength : 0;
            private set => minLength = value >= 0 ? value : 0;
        }

        public StringLengthValidator(int maxLength, int minLength = 0)
        {
            MaxLength = maxLength;
            MinLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueToString = (string)value;
            return (valueToString?.Length > MaxLength || valueToString?.Length < MinLength)
            ? new ValidationResult(ValidationMessages.StringLengthValidatorMessage(MinLength, MaxLength))
            : ValidationResult.Success;
        }
    }
}