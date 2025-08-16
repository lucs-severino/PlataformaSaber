public class AlunoService : Service<AlunoDto, Aluno>, IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository) : base(alunoRepository)
    {
        _alunoRepository = alunoRepository;
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
}

