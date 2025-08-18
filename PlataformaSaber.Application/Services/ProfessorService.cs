public class ProfessorService : PessoaService<ProfessorDto, Professor>, IProfessorService
{
    private readonly IProfessorRepository _ProfessorRepository;

    private readonly IUsuarioService _usuarioService;

    public ProfessorService(IProfessorRepository ProfessorRepository, IUsuarioService usuarioService) : base(ProfessorRepository)
    {
        _ProfessorRepository = ProfessorRepository;
        _usuarioService = usuarioService;
    }

    public override Professor MapToEntity(ProfessorDto dto)
    {
        return new Professor(dto.Nome, dto.Email, "123456789", dto.Cpf, dto.DataNascimento);
    }

    public override ProfessorDto MapToDto(Professor Professor)
    {
        return new ProfessorDto
        {
            Id = Professor.Id,
            Nome = Professor.Nome,
            Email = Professor.Email,
            Cpf = Professor.Cpf,
            DataNascimento = Professor.DataNascimento,
            Status = Professor.Status.ToString()
        };
    }

    public override async Task AdicionarAsync(ProfessorDto dto)
    {
        var jaExisteUsuario = await _usuarioService.BuscarPessoasAsync(dto.Email, dto.Cpf);

        if (jaExisteUsuario.Any())
            throw new InvalidOperationException($"Já existe um usuário cadastrado com o email {dto.Email} ou CPF {dto.Cpf}");

        await base.AdicionarAsync(dto);
    }
}

