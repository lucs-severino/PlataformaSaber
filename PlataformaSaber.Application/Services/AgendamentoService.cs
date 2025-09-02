using System.Globalization;
using System.Threading.Tasks;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository)
    {
        _agendamentoRepository = agendamentoRepository;
    }

    public async Task CriarAgendamentoAsync(AgendamentoDto dto, Guid usuarioLogadoId)
    {
        
        if (!DateTime.TryParseExact(
                $"{dto.Data} {dto.Hora}", 
                "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.AssumeUniversal,
                out var dataHora))
        {
            throw new ArgumentException("Formato de data ou hora inválido.");
        }
        
        var horarioOcupado = await _agendamentoRepository.HorarioOcupadoAsync(dto.ProfessorId, dataHora);
        if (horarioOcupado)
        {
            throw new InvalidOperationException("Este horário não está mais disponível para o professor selecionado.");
        }

        var novoAgendamento = new Agendamento(dto.AlunoId, dto.ProfessorId, usuarioLogadoId, dataHora);

        await _agendamentoRepository.AdicionarAsync(novoAgendamento);
    }

    public async Task CancelarAgendamentoAsync(Guid agendamentoId, Guid usuarioLogadoId, string motivo)
    {
        var agendamento = await _agendamentoRepository.ObterPorIdAsync(agendamentoId);
        if (agendamento == null)
            throw new InvalidOperationException("Agendamento não encontrado.");

        agendamento.Cancelar(usuarioLogadoId, motivo);

        await _agendamentoRepository.AtualizarAsync(agendamento);
    }

    public async Task ConfirmarAgendamentoAsync(Guid agendamentoId, Guid usuarioLogadoId)
    {
        var agendamento = await _agendamentoRepository.ObterPorIdAsync(agendamentoId);
        if (agendamento == null)
            throw new InvalidOperationException("Agendamento não encontrado.");

        agendamento.Confirmar(usuarioLogadoId);
        
        await _agendamentoRepository.AtualizarAsync(agendamento);
    }
}