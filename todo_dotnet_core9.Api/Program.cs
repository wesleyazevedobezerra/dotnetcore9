using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using todo_dotnet_core9.Infra.Bootstraper;
using todo_dotnet_core9.Infra.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.



// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() // Permite qualquer origem
               .AllowAnyHeader() // Permite qualquer cabeçalho
               .AllowAnyMethod(); // Permite qualquer método (GET, POST, etc.)
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NativeInjectorBootstrapper.RegisterServices(builder.Services);



builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlite(configuration.GetConnectionString("BaseIdentity"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

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

app.Run();
