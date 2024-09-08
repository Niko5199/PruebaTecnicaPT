using Microsoft.EntityFrameworkCore;
using PruebaTecnicaPT.Data;
using PruebaTecnicaPT.EmployeesMapper;
using PruebaTecnicaPT.Repository;
using PruebaTecnicaPT.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


string server = Environment.GetEnvironmentVariable("SQLSERVER_SERVER");
string database = Environment.GetEnvironmentVariable("SQLSERVER_DATABASE");
string user = Environment.GetEnvironmentVariable("SQLSERVER_USER");
string password = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD");


string connectionString = builder.Configuration.GetConnectionString("ConnectionSql")
    .Replace("PLACEHOLDER_SERVER", server)
    .Replace("PLACEHOLDER_DATABASE", database)
    .Replace("PLACEHOLDER_USER", user)
    .Replace("PLACEHOLDER_PASSWORD", password);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString));


builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddAutoMapper(typeof(EmployeesMapper));

// Config Cors
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    build.WithExposedHeaders("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddControllers();
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

app.UseCors("PolicyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
