using Microsoft.EntityFrameworkCore;
public class ApplicationDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; } = null!;
        public DbSet<Aluno> Alunos { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PessoaConfiguration());

           // modelBuilder.ApplyConfiguration(new AlunoConfiguration());
        }
    }