
public abstract class Service<TDto, TEntity> : IService<TDto>
    where TEntity : class
{
    protected readonly IRepository<TEntity> _repository;

    protected Service(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public abstract TEntity MapToEntity(TDto dto);
    public abstract TDto MapToDto(TEntity entity);

    public async Task<IEnumerable<TDto>> ObterTodosAsync()
    {
        var list = await _repository.ObterTodosAsync();
        return list.Select(MapToDto);
    }

    public async Task<TDto?> ObterPorIdAsync(Guid id)
    {
        var entity = await _repository.ObterPorIdAsync(id);
        return entity == null ? default : MapToDto(entity);
    }

    public async Task AdicionarAsync(TDto dto)
    {
        var entity = MapToEntity(dto);
        await _repository.AdicionarAsync(entity);
    }

    public async Task AtualizarAsync(TDto dto)
    {
        var entity = MapToEntity(dto);
        await _repository.AtualizarAsync(entity);
    }

    public async Task RemoverAsync(Guid id)
    {
        var entity = await _repository.ObterPorIdAsync(id);
        if (entity != null)
            await _repository.RemoverAsync(entity);
    }
}

