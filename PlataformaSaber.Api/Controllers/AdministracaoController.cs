using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AdministracaoController : ControllerBase
{
    private readonly IAdministracaoService _administracaoService;

    public AdministracaoController(IAdministracaoService AdministracaoService)
    {
        _administracaoService = AdministracaoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var Administracaos = await _administracaoService.ObterTodosAsync();
        return Ok(Administracaos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var Administracao = await _administracaoService.ObterPorIdAsync(id);
        if (Administracao == null) return NotFound();
        return Ok(Administracao);
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AdministracaoDto AdministracaoDto)
    {
        await _administracaoService.AtualizarAsync(AdministracaoDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _administracaoService.RemoverAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AdministracaoDto AdministracaoDto)
    {
        await _administracaoService.AdicionarAsync(AdministracaoDto);
        return NoContent();
    }
}


