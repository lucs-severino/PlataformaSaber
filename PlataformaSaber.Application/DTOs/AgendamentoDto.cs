public class AgendamentoDto
{
    public Guid AlunoId { get; set; }
    public Guid ProfessorId { get; set; }
    public string Data { get; set; } = null!;
    public string Hora { get; set; } = null!;
}