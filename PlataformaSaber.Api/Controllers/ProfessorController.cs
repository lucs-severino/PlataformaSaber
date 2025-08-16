using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _ProfessorService;

    public ProfessorController(IProfessorService ProfessorService)
    {
        _ProfessorService = ProfessorService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var Professors = await _ProfessorService.ObterTodosAsync();
        return Ok(Professors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var Professor = await _ProfessorService.ObterPorIdAsync(id);
        if (Professor == null) return NotFound();
        return Ok(Professor);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ProfessorDto ProfessorDto)
    {
        if (id != ProfessorDto.Id)
            return BadRequest("IDs não coincidem.");

        await _ProfessorService.AtualizarAsync(ProfessorDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _ProfessorService.RemoverAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProfessorDto ProfessorDto)
    {
        await _ProfessorService.AdicionarAsync(ProfessorDto);
        return NoContent();
    }
}


