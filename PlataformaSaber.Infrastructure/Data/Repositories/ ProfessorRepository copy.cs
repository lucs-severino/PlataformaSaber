public class AdministracaoRepository : Repository<Administracao>, IAdministracaoRepository
{
    public AdministracaoRepository(ApplicationDbContext context) : base(context)
    {
    }
}