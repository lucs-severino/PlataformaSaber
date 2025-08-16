using Microsoft.AspNetCore.Mvc;

[ApiController]
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
        if (id != AdministracaoDto.Id)
            return BadRequest("IDs não coincidem.");
        
        var verificarAdministracao = await _administracaoService.BuscarAsync(p => p.Id == AdministracaoDto.Id);
        if (!verificarAdministracao.Any())
            return NotFound($"Administração com ID {AdministracaoDto.Id} não encontrada.");

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
        var cpfExistente = await _administracaoService.BuscarAsync(p => p.Cpf == AdministracaoDto.Cpf);

        if (cpfExistente.Any())
            return BadRequest($"Já existe uma pessoa cadastrada com o CPF {AdministracaoDto.Cpf}.");

        var emailExistente = await _administracaoService.BuscarAsync(p => p.Email == AdministracaoDto.Email);
        if (emailExistente.Any())
            return BadRequest($"Já existe uma pessoa cadastrada com o Email {AdministracaoDto.Email}.");

        await _administracaoService.AdicionarAsync(AdministracaoDto);
        return NoContent();
    }
}


