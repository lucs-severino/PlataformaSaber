public class Aluno : Pessoa
{

    public Aluno(string nome, string email, string senhaHash, string cpf, DateTime? dataNascimento)
        : base(nome, email, senhaHash, cpf, dataNascimento, TipoPessoa.Aluno)
    {

    }
}
