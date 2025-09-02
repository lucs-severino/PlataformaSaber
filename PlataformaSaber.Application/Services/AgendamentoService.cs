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

    public async Task<HorarioDisponivelDto> ObterHorariosDisponiveisAsync(Guid professorId, DateTime data)
    {
        var horariosOcupadosUtc = await _agendamentoRepository.ObterHorariosOcupadosAsync(professorId, data);

        var horariosBloqueados = new HashSet<DateTime>();
        foreach (var horarioUtc in horariosOcupadosUtc)
        {
            var horarioLocal = horarioUtc.ToLocalTime();
            horariosBloqueados.Add(horarioLocal);
            horariosBloqueados.Add(horarioLocal.AddMinutes(30));
        }

        var horariosDisponiveis = new HorarioDisponivelDto();
        var horaInicio = data.Date.AddHours(8);
        var horaFim = data.Date.AddHours(21);

        for (var horario = horaInicio; horario < horaFim; horario = horario.AddMinutes(30))
        {
            if (!horariosBloqueados.Contains(horario))
            {
                horariosDisponiveis.Horarios.Add(horario.ToString("HH:mm"));
            }
        }

        return horariosDisponiveis;
    }
}