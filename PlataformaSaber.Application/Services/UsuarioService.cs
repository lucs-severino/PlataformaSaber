public class UsuarioService : IUsuarioService
{
    private readonly IPessoaRepository<Pessoa> _pessoaRepository;

    public UsuarioService(IPessoaRepository<Pessoa> pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
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
}