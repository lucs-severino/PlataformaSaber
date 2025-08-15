using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PlataformaSaber.Application.DTOs;
using PlataformaSaber.Application.Interfaces;
using PlataformaSaber.Domain.Entities;
using PlataformaSaber.Domain.Repositories;

namespace PlataformaSaber.Application.Services
{
    public partial class AlunoService : Service<AlunoDto, Aluno>, IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository /*, outros deps */) : base(alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<AlunoDto?> AutenticarAsync(string email, string senha)
        {
            // Se existir um método específico no repositório para buscar por email (ex: ObterPorEmailAsync),
            // é melhor usá-lo em vez de ObterTodosAsync.
            var alunos = await _alunoRepository.ObterTodosAsync();
            var aluno = alunos.FirstOrDefault(a => a.Email == email);

            if (aluno == null)
                return null;

            // NOTE: SHA256 usado apenas como exemplo; trocar para BCrypt/Argon2 em produção.
            string senhaHash = ComputeSha256Hash(senha);
            if (aluno.SenhaHash != senhaHash)
                return null;

            return MapToDto(aluno);
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (var sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}