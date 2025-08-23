using System.Linq.Expressions;

public interface IPessoaRepository<T> where T : class
{
    Task<T?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<T>> ObterTodosAsync();
    Task AdicionarAsync(T entity);
    Task AtualizarAsync(T entity);
    Task RemoverAsync(T entity);
    Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> ObterPaginadoAsync(int pageNumber, int pageSize);
    Task<int> ContarTodosAsync();

}