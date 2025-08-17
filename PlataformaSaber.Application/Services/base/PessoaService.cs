
using System.Linq.Expressions;

public abstract class PessoaService<TDto, TEntity> : IPessoaService<TDto, TEntity>
    where TEntity : class
{
    protected readonly IPessoaRepository<TEntity> _repository;

    protected PessoaService(IPessoaRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public abstract TEntity MapToEntity(TDto dto);
    public abstract TDto MapToDto(TEntity entity);

    public virtual async Task<IEnumerable<TDto>> ObterTodosAsync()
    {
        var list = await _repository.ObterTodosAsync();
        return list.Select(MapToDto);
    }

    public virtual async Task<TDto?> ObterPorIdAsync(Guid id)
    {
        var entity = await _repository.ObterPorIdAsync(id);
        return entity == null ? default : MapToDto(entity);
    }

    public virtual async Task AdicionarAsync(TDto dto)
    {
        var entity = MapToEntity(dto);

        await _repository.AdicionarAsync(entity);
    }

    public virtual async Task AtualizarAsync(TDto dto)
    {
        var entity = MapToEntity(dto);
        await _repository.AtualizarAsync(entity);
    }

    public virtual async Task RemoverAsync(Guid id)
    {
        var entity = await _repository.ObterPorIdAsync(id);
        if (entity != null)
            await _repository.RemoverAsync(entity);
    }

    public virtual async Task<IEnumerable<TDto>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _repository.BuscarAsync(predicate);
        return entities.Select(MapToDto);
    }

}
