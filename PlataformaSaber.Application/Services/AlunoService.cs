public class AlunoService : PessoaService<AlunoDto, Aluno>, IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IUsuarioService _usuarioService;

    public AlunoService(IAlunoRepository alunoRepository, IUsuarioService usuarioService) : base(alunoRepository)
    {
        _alunoRepository = alunoRepository;
        _usuarioService = usuarioService;
    }


    public override Aluno MapToEntity(AlunoDto dto)
    {
        return new Aluno(dto.Nome, dto.Email, "123456789", dto.Cpf, dto.DataNascimento);
    }

    public override AlunoDto MapToDto(Aluno aluno)
    {
        return new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Email = aluno.Email,
            Cpf = aluno.Cpf,
            DataNascimento = aluno.DataNascimento,
            Status = aluno.Status.ToString()
        };
    }

    public override async Task AdicionarAsync(AlunoDto dto)
    {
        var jaExisteUsuario = await _usuarioService.BuscarPessoasAsync(dto.Email, dto.Cpf);

        if (jaExisteUsuario.Any())
            throw new InvalidOperationException($"Já existe um usuário cadastrado com o email {dto.Email} ou CPF {dto.Cpf}");

        await base.AdicionarAsync(dto);
    }
}

