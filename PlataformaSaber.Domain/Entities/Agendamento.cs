public class Agendamento
{
    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public Guid ProfessorId { get;  set; }
    public DateTime DataHora { get; set; }
    public DateTime DataCriacao { get; set; }
    public AgendamentoStatus Status { get;  set; } 

    private readonly List<AgendamentoHistorico> _historico = new();
    public IReadOnlyCollection<AgendamentoHistorico> Historico => _historico.AsReadOnly();

    public virtual Aluno Aluno { get; private set; } = null!;
    public virtual Professor Professor { get; private set; } = null!;

    protected Agendamento() { }

    public Agendamento(Guid alunoId, Guid professorId, Guid agendadoPorId, DateTime dataHora)
    {
        Id = Guid.NewGuid();
        AlunoId = alunoId;
        ProfessorId = professorId;
        DataHora = dataHora;
        Status = AgendamentoStatus.Agendado; 
        DataCriacao = DateTime.UtcNow;

        AdicionarHistorico(AgendamentoStatus.Agendado, agendadoPorId, "Agendamento criado");
    }

    public void Confirmar(Guid usuarioId)
    {
        if (Status != AgendamentoStatus.Agendado)
            throw new InvalidOperationException("Somente agendamentos com status 'Agendado' podem ser confirmados.");
        
        Status = AgendamentoStatus.Confirmado;
        AdicionarHistorico(AgendamentoStatus.Confirmado, usuarioId, "Agendamento confirmado pelo usuário.");
    }
    public void Cancelar(Guid usuarioId, string motivo)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException("O motivo do cancelamento é obrigatório.");
        
        if (Status == AgendamentoStatus.Realizado || Status == AgendamentoStatus.Cancelado)
            throw new InvalidOperationException("Este agendamento não pode mais ser cancelado.");

        Status = AgendamentoStatus.Cancelado;
        AdicionarHistorico(AgendamentoStatus.Cancelado, usuarioId, motivo);
    }

    private void AdicionarHistorico(AgendamentoStatus status, Guid usuarioId, string? motivo)
    {
        var novoHistorico = new AgendamentoHistorico(Id, status, usuarioId, motivo);
        _historico.Add(novoHistorico);
    }
}