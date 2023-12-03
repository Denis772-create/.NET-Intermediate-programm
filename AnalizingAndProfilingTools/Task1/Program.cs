using System.Security.Cryptography;

namespace Task1;

internal class Program
{
    static void Main()
    {
        var password = "MySecurePassword";
        byte[] salt = GenerateRandomSalt();

        Console.WriteLine(GeneratePasswordHashUsingSalt_Old(password, salt));

        Console.WriteLine(GeneratePasswordHashUsingSalt_New(password, salt));
    }


    public static string GeneratePasswordHashUsingSalt_Old(string passwordText, byte[] salt)
    {
        var iterate = 10000;
        var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var passwordHash = Convert.ToBase64String(hashBytes);

        return passwordHash;
    }

    /// <summary>
    /// New optimized version
    /// </summary>
    public static string GeneratePasswordHashUsingSalt_New(string passwordText, byte[] salt)
    {
        const int iterations = 10000;
        using var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterations);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
        Buffer.BlockCopy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    static byte[] GenerateRandomSalt()
    {
        var salt = new byte[16];
        using RandomNumberGenerator rng = new RNGCryptoServiceProvider();
        rng.GetBytes(salt);
        return salt;
    }
}