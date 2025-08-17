public class AuthAppService
{
    private readonly IAuthService _authService;

    public AuthAppService(IAuthService authService)
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