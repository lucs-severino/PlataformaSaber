
public class AdministracaoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateTime? DataNascimento { get; set; }
    public string Status { get; set; } = null!;
}

