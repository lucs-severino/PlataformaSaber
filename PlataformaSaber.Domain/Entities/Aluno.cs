public class Aluno : Pessoa
{
    protected Aluno() { }
    public Aluno(string nome, string email, string senhaHash, string cpf, DateTime? dataNascimento)
        : base(nome, email, senhaHash, cpf, dataNascimento)
    {

    }
}
