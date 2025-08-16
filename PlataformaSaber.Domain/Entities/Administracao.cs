public class Administracao: Pessoa
{
    protected Administracao() { }
    public Administracao (string nome, string email, string senhaHash, string cpf, DateTime? dataNascimento)
        : base(nome, email, senhaHash, cpf, dataNascimento)
    {

    }
}
