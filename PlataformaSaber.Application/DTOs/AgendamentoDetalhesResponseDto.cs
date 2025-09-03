public class AgendamentoDetalhesResponseDto : AgendamentoDetalheDto
{
    public string AgendadoPor { get; set; } = null!;
    public DateTime DataCriacao { get; set; }
    public string? MotivoCancelamento { get; set; }
    public IEnumerable<AgendamentoHistoricoDto> Historico { get; set; } = null!;
}

public class AgendamentoHistoricoDto
{
    public DateTime Data { get; set; }
    public string Status { get; set; } = null!;
    public string Responsavel { get; set; } = null!;
    public string? Motivo { get; set; }
}