public interface IAgendamentoRepository
{
    Task AdicionarAsync(Agendamento agendamento);
    Task<Agendamento?> ObterPorIdAsync(Guid id);
    Task AtualizarAsync(Agendamento agendamento);
    Task<bool> HorarioOcupadoAsync(Guid professorId, DateTime dataHora);
    Task<IEnumerable<DateTime>> ObterHorariosOcupadosAsync(Guid professorId, DateTime data);
    Task<Dictionary<AgendamentoStatus, int>> ContarStatusAgendamentosAsync();
    Task<(IEnumerable<Agendamento> Items, long TotalCount)> ObterAgendamentosPaginadosAsync(int page, int pageSize, string? nome, AgendamentoStatus? status, DateTime? data);
    Task<Agendamento?> ObterDetalhesPorIdAsync(Guid id);
}