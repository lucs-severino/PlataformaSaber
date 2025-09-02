using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AlunosController : BaseController
{
    private readonly IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodosAlunos([FromQuery] int page = 1, [FromQuery] string? nome = null)
    {
        try
        {
            var resultado = await _alunoService.ObterAlunosPaginadoAsync(page, nome);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }
}