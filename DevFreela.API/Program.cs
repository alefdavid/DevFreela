using DevFreela.Application;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Infrastructure;
using DevFreela.Infrastructure.Context;
using DevFreela.Infrastructure.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevFreela.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Alef David",
            Url = new Uri("https://www.linkedin.com/in/alefdavid/")
        }
    });
});

// Context
var connectionString = builder.Configuration.GetConnectionString("DBDevFreela");
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

// Dependencies
builder.Services.RegisterApplicationDependencies();
builder.Services.RegisterInfrastrutureDependencies();

// Mapping
builder.Services.AddSingleton(AutoMapperConfig.Initialize());

builder.Services.AddControllers();

// Authentication and Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

// Configure exception handling
app.UseExceptionHandler("/error"); 
app.Map("/error", (HttpContext context) =>
{
context.Response.StatusCode = 500; 
return context.Response.WriteAsync("An error occurred while processing your request."); 
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();