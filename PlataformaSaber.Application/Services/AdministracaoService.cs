using System.Linq.Expressions;

public class AdministracaoService : PessoaService<AdministracaoDto, Administracao>, IAdministracaoService
{
    private readonly IAdministracaoRepository _AdministracaoRepository;
    private readonly IUsuarioService _usuarioService;

    public AdministracaoService(IAdministracaoRepository AdministracaoRepository, IUsuarioService usuarioService) : base(AdministracaoRepository)
    {
        _AdministracaoRepository = AdministracaoRepository;
        _usuarioService = usuarioService;
    }

    public override Administracao MapToEntity(AdministracaoDto dto)
    {
        return new Administracao(dto.Nome, dto.Email, "123456789", dto.Cpf, dto.DataNascimento);
    }

    public override AdministracaoDto MapToDto(Administracao Administracao)
    {
        return new AdministracaoDto
        {
            Id = Administracao.Id,
            Nome = Administracao.Nome,
            Email = Administracao.Email,
            Cpf = Administracao.Cpf,
            DataNascimento = Administracao.DataNascimento,
            Status = Administracao.Status.ToString()
        };
    }

    public override async Task AdicionarAsync(AdministracaoDto dto)
    {
        var jaExisteUsuario = await _usuarioService.BuscarPessoasAsync(dto.Email, dto.Cpf);

        if (jaExisteUsuario.Any())
            throw new InvalidOperationException($"Já existe um usuário cadastrado com o email {dto.Email} ou CPF {dto.Cpf}");

        await base.AdicionarAsync(dto);
    }

}

