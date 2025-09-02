using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AgendamentoHistoricoConfiguration : IEntityTypeConfiguration<AgendamentoHistorico>
{
    public void Configure(EntityTypeBuilder<AgendamentoHistorico> builder)
    {
        builder.ToTable("AgendamentoHistoricos");
        
        builder.HasKey(h => h.Id);

        builder.Property(h => h.DataOcorrencia)
            .IsRequired();

        builder.Property(h => h.Motivo)
            .HasMaxLength(500);

        builder.Property(h => h.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AgendamentoStatus)Enum.Parse(typeof(AgendamentoStatus), v)
            );

        builder.HasOne(h => h.UsuarioResponsavel)
            .WithMany()
            .HasForeignKey(h => h.UsuarioResponsavelId);
    }
}