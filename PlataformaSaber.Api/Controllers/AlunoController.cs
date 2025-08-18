using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunoController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var alunos = await _alunoService.ObterTodosAsync();
        return Ok(alunos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var aluno = await _alunoService.ObterPorIdAsync(id);
        if (aluno == null) return NotFound();
        return Ok(aluno);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AlunoDto alunoDto)
    {
        await _alunoService.AtualizarAsync(alunoDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {

        await _alunoService.RemoverAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AlunoDto alunoDto)
    {
        await _alunoService.AdicionarAsync(alunoDto);
        return NoContent();
    }
}


