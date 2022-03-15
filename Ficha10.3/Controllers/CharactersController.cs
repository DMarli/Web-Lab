using Ficha10._3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace Ficha10._3.Controllers
{
    [Route("[controller]")]
    //EmployeesController vai ser rota Employees, se tiver /api/[controller], vai ter api antes
    [ApiController]

    public class CharactersController : ControllerBase
    {
        public Characters characters;

        public CharactersController()
        {
           characters = JsonLoader.LoadCharactersJSON();
        }

        [HttpGet(Name = "GetCharacters")]
        public IEnumerable<Character> Get()
        {
            return characters.CharactersList;
        }


        [HttpGet("{id:int}", Name = "GetByIdChar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Character? e = characters.CharactersList.Find(e => e.Id == id);

            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return NotFound($"ID: {id} Not Found"); 
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Character? e = characters.CharactersList.Find(e => e.Id == id);

            if (e != null)
            {
                characters.CharactersList.Remove(e);
                return Ok("Id " + id + " Removed");
            }
            else
            {
                return NotFound("ID " + id + " Not Found");
            }


        }
        // FALTA INJECTION
        // POST api/<EmployeesController>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Character character)
        {
            Character lastChar = characters.CharactersList[characters.CharactersList.Count - 1];

            if (characters.CharactersList.Count == 0)
            {
                lastChar.Id = 1;
                characters.CharactersList.Add(character);
                return Ok(character);
            }
            else
            {
                lastChar.Id = lastChar.Id + 1;
                characters.CharactersList.Add(character);
                return Created("/employees/" + character.Id, character);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Character character)
        {
            var e = characters.CharactersList.Find(e => e.Id == id);

            if (e == null)
            {
                return NotFound(id);
            }
            else
            {
                e.Id = character.Id;
                e.Name = character.Name;
                e.Gender = character.Gender;
                e.Homeworld = character.Homeworld;
                e.Born = character.Born;
                e.Jedi = character.Jedi;    

                return Ok(e);
            }
        }

        [HttpGet("jedi/{jedi}", Name = "GetByJedi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(bool jedi)
        {
            List<Character> e = characters.CharactersList.FindAll(e => e.Jedi == jedi);

            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return NotFound($"Jedi: {jedi} Not Found");
            }
        }

        [HttpGet("download", Name = "GetCharDownload")]
        public IActionResult GetDownload()

        {
            string jsonAllChar = JsonSerializer.Serialize<Characters>(characters);

            System.IO.File.WriteAllText("./JSON/allChar.json", jsonAllChar);

            try
            {
                //namespace.class.function
                byte[] bytes = System.IO.File.ReadAllBytes("./JSON/allChar.json");
                return File(bytes, "application/json", "Characters.json");
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

    }
}
