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

            horariosBloqueados.Add(horarioLocal.AddMinutes(-30));

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

    public async Task<DashboardCardsDto> ObterDadosDashboardCardsAsync()
    {
        var contagem = await _agendamentoRepository.ContarStatusAgendamentosAsync();
        return new DashboardCardsDto
        {
            Total = contagem.Sum(c => c.Value),
            Pendentes = contagem.GetValueOrDefault(AgendamentoStatus.Agendado),
            Confirmadas = contagem.GetValueOrDefault(AgendamentoStatus.Confirmado),
            Canceladas = contagem.GetValueOrDefault(AgendamentoStatus.Cancelado)
        };
    }

    public async Task<PagedResultDto<AgendamentoDetalheDto>> ObterAgendamentosPaginadosAsync(
    int page, int pageSize, string? nome, string? status, DateTime? data)
    {
        AgendamentoStatus? statusEnum = null;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<AgendamentoStatus>(status, true, out var parsedStatus))
        {
            statusEnum = parsedStatus;
        }

        (IEnumerable<Agendamento> items, long totalCount) =
            await _agendamentoRepository.ObterAgendamentosPaginadosAsync(page, pageSize, nome, status, data);

        var itemsDto = items.Select(a => new AgendamentoDetalheDto
        {
            Id = a.Id,
            DataHora = a.DataHora,
            Status = a.Status.ToString(),
            Aluno = new PessoaSimplificadoDto { Nome = a.Aluno.Nome, Email = a.Aluno.Email },
            Professor = new PessoaSimplificadoDto { Nome = a.Professor.Nome, Email = a.Professor.Email }
        });

        return new PagedResultDto<AgendamentoDetalheDto>
        {
            Items = itemsDto,
            CurPage = page,
            ItemsTotal = totalCount,
            PageTotal = (int)Math.Ceiling((double)totalCount / pageSize)
        };
    }



    public async Task<AgendamentoDetalhesResponseDto?> ObterDetalhesAgendamentoAsync(Guid id)
    {
        var agendamento = await _agendamentoRepository.ObterDetalhesPorIdAsync(id);
        if (agendamento == null) return null;

        var historicoCriacao = agendamento.Historico
            .OrderBy(h => h.DataOcorrencia)
            .FirstOrDefault();

        var historicoCancelamento = agendamento.Historico
            .FirstOrDefault(h => h.Status == AgendamentoStatus.Cancelado);

        return new AgendamentoDetalhesResponseDto
        {
            Id = agendamento.Id,
            DataHora = agendamento.DataHora,
            Status = agendamento.Status.ToString(),
            Aluno = new PessoaSimplificadoDto { Nome = agendamento.Aluno.Nome, Email = agendamento.Aluno.Email },
            Professor = new PessoaSimplificadoDto { Nome = agendamento.Professor.Nome, Email = agendamento.Professor.Email },
            DataCriacao = agendamento.DataCriacao,
            AgendadoPor = historicoCriacao?.UsuarioResponsavel?.Nome ?? "Desconhecido",
            MotivoCancelamento = historicoCancelamento?.Motivo,
            Historico = agendamento.Historico.OrderBy(h => h.DataOcorrencia).Select(h => new AgendamentoHistoricoDto
            {
                Data = h.DataOcorrencia,
                Status = h.Status.ToString(),
                Responsavel = h.UsuarioResponsavel.Nome,
                Motivo = h.Motivo
            })
        };
    }
}