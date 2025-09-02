using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AgendamentosController : BaseController
{
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentosController(IAgendamentoService agendamentoService)
    {
        _agendamentoService = agendamentoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAgendamento([FromBody] AgendamentoDto dto)
    {
        if (dto is null)
            return RespostaBadRequest("Dados do agendamento não fornecidos.");

        try
        {
            var usuarioLogadoId = ObterUsuarioIdLogado();

            await _agendamentoService.CriarAgendamentoAsync(dto, usuarioLogadoId);
            return RespostaSucesso(new { message = "Agendamento criado com sucesso!" });
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }

    [HttpPut("{id:guid}/confirmar")]
    public async Task<IActionResult> ConfirmarAgendamento(Guid id)
    {
        try
        {
            var usuarioLogadoId = ObterUsuarioIdLogado();
            await _agendamentoService.ConfirmarAgendamentoAsync(id, usuarioLogadoId);
            return RespostaSucesso(new { message = "Agendamento confirmado com sucesso." });
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }

    [HttpPut("{id:guid}/cancelar")]
    public async Task<IActionResult> CancelarAgendamento(Guid id, [FromBody] CancelamentoDto dto)
    {
        if (dto is null || string.IsNullOrWhiteSpace(dto.Motivo))
            return RespostaBadRequest("O motivo do cancelamento é obrigatório.");

        try
        {
            var usuarioLogadoId = ObterUsuarioIdLogado();
            await _agendamentoService.CancelarAgendamentoAsync(id, usuarioLogadoId, dto.Motivo);
            return RespostaSucesso(new { message = "Agendamento cancelado com sucesso." });
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }

    private Guid ObterUsuarioIdLogado()
    {
        var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(usuarioIdClaim, out var usuarioLogadoId))
        {
            throw new UnauthorizedAccessException("Token de usuário inválido ou não encontrado.");
        }
        return usuarioLogadoId;
    }
    
    [HttpGet("disponiveis/{professorId:guid}")]
    public async Task<IActionResult> ObterHorariosDisponiveis(Guid professorId, [FromQuery] DateTime data)
    {
        try
        {
            var horarios = await _agendamentoService.ObterHorariosDisponiveisAsync(professorId, data);
            return Ok(horarios);
        }
        catch (Exception ex)
        {
            return TratarErro(ex);
        }
    }
}