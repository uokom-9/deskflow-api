using DeskFlow.API.Data;
using Microsoft.EntityFrameworkCore;
using DeskFlow.API.Repositories;
using DeskFlow.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Connection string do SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Controllers
builder.Services.AddControllers();

// Injeção de dependência para Repository e Service
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 6. Mapeia as rotas para que os Controllers consigam receber pedidos da internet
app.MapControllers();

app.Run();