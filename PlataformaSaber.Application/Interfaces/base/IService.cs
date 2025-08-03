public interface IService<TDto>
{
    Task<IEnumerable<TDto>> ObterTodosAsync();
    Task<TDto?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(TDto dto);
    Task AtualizarAsync(TDto dto);
    Task RemoverAsync(Guid id);
}