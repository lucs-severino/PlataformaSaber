using Microsoft.AspNetCore.Mvc;
using PlataformaSaber.Application.Interfaces;

namespace PlataformaSaber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AuthController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var aluno = await _alunoService.AutenticarAsync(request.Email, request.Senha);

            if (aluno == null)
                return Unauthorized();

            // Aqui pode retornar um JWT, por enquanto retorna o DTO do aluno
            return Ok(aluno);
        }
    
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}