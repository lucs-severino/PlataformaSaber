using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<IAdministracaoService, AdministracaoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}

