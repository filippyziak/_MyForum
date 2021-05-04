namespace MyForum.Core.Helpers
{
    public class ValidationMessages
    {
        public const string RequiredValidation = "This field is required";
        public const string WhitespaceValidation = "Whitespaces are not allowed";

        public static string StringLengthValidatorMessage(int minLength, int maxLength)
            => minLength != 0
            ? $"Field must have between {minLength} and {maxLength} characters"
            : $"Field cannot have more than {maxLength} characters";

        public static string CharactersValidatorMessage(char[] charactersNotAllowed)
            => charactersNotAllowed.Length == 1
                ? $"Character {string.Join(", ", charactersNotAllowed)} is not allowed"
                : $"Characters: {string.Join(", ", charactersNotAllowed)} are not allowed";
    }
}