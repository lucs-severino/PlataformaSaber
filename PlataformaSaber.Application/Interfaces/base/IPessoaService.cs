using System.Linq.Expressions;

public interface IPessoaService<TDto, TEntity>
{
    Task<IEnumerable<TDto>> ObterTodosAsync();
    Task<TDto?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(TDto dto);
    Task AtualizarAsync(TDto dto);
    Task RemoverAsync(Guid id);
    Task<IEnumerable<TDto>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
}