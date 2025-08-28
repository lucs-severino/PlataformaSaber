using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : ControllerBase
{
    protected IActionResult RespostaSucesso(object? data = null) =>
        Ok(new { success = true, data });

    protected IActionResult RespostaBadRequest(string mensagem) =>
        BadRequest(new { success = false, message = mensagem });

    protected IActionResult RespostaConflito(string mensagem) =>
        Conflict(new { success = false, message = mensagem });

    protected IActionResult RespostaErro(string mensagem) =>
        StatusCode(500, new { success = false, message = mensagem });

    protected IActionResult TratarErro(Exception ex) =>
        ex switch
        {
            ArgumentException => RespostaBadRequest(ex.Message),
            InvalidOperationException => RespostaConflito(ex.Message),
            _ => RespostaErro($"Erro inesperado: {ex.Message}")
        };
}
