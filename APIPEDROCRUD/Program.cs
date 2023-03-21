using Microsoft.EntityFrameworkCore;
using APIPEDROCRUD.Models.Cadastro_1;
using APIPEDROCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString_1 = builder.Configuration.GetConnectionString("BDCadastro");
var connectionString_2 = builder.Configuration.GetConnectionString("LoginCadastro");

builder.Services.AddDbContext<BdcadastroContext>(option =>
option.UseSqlServer(connectionString_1)
);

builder.Services.AddDbContext<LoginContext>(option =>
option.UseSqlServer(connectionString_2)
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.

app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
