using System.Linq.Expressions;

public class AlunoService : PessoaService<AlunoDto, Aluno>, IAlunoService
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


    public async Task<object> ObterAlunosPaginadoAsync(int page, string? nome = null)
    {
        var pageSize = 10;

        Expression<Func<Aluno, bool>> predicate = p => true;

        if (!string.IsNullOrWhiteSpace(nome))
        {
            predicate = p => p.Nome.ToLower().Contains(nome.ToLower());
        }

        var totalItens = await _alunoRepository.ContarFiltradoAsync(predicate);
        var totalPaginas = (int)Math.Ceiling((double)totalItens / pageSize);

        if (page > totalPaginas && totalPaginas > 0)
        {
            page = totalPaginas;
        }

        var alunos = await _alunoRepository.ObterFiltradoEPaginadoAsync(predicate, page, pageSize);
        var itemsDto = alunos.Select(MapToDto);

        return new
        {
            alunos = new
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

