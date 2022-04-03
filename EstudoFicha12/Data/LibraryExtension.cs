using EstudoFicha12.Models;

namespace EstudoFicha12.Data
{
    public static class LibraryExtension
    {
            public static void CreateDbIfNotExists(this IHost host)
            //verifica se existe, senão cria, chamado no program
        {
            {
                    using (var scope = host.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;
                        var context = services.GetRequiredService<LibraryContext>();
                        // Creates the database if not exists
                        if (context.Database.EnsureCreated())
                        {
                        //drop schema no Workbench se já estiver criada
                        //Só faz isto uma vez, depois já fica criada
                        LibraryDBInitializer.InsertData(context);
                        }
                    }
                }
            }
        }
}
