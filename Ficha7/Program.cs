using Ficha7;
using System.Text.Json;

//Add Range, várias de uma vez

Employee emp1 = new Employee()

{
  UserId = 5,
  JobTitle = "Rececionista",
  FirstName = "Fábio",
  LastName = "Leou",
  EmployeeCode = "E7",
  Region = "PT",
  PhoneNumber = "452453",
  EmailAddress = "teaaf@learningcontainer.com"
};

Employee emp2 = new Employee()

{
  UserId = 6,
  JobTitle = "Jogador",
  FirstName = "Thoro",
  LastName = "Fewf",
  EmployeeCode = "E8",
  Region = "PT",
  PhoneNumber = "3433453",
  EmailAddress = "ewtwew@learningcontainer.com"
};

Employee emp3 = new Employee()

{
    UserId = 7,
    JobTitle = "Puladora",
    FirstName = "Goria",
    LastName = "Goriu",
    EmployeeCode = "E9",
    Region = "PT",
    PhoneNumber = "455453",
    EmailAddress = "gugugu@learningcontainer.com"
};

//podíamos fazer de outra forma emp3.UserId =, e igual em todos

Employees emps = new Employees();

emps.EmployeesList.Add(emp1);
emps.EmployeesList.Add(emp2);
emps.EmployeesList.Add(emp3);

//serializar é tranformar uma instância em Json
string json = JsonSerializer.Serialize<Employee>(emp1);
File.WriteAllText("Test.json", json);

//pomos string - ao colocar o rato na função vemos que devolve uma string,
//então criamos uma variável para a guardar

string jsonEmps = JsonSerializer.Serialize<Employees>(emps);
File.WriteAllText("TestEmps.json", jsonEmps);

//File.WriteAllText("Test.json", json2); 
//File.WriteAllText("Test.json", json3); 
//Criou em ficheiro Json. Em vez de fazermos 1 para todos, podemos criar lista

Employee emp = new Employee();


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

Employees allEmps = LoadEmployeesJSON(); //para guardar a variável
Employee allEmp = LoadEmployeeJSON();

app.MapGet("/employees", () =>
{
    if (allEmps == null)
    {
        return Results.NotFound("Not found.");
    }
    else
    {
        return Results.Ok(allEmps);
    }
});

app.MapGet("/employees/{id:int}", (int id) => //id como paramêtro
{
    Employee emp = null;

    //podemos fazer com find
    for (int i = 0; i < allEmps.EmployeesList.Count; i++)
    {
        Employee employee = allEmps.EmployeesList[i];

        if (employee.UserId == id)
        {
            emp = employee;
        }
      
    }
    //ou
    //Employees emp = employees.EmployeesList.Fina(e => e.UserID == id)

    if (emp == null)
    { 
        return Results.NotFound($"ID: {id} not found!");
    }
    else
    {
        return Results.Ok(emp);
    }
});



app.MapDelete("/employees/{id}", (int id) => //id como paramêtro
{
    //for (int i = 0; i < allEmps.length; i++)
    //{
    //    var e = allEmps.EmployeesList[i];
    //}


    int removed = allEmps.EmployeesList.RemoveAll(e => e.UserId == id);

    //remover Manager (e => e.JobTitle == "Manager");
    //remover todos menos Manager (e => e.JobTitle != "Manager");

    if (removed == 0)
    {
        return Results.NotFound(String.Format ("ID:" + id + "not found!"));

        //ou $"/employees/{id}";
        //ou String.Format("/people/{0}", id);
        //ou "/employee/" + id
    }
    else
    {
        return Results.Ok(removed);
    }
});



app.MapPost("/employees", (Employee newEmp) => //mais correto é create, estamos a criar um recurso
{
    var firstEmp = allEmps.EmployeesList[0];
    //var firstEmp = allEmps.EmployeesList.FirstOrDefault();
    //var lastEmp = allEmps.EmployeesList[allEmps.EmployeesList.Count - 1];
    //var lastEmp = allEmps.EmployeesList[allEmps.EmployeesList.LastOrDefault()]; //Só devolve se houver pelo menos 1 elemento

    if (allEmps.EmployeesList.Count == 0)
    {
        newEmp.UserId = 1;
        allEmps.EmployeesList.Add(newEmp);
    }
    else
    {
        var lastEmp = allEmps.EmployeesList[allEmps.EmployeesList.Count - 1];
        newEmp.UserId = lastEmp.UserId + 1;
        allEmps.EmployeesList.Add(newEmp);
    }
    return Results.Ok(newEmp);
});




//criar primeiro aqui, vai dar erro e aparecer para gerar classe
Employee LoadEmployeeJSON()
{
    string text = File.ReadAllText("Employee.json");
    //também pode ser var. ReadAll é para ler

    Employee emp = JsonSerializer.Deserialize<Employee>(text);

    return emp;
}


Employees LoadEmployeesJSON()

{
    //ler Json e converter em instância
    string text = File.ReadAllText("Employees.json");

    Employees emps = JsonSerializer.Deserialize<Employees>(text);

    return emps;
}



app.MapPut("/employees/{id}", (int id, Employee employee) =>

{
    var e = allEmps.EmployeesList.Find(e => e.UserId == id); //e onde e.userid é igual a id

    if (e == null)
    {
        return Results.NotFound(id);
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

        return Results.Ok(e);
    }
});

app.MapGet("/employees/{region}", (string region) =>
//id como paramêtro
//"/employees/region/{region}" - não é necessário porque colocámos id:region no primeiro get
//pomos region porque estava a fazer conflito com o id
{
    List<Employee> emps = allEmps.EmployeesList.FindAll(emps => emps.Region == region); //e onde e.userid é igual a id

    if (emps.Count == 0)
    {
        return Results.NotFound(region + "Not found");
    }
    else
    {
        return Results.Ok(emps);
    }
});

//nao está correto
app.MapGet("/employees/download", () =>
{
    byte[] bytes = File.ReadAllBytes("employees.json");

    return Results.File(bytes, null, "employees.json");

});

app.Run(); //fica sempre no final para o programa correr

