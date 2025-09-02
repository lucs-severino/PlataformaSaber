

public interface IAlunoService : IPessoaService<AlunoDto, Aluno>
{
    Task<object> ObterAlunosPaginadoAsync(int page, string? nome = null);
}

