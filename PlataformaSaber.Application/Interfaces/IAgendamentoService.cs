public interface IAgendamentoService
{
    Task CriarAgendamentoAsync(AgendamentoDto dto, Guid usuarioLogadoId);
    Task CancelarAgendamentoAsync(Guid agendamentoId, Guid usuarioLogadoId, string motivo); // NOVO
    Task ConfirmarAgendamentoAsync(Guid agendamentoId, Guid usuarioLogadoId); // NOVO
}