namespace Ficha10_View.Models
{
    public class Characters
    {
       public List <Character> CharactersList { get; set; }
       
        public Characters()
        {
            CharactersList = new List<Character>(); 
        }

        public Characters(List <Character> listaChar)
        {
            CharactersList = listaChar;
        }
    }

}