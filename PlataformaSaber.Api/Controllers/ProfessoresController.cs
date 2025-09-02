using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfessoresController : BaseController
{
    private readonly IProfessorService _professorService;

    public ProfessoresController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodosProfessores([FromQuery] int page = 1, [FromQuery] string? nome = null)
    {
        try
        {
            var resultado = await _professorService.ObterProfessoresPaginadoAsync(page, nome);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }
}