public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    public UsuarioTipo Tipo { get; private set; }
    public UsuarioStatus Status { get; private set; }

    
    public Usuario(string nome, string email, string senhaHash, UsuarioTipo tipo, UsuarioStatus status, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid(); 
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        SenhaHash = senhaHash ?? throw new ArgumentNullException(nameof(senhaHash));
        Tipo = tipo;
        Status = status;
    }

    protected Usuario() { }
}