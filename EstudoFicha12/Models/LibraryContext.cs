using Microsoft.EntityFrameworkCore;

namespace EstudoFicha12.Models
{
    //Estabelece a sessão com a base de dados
    //Herança da DbContext, Classe do pacote Microsoft.EntityFrameworkCore
    public class LibraryContext: DbContext

    //Propriedades das colunas e relações das tabelas

    {
        //Propriedade do tipo DbSet para realizar queries à base de dados
        //Usa instância do tipo Book e Publisher
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

       //Construtor por parâmetros, gerar e só colocar <LibraryContext>
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost; database=library;" + "user=root; password=2021");
            //É aqui que alteramos o utilizador e password para coincidir com a nossa DB
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating (modelBuilder); //Aparece no tab, o de baixo também, parcialmente
            modelBuilder.Entity<Book>( entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.HasOne(e => e.Publisher); //relação 1 para n - 1 livro só pode ter uma editora
                entity.Property(e => e.Title).IsRequired();
            });
            modelBuilder.Entity<Publisher>(entity =>
           {
               entity.HasKey(e => e.ID); //chave-primária
                entity.Property(e => e.Name).IsRequired(); //é obrigatório, campo não pode ser nulo
            });
        }

    }
}
