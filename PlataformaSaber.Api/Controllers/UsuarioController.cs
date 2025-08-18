using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodasPessoas()
    {
        var pessoas = await _usuarioService.ObterTodasPessoasAsync();
        return Ok(pessoas);
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarPessoas([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? tipo)
    {
        var pessoas = await _usuarioService.BuscarPessoasAsync(email, tipo);
        return Ok(pessoas);
    }
}