using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IPessoaRepository<>), typeof(PessoaRepository<>));
        services.AddScoped<IAlunoRepository, AlunoRepository>();
        services.AddScoped<IProfessorRepository,ProfessorRepository>();
        services.AddScoped<IAdministracaoRepository, AdministracaoRepository>();
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

        services.AddScoped<IUserAuthentication, JwtAuthService>();

        return services;
    }
}