public abstract class Pessoa
{
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Cpf { get; protected set; }
    public string Email { get; protected set; }
    public string SenhaHash { get; protected set; }
    public DateTime DataCadastro { get; protected set; }
    public DateTime? DataNascimento { get; private set; }
    public PessoaStatus Status { get; private set; }


    public Pessoa(string nome, string email, string senhaHash, string cpf, DateTime? dataNascimento)
    {
        Id = Guid.NewGuid(); ;
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        SenhaHash = senhaHash ?? throw new ArgumentNullException(nameof(senhaHash));
        DataCadastro = DateTime.UtcNow;
        Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
        DataNascimento = dataNascimento;
        Status = PessoaStatus.Ativo;
    }

    public void AtualizarDados(string nome, string email)
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void AtualizarSenha(string senhaHash)
    {
        SenhaHash = senhaHash ?? throw new ArgumentNullException(nameof(senhaHash));
    }

    public void AtualizarStatus(PessoaStatus status)
    {
        Status = status;
    }

}