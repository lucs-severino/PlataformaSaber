using System.Threading.Tasks;
using PlataformaSaber.Application.DTOs;

namespace PlataformaSaber.Application.Interfaces
{
    public interface IAlunoService : IService<AlunoDto>
    {
        Task<AlunoDto?> AutenticarAsync(string email, string senha);
    }
}