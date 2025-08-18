using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService ProfessorService)
    {
        _professorService = ProfessorService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var Professors = await _professorService.ObterTodosAsync();
        return Ok(Professors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var Professor = await _professorService.ObterPorIdAsync(id);
        if (Professor == null) return NotFound();
        return Ok(Professor);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ProfessorDto professorDto)
    {
        await _professorService.AtualizarAsync(professorDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _professorService.RemoverAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProfessorDto professorDto)
    {
        await _professorService.AdicionarAsync(professorDto);
        return NoContent();
    }
}


