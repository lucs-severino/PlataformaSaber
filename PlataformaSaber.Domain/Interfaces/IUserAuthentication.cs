public interface IUserAuthentication
{
    Task<string?> AutenticarAsync(string email, string senha);
}