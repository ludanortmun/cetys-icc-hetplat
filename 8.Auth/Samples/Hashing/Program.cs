using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Hashing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a password: ");
            string? password = Console.ReadLine();


            byte[] salt;
            Console.Write("Use salt? (y/n): ");
            string? useSaltInput = Console.ReadLine();

            if (useSaltInput?.ToLower() != "y")
            {
                salt = [];
            }
            else
            {
                // Generate a 128-bit salt using a sequence of
                // cryptographically strong random bytes.
                salt = RandomNumberGenerator.GetBytes(128 / 8);
                Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            Console.WriteLine($"Hashed password: {hashed}");
        }
    }
}
