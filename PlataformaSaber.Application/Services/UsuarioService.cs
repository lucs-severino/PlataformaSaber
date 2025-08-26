public class UsuarioService : IUsuarioService
{
    private readonly IPessoaRepository<Pessoa> _pessoaRepository;
    private readonly IProfessorService _professorService;
    private readonly IAdministracaoService _administracaoService;
    private readonly IAlunoService _alunoService;

    public UsuarioService(
        IPessoaRepository<Pessoa> pessoaRepository,
        IProfessorService professorService,
        IAlunoService alunoService,
        IAdministracaoService administracaoService)
    {
        _pessoaRepository = pessoaRepository;
        _professorService = professorService;
        _administracaoService = administracaoService;
        _alunoService = alunoService;
    }

    public async Task<IEnumerable<UsuarioDto>> ObterTodasPessoasAsync()
    {
        var pessoas = await _pessoaRepository.ObterTodosAsync();
        return pessoas.Select(MapToDto);
    }


    public async Task<IEnumerable<UsuarioDto>> BuscarPessoasAsync(string? email = null, string? cpf = null)
    {
        if (!string.IsNullOrWhiteSpace(cpf))
        {
            var pessoaPorCpf = await _pessoaRepository.BuscarAsync(p => p.Cpf == cpf);
            var pessoa = pessoaPorCpf.FirstOrDefault();

            if (pessoa != null)
                return new List<UsuarioDto> { MapToDto(pessoa) };
        }
        var pessoas = await _pessoaRepository.BuscarAsync(p => string.IsNullOrEmpty(email) || p.Email == email);

        return pessoas.Select(MapToDto).ToList();
    }

    private UsuarioDto MapToDto(Pessoa pessoa)
    {
        return new UsuarioDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Email = pessoa.Email,
            Cpf = pessoa.Cpf,
            DataNascimento = pessoa.DataNascimento,
            Status = pessoa.Status?.ToString() ?? "Ativo",
            DataCadastro = pessoa.DataCadastro,
            TipoPessoa = pessoa switch
            {
                Aluno => "Aluno",
                Professor => "Professor",
                Administracao => "Administracao",
                _ => "Desconhecido"
            }
        };
    }

    public async Task<object> ObterTodasPessoasPaginadasAsync(int page)
    {

        var totalItens = await _pessoaRepository.ContarTodosAsync();

        var pageSize = 10;
        var totalPaginas = (int)Math.Ceiling((double)totalItens / pageSize);

        var pessoas = await _pessoaRepository.ObterPaginadoAsync(page, pageSize);


        var itemsDto = pessoas.Select(MapToDto);

        return new
        {
            usuarios = new
            {
                itemsReceived = itemsDto.Count(),
                curPage = page,
                itemsTotal = totalItens,
                pageTotal = totalPaginas,
                items = itemsDto
            }
        };
    }

    public async Task<UsuarioDto?> BuscarUsuarioPorId(Guid id)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var pessoasEncontradas = await _pessoaRepository.BuscarAsync(p => p.Id == id);
        var pessoa = pessoasEncontradas.FirstOrDefault();

        if (pessoa != null)
        {
            return MapToDto(pessoa);
        }
        return null;
    }

    public async Task AlterarUsuarioAsync(UsuarioDto dto , Guid id)
    {
        switch (dto.TipoPessoa)
        {
            case "Professor":
                await _professorService.AtualizarAsync(new ProfessorDto
                {
                    Id = id,
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Cpf = dto.Cpf,
                    DataNascimento = dto.DataNascimento,
                    Status = dto.Status
                });
                break;

            case "Administracao":
                await _administracaoService.AtualizarAsync(new AdministracaoDto
                {
                    Id = id,
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Cpf = dto.Cpf,
                    DataNascimento = dto.DataNascimento,
                    Status = dto.Status
                });
                break;

            case "Aluno":
                await _alunoService.AtualizarAsync(new AlunoDto
                {
                    Id = id,
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Cpf = dto.Cpf,
                    DataNascimento = dto.DataNascimento,
                    Status = dto.Status
                });
                break;

            default:
                throw new ArgumentException("Tipo de pessoa inválido.");
        }
    }

}