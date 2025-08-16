public class ProfessorRepository : Repository<Professor>, IProfessorRepository
{
    public ProfessorRepository(ApplicationDbContext context) : base(context)
    {
    }
}