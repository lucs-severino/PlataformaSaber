using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsuariosController : BaseController
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodasPessoas([FromQuery] int page = 1, [FromQuery] string? nome = null)
    {
        try
        {
            var pessoas = await _usuarioService.ObterTodasPessoasPaginadasAsync(page, nome);
            return Ok(pessoas);

        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }

    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarPessoas([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? tipo)
    {
        try
        {
            var pessoas = await _usuarioService.BuscarPessoasAsync(email, tipo);
            return Ok(pessoas);
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioPorId(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId))
        {
            return BadRequest("O ID fornecido não é um GUID válido.");
        }
        try
        {
            var usuario = await _usuarioService.BuscarUsuarioPorId(guidId);

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);

        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AlterarUsuario(Guid id, [FromBody] UsuarioDto dto)
    {
        if (id == Guid.Empty || dto == null)
            return BadRequest("ID inválido ou dados do usuário não fornecidos.");
        try
        {
            await _usuarioService.AlterarUsuarioAsync(dto, id);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriaUsuario([FromBody] UsuarioDto dto)
    {
        if (dto is null)
            return RespostaBadRequest("Dados inválidos ou não fornecidos.");

        try
        {
            await _usuarioService.CriarUsuarioAsync(dto);
            return RespostaSucesso(dto);
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }

}