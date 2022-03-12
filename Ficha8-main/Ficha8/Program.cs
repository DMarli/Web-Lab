using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

Employees emps = new Employees();

//pomos string - ao colocar o rato na função vemos que devolve uma string,
//então criamos uma variável para a guardar

string jsonEmps = JsonSerializer.Serialize<Employees>(emps);
File.WriteAllText("TestEmps.json", jsonEmps);

//File.WriteAllText("Test.json", json2); 
//File.WriteAllText("Test.json", json3); 
//Criou em ficheiro Json. Em vez de fazermos 1 para todos, podemos criar lista

Employee emp = new Employee();



app.UseHttpsRedirection();

Employees allEmps = LoadEmployeesJSON(); //para guardar a variável
Employee allEmp = LoadEmployeeJSON();


app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

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
        return Results.NotFound(String.Format("ID:" + id + "not found!"));

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


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}