using Ficha12.Models;


namespace Ficha12.Data
{
    static class LibraryExtension
    {
        public static void CreateDbIfNotExists (this IHost host)
            //verifica se existe, senão cria, chamado no program
        {
            {
                using (var scope = host.Services.CreateScope())
                { 
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<LibraryContext>();

                    //Creates data if not exists

                    if (context.Database.EnsureCreated())
                    {
                        LibraryDBInitializer.InsertData(context);
                        //drop schema no Workbench se já estiver criada
                        //Só faz isto uma vez, depois já fica criada
                    }
                }
            }
        }
    }
}
