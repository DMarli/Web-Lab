using System.Text.Json;
using Web6;

// ------------------- Swagger -------------------

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ------------------- Swagger -------------------

app.UseHttpsRedirection();

var people = LoadJsonData();
//método devolve instância da classe people, então tenho de guardar esse resultado nalgum sítio
//se colocasse LoadJsonData(); sem não ia devolver nada

app.MapGet("/people", () =>
{
    if (people != null)
    {
        return Results.Ok(people);
        //Results é uma Classe com os códigos HTML, ex. o 404
        //porque já instanciámos o LoadJsonData acima
    }

    else
    {
        return Results.NotFound("Not found");
    }

    //return people != null ? Results.Ok(people) : Results.NotFound("Not Found");
});

app.MapGet("/people/{id}", (int id) =>
{
    Person person = people.PersonList.Find(person => person.Id == id);

    if (person != null)
    {
        return Results.Ok(person);
    }
    else
    {
        return Results.NotFound("ID " + id + " Not found");
    }

    //Person person = people.PersonList.Find(person => person.Id == id);
    //return person != null ? Results.Ok(person) : Results.NotFound("Not Found");

});

app.MapPost("/people", (Person person) =>
{
    people.PersonList.Add(person);
    return Results.Ok(person);
});

app.MapDelete("/people/{id}", (int id) =>
{

    Person person = people.PersonList.Find(person => person.Id == id);

    if (person != null)
    {
        people.PersonList.Remove(person);
        return Results.Ok(person.Id);
    }

    return Results.NotFound("ID " + id + " Not found");

    //ou
    //for (int i = 0; i < people.PersonList.Count; i++) //Count é o tamanho do Array
    //{
    //    Person person = people.PersonList[i];
    //variável person vai guardar o valor que for encontrado no índice i
    //    if (person.Id == id)
    //    {
    //        people.PersonList.RemoveAt(i);
    //        return Results.Ok(id);
    //    }
    //}
    //return Results.NotFound($"ID: {id} not found!");

});

app.MapPut("/people/{id}", (int id, Person newPerson) =>
{
    Person person = people.PersonList.Find(person => person.Id == id);
    //person - todos os elementos da lista
    //vai procurar dentro dos elementos da lista uma pessoa que tenha o id
    //como aquele que estou a passar como argumento

    if (person != null)
    {
        people.PersonList.Remove(person);
        people.PersonList.Add(newPerson);
        return Results.Ok(newPerson);
    }

    return Results.NotFound("ID " + id + " Not found");

    //Person p = people.PersonList.Find(p => p.Id == id);

    //if (p == null)
    //{
    //    return Results.NotFound(id);
    //}
    //else
    //{
    //    p.Firstname = person.Firstname; //modificar primeiro nome.
    //    p.Lastname = person.Lastname;
    //    p.Profession = person.Profession;
    //    p.Gender = person.Gender;
    //    p.Age = person.Age;
    //    return Results.Ok(p);
    //}
});

People LoadJsonData()
{
 
    var jsonData = File.ReadAllText("data.json");
    //Classe C# com método estático para ler texto
    //Devolve o conteúdo para uma string do ficheiro Data.json

    People p = JsonSerializer.Deserialize<People>(jsonData);
    //Deserialize People no ficheiro jsonData, que é conteúdo convertido em objeto

    return p;
}

app.Run();