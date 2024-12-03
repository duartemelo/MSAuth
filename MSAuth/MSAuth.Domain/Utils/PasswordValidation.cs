namespace MSAuth.Domain.Utils
{
    public class PasswordValidation
    {
        public static bool ContainsUpperCaseLetter(string password)
        {
            return password.Any(char.IsUpper);
        }

        public static bool ContainsDigit(string password)
        {
            return password.Any(char.IsDigit);
        }
    }
}
