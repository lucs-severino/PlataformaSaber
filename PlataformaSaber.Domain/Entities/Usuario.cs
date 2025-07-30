public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public UsuarioTipo Tipo { get; set; }
    public UsuarioStatus Status { get; set; }

    public Usuario(Guid id, string nome, string email, string senhaHash, UsuarioTipo tipo, UsuarioStatus status)
    {
        Id = id;
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Tipo = tipo;
        Status = status;
    }
    
    public Usuario () {}
}