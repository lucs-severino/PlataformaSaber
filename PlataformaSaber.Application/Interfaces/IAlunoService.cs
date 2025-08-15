// IAlunoService.cs

using System.Threading.Tasks;

namespace PlataformaSaber.Application.Interfaces
{
    public interface IAlunoService
    {
        Task<AlunoDto> GetAlunoByIdAsync(int id);
        Task<IEnumerable<AlunoDto>> GetAllAlunosAsync();
        Task AddAlunoAsync(AlunoDto aluno);
        Task UpdateAlunoAsync(AlunoDto aluno);
        Task DeleteAlunoAsync(int id);
    }
}