public interface IAgendamentoRepository
{
    Task AdicionarAsync(Agendamento agendamento);
    Task<Agendamento?> ObterPorIdAsync(Guid id);
    Task AtualizarAsync(Agendamento agendamento); 
    Task<bool> HorarioOcupadoAsync(Guid professorId, DateTime dataHora);
}