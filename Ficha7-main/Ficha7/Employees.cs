namespace Ficha7
{
    public class Employees
    {
        public Employees()
        {
           EmployeesList = new List<Employee>(); 
        }
        public List<Employee> EmployeesList { get; set; } //se este estiver no json diferente, não carrega. Json tem de estar EmployeesList
        //lista não está inicializada, está null
    }

}
