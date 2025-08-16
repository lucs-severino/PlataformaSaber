public class ProfessorRepository : PessoaRepository<Professor>, IProfessorRepository
{
    public ProfessorRepository(ApplicationDbContext context) : base(context)
    {
    }
}