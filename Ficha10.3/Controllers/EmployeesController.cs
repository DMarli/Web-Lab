using Ficha10._3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace Ficha10._3.Controllers
{
    [Route("[controller]")]
    //EmployeesController vai ser rota Employees, se tiver /api/[controller], vai ter api antes
    //substituído pelo nome que damos ao controller, neste caso Employees, no CharactersController é Characters
    [ApiController]

    public class EmployeesController : ControllerBase
        
    {
        public Employees employees;

        public EmployeesController()
        {
            employees = JsonLoader.LoadEmployeesJSON();
        }

        // GET: api/<EmployeesController>

        [HttpGet(Name = "GetEmployees")]
        public IEnumerable<Employee> Get()
        //Posso devolver qualquer tipo de Lista com INumerable
        {
            return employees.EmployeesList;
        }
         
            //Ou IActionResult
        //{
        //    if (employees.EmployeesList.Count == 0)
        //    {
        //        return NotFound("Not Found");
        //    }
        //    else
        //    {
        //        return Ok(employees.EmployeesList);
        //    }
           
        //}

        // GET api/<EmployeesController>/5

        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))] //resposta sucesso
        [ProducesResponseType(StatusCodes.Status404NotFound)] //resposta de insucesso
        public IActionResult Get(int id)
        {
            Employee? e = employees.EmployeesList.Find(e => e.UserId == id);
            //? significa nullable, ou seja se não encontrar ninguém na lista com id igual, retorna null

            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return NotFound($"ID: {id} Not Found"); 
                //§ deixa-nos fazer id dentro do string
            }
        }


        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Employee? e = employees.EmployeesList.Find(e => e.UserId == id);

            if (e != null)
            {
                employees.EmployeesList.Remove(e);
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
        [Consumes(MediaTypeNames.Application.Json)] //o que ele quer receber, ex pdf, zip, etc. Neste caso está bloqueado para só receber Json
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Employee employee)
        {
            Employee lastEmp = employees.EmployeesList[employees.EmployeesList.Count - 1];

            if (employees.EmployeesList.Count == 0)
            {
                lastEmp.UserId = 1;
                employees.EmployeesList.Add(employee);
                return Ok(employee);
            }
            else
            {
                lastEmp.UserId = lastEmp.UserId + 1;
                employees.EmployeesList.Add(employee);
                return Ok(employee);
            }
        }
        // NÃO ESTÁ A DAR 
        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            var e = employees.EmployeesList.Find(e => e.UserId == id); //e onde e.userid é igual a id

            if (e == null)
            {
                return NotFound(id);
            }
            else
            {
                e.UserId = employee.UserId;
                e.JobTitle = employee.JobTitle;
                e.FirstName = employee.FirstName;
                e.LastName = employee.LastName;
                e.EmployeeCode = employee.EmployeeCode;
                e.Region = employee.Region;
                e.PhoneNumber = employee.PhoneNumber;
                e.EmailAddress = employee.EmailAddress;

                return Ok(e);
            }
        }
        //GET Region

        [HttpGet("region/{region}", Name = "GetByRegion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string region)
        {
            List<Employee> e = employees.EmployeesList.FindAll(e => e.Region == region);
            //? significa nullable, ou seja se não encontrar ninguém na lista com id igual, retorna null

            if (e != null)
            {
                return Ok(e);
            }
            else
            {
                return NotFound($"Region: {region} Not Found");
                //§ deixa-nos fazer id dentro do string
            }
        }

        //GET DOWNLOAD

        [HttpGet("download", Name = "GetDownload")]
        public IActionResult GetDownload()

        {
            string jsonAllEmps = JsonSerializer.Serialize<Employees>(employees);
            //pegar lista de funcionários que está em memória e passamos para string Json

            System.IO.File.WriteAllText("./JSON/allEmps.json", jsonAllEmps);

            try
            {
                               //namespace.class.function
                byte[] bytes = System.IO.File.ReadAllBytes("./JSON/allEmps.json");
                return File(bytes, "application/json", "Employees.json");
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

    }
}
