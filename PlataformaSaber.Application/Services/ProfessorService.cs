using System.Linq.Expressions;

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

     public async Task<object> ObterProfessoresPaginadoAsync(int page, string? nome = null)
    {
        var pageSize = 10;
        
        Expression<Func<Professor, bool>> predicate = p => true;

        if (!string.IsNullOrWhiteSpace(nome))
        {
            predicate = p => p.Nome.ToLower().Contains(nome.ToLower());
        }

        var totalItens = await _ProfessorRepository.ContarFiltradoAsync(predicate);
        var totalPaginas = (int)Math.Ceiling((double)totalItens / pageSize);

        if (page > totalPaginas && totalPaginas > 0) page = totalPaginas;

        var professores = await _ProfessorRepository.ObterFiltradoEPaginadoAsync(predicate, page, pageSize);
        var itemsDto = professores.Select(MapToDto);

        return new
        {
            // A chave da resposta será "professores"
            professores = new 
            {
                itemsReceived = itemsDto.Count(),
                curPage = page,
                itemsTotal = totalItens,
                pageTotal = totalPaginas,
                items = itemsDto
            }
        };
    }
}

