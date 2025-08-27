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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioPorId(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId))
        {
            return BadRequest("O ID fornecido não é um GUID válido.");
        }

        var usuario = await _usuarioService.BuscarUsuarioPorId(guidId);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AlterarUsuario(Guid id, [FromBody] UsuarioDto dto)
    {
        if (id == Guid.Empty || dto == null)
            return BadRequest("ID inválido ou dados do usuário não fornecidos.");

        await _usuarioService.AlterarUsuarioAsync(dto, id);

        return Ok(dto);
    }


}