using System.Security.Cryptography;
using System.Text;

public class Password
{
    public string? Hash { get; private set; }

    private Password() { }

    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password Create(string plainTextPassword)
    {
        if (string.IsNullOrWhiteSpace(plainTextPassword))
            throw new ArgumentException("Senha não pode ser vazia.");

        if (plainTextPassword.Length < 8)
            throw new ArgumentException("Senha deve ter no mínimo 8 caracteres.");

        var hash = ComputeHash(plainTextPassword);
        return new Password(hash);
    }

    private static string ComputeHash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string plainTextPassword)
    {
        var hash = ComputeHash(plainTextPassword);
        return hash == Hash;
    }

    public static Password CreateFromHash(string hash)
    {

        return new Password(hash);
    }
}
