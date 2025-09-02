
public interface IProfessorService : IPessoaService<ProfessorDto, Professor>
{
    Task<object> ObterProfessoresPaginadoAsync(int page, string? nome = null);
}

