using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AdministracaoController : ControllerBase
{
    private readonly IAdministracaoService _AdministracaoService;

    public AdministracaoController(IAdministracaoService AdministracaoService)
    {
        _AdministracaoService = AdministracaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var Administracaos = await _AdministracaoService.ObterTodosAsync();
        return Ok(Administracaos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var Administracao = await _AdministracaoService.ObterPorIdAsync(id);
        if (Administracao == null) return NotFound();
        return Ok(Administracao);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AdministracaoDto AdministracaoDto)
    {
        if (id != AdministracaoDto.Id)
            return BadRequest("IDs não coincidem.");

        await _AdministracaoService.AtualizarAsync(AdministracaoDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _AdministracaoService.RemoverAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AdministracaoDto AdministracaoDto)
    {
        await _AdministracaoService.AdicionarAsync(AdministracaoDto);
        return NoContent();
    }
}


