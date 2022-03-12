using System.Text.Json;

namespace Ficha10_View.Models
{
    public class JsonLoader
    {
       public static Characters LoadCharactersJSON()
        {
            var charJson = File.ReadAllText("JSON/Characters.json");
            Characters c = JsonSerializer.Deserialize<Characters>(charJson);

            return c;
        }

        public static Employees LoadEmployeesJSON()
        {
            string text = File.ReadAllText("JSON/Employees.json");

            Employees emps = JsonSerializer.Deserialize<Employees>(text);

            return emps;
        }
    }
}
