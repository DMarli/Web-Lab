using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Ficha10.Controllers
{
    [Route("api/[controller]")]
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

        //Posso devolver qualquer tipo de Lista com INumerable
        public IEnumerable<Employee> Get()

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

        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
    }
}
