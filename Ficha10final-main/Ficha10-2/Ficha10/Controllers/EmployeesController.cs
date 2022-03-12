using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace Ficha10.Controllers
{
    [Route("[controller]")]
    //EmployeesController vai ser rota Employees, se tiver /api/[controller], vai ter api antes
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Consumes(MediaTypeNames.Application.Json)]
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

        [HttpGet("download", Name = "Download")]
        public IEnumerable<Employee> GetDownload()

        {
            // Save the current employee list to a file
            string jsonAllEmps = JsonSerializer.Serialize<Employees>(employees);
            System.IO.File.WriteAllText("Employees.json", jsonAllEmps);

            try
            {
                               //namespace.class.function
                byte[] bytes = System.IO.File.ReadAllBytes("./JSON/allEmps.json");
                return Results.File(bytes, null, "Employees.json");
            }
            catch (FileNotFoundException e)
            {
                return (IEnumerable<Employee>)NotFound(e.Message);
            }

        }

    }
}
