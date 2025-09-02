public interface IAgendamentoService
{
    Task CriarAgendamentoAsync(AgendamentoDto dto, Guid usuarioLogadoId);
    Task CancelarAgendamentoAsync(Guid agendamentoId, Guid usuarioLogadoId, string motivo);
    Task ConfirmarAgendamentoAsync(Guid agendamentoId, Guid usuarioLogadoId);
    Task<HorarioDisponivelDto> ObterHorariosDisponiveisAsync(Guid professorId, DateTime data);
}