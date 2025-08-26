using System.Linq.Expressions;

public class AdministracaoService : PessoaService<AdministracaoDto, Administracao>, IAdministracaoService
{
    private readonly IAdministracaoRepository _AdministracaoRepository;

    public AdministracaoService(IAdministracaoRepository AdministracaoRepository) : base(AdministracaoRepository)
    {
        _AdministracaoRepository = AdministracaoRepository;
    
    }

    public override Administracao MapToEntity(AdministracaoDto dto)
    {
        return new Administracao(dto.Nome, dto.Email, "123456789", dto.Cpf, dto.DataNascimento);
    }

    public override AdministracaoDto MapToDto(Administracao Administracao)
    {
        return new AdministracaoDto
        {
            Id = Administracao.Id,
            Nome = Administracao.Nome,
            Email = Administracao.Email,
            Cpf = Administracao.Cpf,
            DataNascimento = Administracao.DataNascimento,
            Status = Administracao.Status.ToString()
        };
    }
}

