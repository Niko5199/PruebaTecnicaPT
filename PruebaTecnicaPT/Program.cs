using Microsoft.EntityFrameworkCore;
using PruebaTecnicaPT.Data;
using PruebaTecnicaPT.EmployeesMapper;
using PruebaTecnicaPT.Repository;
using PruebaTecnicaPT.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSql")));


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
