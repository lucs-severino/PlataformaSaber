using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // Adicionar entidade
    public async Task AdicionarAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao Adicionar entidade do tipo {typeof(T).Name}: {ex.Message}");
            throw;
        }
    }

    // Obter todos
    public async Task<IEnumerable<T>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    // Obter por Id
    public async Task<T?> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    // Atualizar entidade
    public async Task AtualizarAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao Atualizar entidade do tipo {typeof(T).Name}: {ex.Message}");
            throw;
        }
    }

    // Remover entidade
    public async Task RemoverAsync(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao Remover entidade do tipo {typeof(T).Name}: {ex.Message}");
            throw;
        }
    }

    // Buscar com filtro (opcional)
    public async Task<IEnumerable<T>> BuscarAsync(Func<T, bool> predicate)
    {
        return await Task.FromResult(_dbSet.AsQueryable().Where(predicate));
    }
}
