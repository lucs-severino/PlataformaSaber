public class AgendamentoDetalheDto
{
    public Guid Id { get; set; }
    public string Disciplina { get; set; } = "Indefinido"; 
    public string Descricao { get; set; } = "Aula de reforço";
    public PessoaSimplificadoDto Aluno { get; set; } = null!;
    public PessoaSimplificadoDto Professor { get; set; } = null!;
    public DateTime DataHora { get; set; }
    public string Status { get; set; } = null!;
}

public class PessoaSimplificadoDto
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
}