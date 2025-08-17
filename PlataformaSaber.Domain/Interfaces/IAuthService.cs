public interface IAuthService
{
    Task<string?> AutenticarAsync(string email, string senha);
}