public class Professor : Pessoa
{
    protected Professor() { }
    public Professor (string nome, string email, string senhaHash, string cpf, DateTime? dataNascimento)
        : base(nome, email, senhaHash, cpf, dataNascimento)
    {

    }
}
