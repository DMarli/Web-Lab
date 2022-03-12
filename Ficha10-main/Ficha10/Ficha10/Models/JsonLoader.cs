using System.Text.Json;

namespace Ficha10
{
    public class JsonLoader
    {
       public static Characters LoadCharactersJSON()
        {
            var charJson = File.ReadAllText("Characters.json");
            Characters c = JsonSerializer.Deserialize<Characters>(charJson);

            return c;
        }

        public static Employees LoadEmployeesJSON()
        {
            string text = File.ReadAllText("Employees.json");

            Employees emps = JsonSerializer.Deserialize<Employees>(text);

            return emps;
        }
    }
}
