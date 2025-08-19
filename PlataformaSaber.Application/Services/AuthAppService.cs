public class AuthAppService
{
    private readonly IUserAuthentication _authService;

    public AuthAppService(IUserAuthentication authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var token = await _authService.AutenticarAsync(dto.Email, dto.Senha);

        if (string.IsNullOrEmpty(token))
            return null;

        return new LoginResponseDto
        {
            Token = token,
            ExpiraEm = DateTime.UtcNow.AddHours(1)
        };
    }
}