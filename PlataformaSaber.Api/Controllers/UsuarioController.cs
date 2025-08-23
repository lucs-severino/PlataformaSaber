using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
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
}