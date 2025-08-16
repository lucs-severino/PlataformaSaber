public class AlunoRepository : PessoaRepository<Aluno>, IAlunoRepository
{
    public AlunoRepository(ApplicationDbContext context) : base(context)
    {
    }
}