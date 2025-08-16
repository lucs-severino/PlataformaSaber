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

        builder.Property(p => p.Senha)
            .HasConversion(
                pw => pw.Hash,
                hash => Password.CreateFromHash(hash)
            )
            .IsRequired()
            .HasColumnName("SenhaHash");

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(p => p.DataCadastro)
            .IsRequired();

        builder.Property(p => p.DataNascimento);

        builder.Property(p => p.Status)
            .IsRequired();

    }
}