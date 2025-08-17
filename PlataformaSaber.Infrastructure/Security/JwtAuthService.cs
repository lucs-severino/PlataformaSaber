using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class JwtAuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IPessoaRepository<Pessoa> _pessoaRepository;

    public JwtAuthService(IConfiguration config, IPessoaRepository<Pessoa> pessoaRepository)
    {
        _config = config;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<string?> AutenticarAsync(string email, string senha)
    {
        var usuarios = await _pessoaRepository.BuscarAsync(p => p.Email == email);

        var usuario = usuarios.FirstOrDefault();

        if (usuario == null || !usuario.VerificarSenha(senha))
            return null;

        return GerarToken(usuario.Email, usuario.Id.ToString());
    }

    private string GerarToken(string email, string userId)
    {
        var claims = new[]
        {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}