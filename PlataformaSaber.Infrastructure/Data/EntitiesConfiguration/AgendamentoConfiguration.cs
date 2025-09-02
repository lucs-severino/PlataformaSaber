using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
{
    public void Configure(EntityTypeBuilder<Agendamento> builder)
    {
        builder.ToTable("Agendamentos"); 
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AgendamentoStatus)Enum.Parse(typeof(AgendamentoStatus), v)
            );

        builder.HasOne(a => a.Aluno)
            .WithMany() 
            .HasForeignKey(a => a.AlunoId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(a => a.Professor)
            .WithMany() 
            .HasForeignKey(a => a.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

      
        builder.HasMany(a => a.Historico)
            .WithOne(h => h.Agendamento)
            .HasForeignKey(h => h.AgendamentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}