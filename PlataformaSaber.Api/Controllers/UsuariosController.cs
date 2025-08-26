using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodasPessoas([FromQuery] int page = 1)
    {
        var pessoas = await _usuarioService.ObterTodasPessoasPaginadasAsync(page);
        return Ok(pessoas);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarPessoas([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? tipo)
    {
        var pessoas = await _usuarioService.BuscarPessoasAsync(email, tipo);
        return Ok(pessoas);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AlterarUsuario(Guid id, [FromBody] UsuarioDto dto)
    {
        if (id == Guid.Empty || dto == null || id != dto.Id)
            return BadRequest("ID inválido ou dados do usuário não fornecidos.");

        await _usuarioService.AlterarUsuarioAsync(dto);

        return NoContent();
    }

}