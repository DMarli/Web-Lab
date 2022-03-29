namespace EstudoFicha12.Models
{
    public class Book
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public int Pages { get; set; }
        public virtual Publisher Publisher { get; set; }
        //Carregamento preguiçoso, carrega o mínimo, e, se este for necessário fazemos um pedido
        //Lazy Loading
        public string Title { get; set; }
    }
}
