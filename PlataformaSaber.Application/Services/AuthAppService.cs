public class AuthAppService
{
    private readonly IUserAuthentication _authService;
    private readonly IPessoaRepository<Pessoa> _pessoaRepository;

    public AuthAppService(IUserAuthentication authService, IPessoaRepository<Pessoa> pessoaRepository)
    {
        _authService = authService;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var token = await _authService.AutenticarAsync(dto.Email, dto.password);

        if (string.IsNullOrEmpty(token))
            return null;

        var usuarios = await _pessoaRepository.BuscarAsync(p => p.Email == dto.Email);
        var usuario = usuarios.FirstOrDefault();

        return new LoginResponseDto
        {
            AuthToken = token,
            User = new UserDto
            {
                Name = usuario.Nome,
                Email = usuario.Email
            }
        };
    }

}