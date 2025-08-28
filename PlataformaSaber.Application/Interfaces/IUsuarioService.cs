
public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> ObterTodasPessoasAsync();
    Task<IEnumerable<UsuarioDto>> BuscarPessoasAsync(string? email = null, string? cpf = null);
    Task<object> ObterTodasPessoasPaginadasAsync(int page);
    Task AlterarUsuarioAsync(UsuarioDto dto,Guid id);
    Task<UsuarioDto?> BuscarUsuarioPorId(Guid id);
    Task CriarUsuarioAsync(UsuarioDto dto);
}
