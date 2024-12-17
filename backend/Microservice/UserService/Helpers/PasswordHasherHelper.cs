using Microsoft.AspNetCore.Identity;

namespace UserService.Helpers
{
    public static class PasswordHasherHelper
    {
        private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        // Hashes a password
        public static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        // Verifies if a plain text password matches the hashed password
        public static bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
