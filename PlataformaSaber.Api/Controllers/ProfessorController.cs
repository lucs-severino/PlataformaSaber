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
        if (id != professorDto.Id)
            return BadRequest("IDs não coincidem.");


        var verificarProfessor = await _professorService.BuscarAsync(p => p.Id == professorDto.Id);
        if (!verificarProfessor.Any())
            return NotFound($"Professor com ID {professorDto.Id} não encontrado.");

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
        var cpfExistente = await _professorService.BuscarAsync(p => p.Cpf == professorDto.Cpf); 

        if (cpfExistente.Any())
            return BadRequest($"Já existe uma pessoa cadastrada com o CPF {professorDto.Cpf}.");

        var emailExistente = await _professorService.BuscarAsync(p => p.Email == professorDto.Email);

        if (emailExistente.Any())
            return BadRequest($"Já existe uma pessoa cadastrada com o Email {professorDto.Email}.");

        await _professorService.AdicionarAsync(professorDto);
        return NoContent();
    }
}


