// Program.cs serve para configurar o projeto, como por exemplo, a conexão com o banco de dados, os controllers e o Swagger

using DeskFlow.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection string do SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Ativa a tela visual do Swagger se o projeto estiver rodando em modo de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Mapeia as rotas para os Controllers conseguirem receber pedidos da internet
app.MapControllers();

app.Run();