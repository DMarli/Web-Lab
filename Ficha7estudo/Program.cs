using Ficha7estudo;
using System.Text.Json;

    var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var employees = LoadEmployeesJSON();
var employee = LoadEmployeeJSON();

Employees LoadEmployeesJSON()
{
    var employeesJson = File.ReadAllText("Employees.Json");

    Employees e = JsonSerializer.Deserialize<Employees>(employeesJson);

    return e;

}

Employee LoadEmployeeJSON()
{
    var employeeJson = File.ReadAllText("Employee.Json");

    Employee e = JsonSerializer.Deserialize<Employee>(employeeJson);

    return e;

}

Employee emp1 = new Employee()

{
    UserId = 1,
    FirstName = "Test",
    LastName = "Name",
    EmailAdress = "email",
    JobTitle = "job",
    EmployeeCode = "123",
    PhoneNumber = "98765",
    Region = "FX"
};

Employee emp2 = new Employee()

{
    UserId = 2,
    FirstName = "Test2",
    LastName = "Name",
    EmailAdress = "email",
    JobTitle = "job",
    EmployeeCode = "123",
    PhoneNumber = "98765",
    Region = "PT"
};

Employee emp3 = new Employee()

{
    UserId = 3,
    FirstName = "Test3",
    LastName = "Name",
    EmailAdress = "email",
    JobTitle = "job",
    EmployeeCode = "123",
    PhoneNumber = "98765",
    Region = "PT"
};


Employees emps = new Employees();

emps.EmployeesList.Add(emp1);
emps.EmployeesList.Add(emp2);
emps.EmployeesList.Add(emp3);

var jsonEmp = JsonSerializer.Serialize<Employee>(emp1);
File.WriteAllText("jsonEmp.json", jsonEmp);

var jsonEmps = JsonSerializer.Serialize<Employees>(emps);
File.WriteAllText("jsonEmps.json", jsonEmps);

app.MapGet("/employees", () =>
{
    if (employees != null)
    {
        return Results.Ok(employees.EmployeesList);
    }
    else
    {
        return Results.NotFound("Not Found");
    }
});

app.MapPost("/employees", (Employee emp) =>
{
    if (employees.EmployeesList.Count == 0)

    {
        emp.UserId = 1;
        employees.EmployeesList.Add(emp);
    }

    else 
    {
        var lastEmp = employees.EmployeesList[employees.EmployeesList.Count - 1];
        emp.UserId = lastEmp.UserId + 1;
        employees.EmployeesList.Add(emp);
    }
    return Results.Created($"/employees/{emp.UserId}", emp);

});

app.MapDelete("/employees/{id}", (int id) =>
{
    Employee empl = employees.EmployeesList.Find(empl => empl.UserId == id);

    if (empl != null)
    {
        employees.EmployeesList.Remove(empl);
        return Results.Ok("ID " + id + " was removed");
    }
    else
    {
        return Results.NotFound("ID" + id + "Not Found");
    }
});

app.MapGet("/employees/{id:int}", (int id) =>
{
    Employee empl = employees.EmployeesList.Find(empl => empl.UserId == id);

    if (empl != null)
    {
        return Results.Ok(empl);
    }
    else
    {
        return Results.NotFound("ID" + id + "Not Found");
    }
});

app.MapPut("/employees/{id}", (int id, Employee empPut) =>
{
    Employee e = employees.EmployeesList.Find(e => e.UserId == id);

    if (e != null)
    {
        employees.EmployeesList.Remove(e);
        employees.EmployeesList.Add(empPut);
        return Results.Ok(empPut);
    }
    else
    {
        return Results.NotFound("ID " + id + " Not Found");
    }
});

app.MapGet("/employees/{region}", (string region) =>
{
    List<Employee> e = employees.EmployeesList.FindAll(e => e.Region == region);

    if (e != null)
    {
        return Results.Ok(e);
    }

    return Results.NotFound(String.Format("Region: {0} not found", region));

});

app.MapGet("/employees/download", () =>
{
    var jsonEmps = JsonSerializer.Serialize<Employees>(emps);
    File.WriteAllText("jsonEmps.json", jsonEmps);

    try
    {
        byte[] bytes = File.ReadAllBytes("jsonEmps.json");
        return Results.File(bytes, null, "Employees.json");
    }
    catch (FileNotFoundException e)
    {
        return Results.NotFound(e.Message);
    }
});

app.Run();

