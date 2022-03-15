using Microsoft.EntityFrameworkCore;

namespace Ficha12.Models

{
    //estabelece a sessão com a base de dados
    public class LibraryContext: DbContext

        //propriedades das colunas e relações das tabelas
    {
        public DbSet<Book> Books { get; set; } //todas as entradas que estiverem na tabela
        public DbSet<Publisher> Publishers { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost; database=library;" + "user=root; password=2021");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.ID); //chave-primária
                entity.Property(e => e.Name).IsRequired();  
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN); //chave-primária
                entity.Property(e => e.Title).IsRequired(); //este campo não pode ser nulo
                entity.HasOne(d => d.Publisher); //relação 1 para n - 1 livro só pode ter uma editora
            });
        }
       
    }
}
