using EstudoFicha12.Data;
using EstudoFicha12.Models;
using EstudoFicha12.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependence Injection
builder.Services.AddDbContext<LibraryContext>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Create DB
app.CreateDbIfNotExists();

app.Run();

//WEB Core API - Instalar Microsoft Entity Framework e MySQL Entity Framework e Swashbuckle.AspNet Core