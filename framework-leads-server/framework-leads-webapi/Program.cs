using FrameworkLeads.Data;
using FrameworkLeads.Helpers;
using FrameworkLeads.Infrastructure.Helpers;
using FrameworkLeads.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ILeadsService, LeadsService>();
builder.Services.AddTransient<ILeadsRepository, LeadsRepository>();
builder.Services.AddTransient<IDapperBase, DapperBase>();

builder.Services.AddControllers();
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// error handler global
app.UseMiddleware<MiddlewareErrorHandler>();

// inicializador do typeMapper para o dapper
TypeMapper.Initialize("FrameworkLeads.Models");

app.Run();
