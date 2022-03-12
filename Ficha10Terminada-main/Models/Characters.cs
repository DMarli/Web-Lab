namespace Ficha10
{
    public class Characters : ICharacters
    {
        private List<Character> charactersList;

        List<Character> ICharacters.CharactersList
        {
            get => charactersList;
            set => charactersList = value;
        }

        public Characters()
        {
            charactersList = JsonLoader.LoadCharactersJSON();   
        }
    }
}