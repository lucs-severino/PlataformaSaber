using System.Net.Mail;
public abstract class Pessoa
{
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Cpf { get; protected set; }
    public string Email { get; protected set; }
    public Password Senha { get; private set; }
    public DateTime DataCadastro { get; protected set; }
    public DateTime? DataNascimento { get; private set; }
    public PessoaStatus Status { get; private set; }

    protected Pessoa() { }

    public Pessoa(string nome, string email, string senha, string cpf, DateTime? dataNascimento)
    {
        Id = Guid.NewGuid(); ;
        Nome = ValidarNome (nome);
        Email = ValidarEmail(email);
        Senha = Password.Create(senha);
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

    public void AtualizarSenha(string novaSenha)
    {
        Senha = Password.Create(novaSenha);
    }

    public bool VerificarSenha(string senha)
    {
        return Senha.Verify(senha);
    }

    private string ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "O email não pode ser vazio.");
        if (email.Length > 150)
            throw new ArgumentException("Nome muito longo", nameof(email));
        try
        {
            var addr = new MailAddress(email);
            return addr.Address;
        }
        catch
        {
            throw new ArgumentException("Email inválido.", nameof(email));
        }
    }
    private string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentNullException(nameof(nome));
        if (nome.Length > 150)
            throw new ArgumentException("Nome muito longo", nameof(nome));
        return nome;
    }

}
