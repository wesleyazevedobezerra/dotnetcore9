using Microsoft.Extensions.Configuration;
using todo_dotnet_core9.Infra.Bootstraper;
using todo_dotnet_core9.Infra.Context;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Metrics HTTP Client
builder.Services.UseHttpClientMetrics();

// Configuração de CORS - permite qualquer origem, cabeçalho e método
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.ASCII.GetBytes("6WQ6J7Z3Q2GxCvTUlXGLrBD5Xf8Auh6qx0CeQ8qqVNs=");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NativeInjectorBootstrapper.RegisterServices(builder.Services);

// Detecta se está rodando dentro do Docker pela variável de ambiente
var runningInDocker = Environment.GetEnvironmentVariable("RUNNING_IN_DOCKER") == "true";

string connectionString = runningInDocker
    ? configuration.GetConnectionString("BaseIdentityDocker")
    : configuration.GetConnectionString("BaseIdentity");

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlite(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

// Aplica migrations automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
    db.Database.Migrate();
}

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricServer();
app.UseHttpMetrics();

var error401Counter = Metrics.CreateCounter("http_response_401_total", "Número total de respostas HTTP 401");

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)
    {
        error401Counter.Inc();
    }
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
