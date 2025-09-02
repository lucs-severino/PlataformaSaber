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
        _context.Agendamentos.Update(agendamento);
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
}