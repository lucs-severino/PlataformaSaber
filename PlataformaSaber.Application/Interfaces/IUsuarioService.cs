
public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> ObterTodasPessoasAsync();
    Task<IEnumerable<UsuarioDto>> BuscarPessoasAsync(string? email = null, string? cpf = null);
    Task<object> ObterTodasPessoasPaginadasAsync(int page);}