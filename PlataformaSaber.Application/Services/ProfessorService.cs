public class ProfessorService : PessoaService<ProfessorDto, Professor>, IProfessorService
{
    private readonly IProfessorRepository _ProfessorRepository;

    public ProfessorService(IProfessorRepository ProfessorRepository) : base(ProfessorRepository)
    {
        _ProfessorRepository = ProfessorRepository;
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
}

