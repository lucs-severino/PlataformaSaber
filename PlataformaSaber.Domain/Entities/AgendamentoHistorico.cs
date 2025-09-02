public class AgendamentoHistorico
{
    public Guid Id { get; private set; }
    public Guid AgendamentoId { get; private set; }
    public AgendamentoStatus Status { get; private set; } 
    public Guid UsuarioResponsavelId { get; private set; } 
    public string? Motivo { get; private set; } 
    public DateTime DataOcorrencia { get; private set; }

    // Propriedades de navegação
    public virtual Agendamento Agendamento { get; private set; } = null!;
    public virtual Pessoa UsuarioResponsavel { get; private set; } = null!;

    protected AgendamentoHistorico() { }

    public AgendamentoHistorico(Guid agendamentoId, AgendamentoStatus status, Guid usuarioResponsavelId, string? motivo = null)
    {
        Id = Guid.NewGuid();
        AgendamentoId = agendamentoId;
        Status = status;
        UsuarioResponsavelId = usuarioResponsavelId;
        Motivo = motivo;
        DataOcorrencia = DateTime.UtcNow;
    }
}
