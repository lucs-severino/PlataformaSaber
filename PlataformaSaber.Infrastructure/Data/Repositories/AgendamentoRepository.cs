using Microsoft.EntityFrameworkCore;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly ApplicationDbContext _context;
    public AgendamentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Agendamento agendamento)
    {

        await _context.Agendamentos.AddAsync(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task<Agendamento?> ObterPorIdAsync(Guid id)
    {
        return await _context.Agendamentos
            .Include(a => a.Historico)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AtualizarAsync(Agendamento agendamento)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HorarioOcupadoAsync(Guid professorId, DateTime dataHora)
    {
        return await _context.Agendamentos
            .AnyAsync(a =>
                a.ProfessorId == professorId &&
                a.DataHora == dataHora &&
                a.Status == AgendamentoStatus.Agendado
            );
    }

    public async Task<IEnumerable<DateTime>> ObterHorariosOcupadosAsync(Guid professorId, DateTime data)
    {
        var inicioDia = data.Date;
        var fimDia = inicioDia.AddDays(1);

        return await _context.Agendamentos
            .Where(a => a.ProfessorId == professorId && a.DataHora >= inicioDia && a.DataHora < fimDia)
            .Select(a => a.DataHora)
            .ToListAsync();
    }

    public async Task<Dictionary<AgendamentoStatus, int>> ContarStatusAgendamentosAsync()
    {
        return await _context.Agendamentos
            .GroupBy(a => a.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    public async Task<(IEnumerable<Agendamento> Items, long TotalCount)> ObterAgendamentosPaginadosAsync(
        int page, int pageSize, string? nome, AgendamentoStatus? status, DateTime? data)
    {
        var query = _context.Agendamentos
            .Include(a => a.Aluno)
            .Include(a => a.Professor)
            .AsQueryable();

        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(a => a.Aluno.Nome.Contains(nome) || a.Professor.Nome.Contains(nome));
        }
        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }
        if (data.HasValue)
        {
            query = query.Where(a => a.DataHora.Date == data.Value.Date);
        }

        var totalCount = await query.LongCountAsync();
        var items = await query
            .OrderByDescending(a => a.DataHora)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Agendamento?> ObterDetalhesPorIdAsync(Guid id)
    {
        return await _context.Agendamentos
            .Include(a => a.Aluno)
            .Include(a => a.Professor)
            .Include(a => a.Historico)
                .ThenInclude(h => h.UsuarioResponsavel)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}