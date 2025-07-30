public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } 
    public string Email { get; set; } 
    public string SenhaHash { get; set; } 
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