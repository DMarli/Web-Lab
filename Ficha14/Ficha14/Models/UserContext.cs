using Microsoft.EntityFrameworkCore;

namespace Ficha14.Models
{
    public class UserContext: DbContext
    {
     
            public DbSet<User> Users { get; set; }

            public UserContext(DbContextOptions options) : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    optionsBuilder.UseMySQL("server=localhost;database=users;" +
                        "user=root;password=2021");
            //mudar sempre nome da base de dados
                }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.ID);
                    entity.Property(e => e.UserName).IsRequired();
                    //propriedade Has One era caso houvesse uma propriedade que tivesse outra tabela
                });
            }

    }
}
