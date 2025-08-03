using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.SenhaHash)
            .IsRequired();

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(p => p.DataCadastro)
            .IsRequired();

        builder.Property(p => p.DataNascimento);

        builder.Property(p => p.Status)
            .IsRequired();


        builder.HasDiscriminator<string>("TipoPessoa")
            .HasValue<Pessoa>("Pessoa")
            .HasValue<Aluno>("Aluno");

    }
}