using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class PessoaRepository<T> : IPessoaRepository<T> where T : Pessoa

{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public PessoaRepository(ApplicationDbContext context)
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
            // 
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

    // Busca por filtros
    public async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.FromResult(_dbSet.AsQueryable().Where(predicate));
    }

    // Obter entidades paginadas
    public async Task<IEnumerable<T>> ObterPaginadoAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return await _dbSet.Skip(skip).Take(pageSize).ToListAsync();
    }

    public async Task<int> ContarTodosAsync()
    {
        return await _dbSet.CountAsync();
    }

}
