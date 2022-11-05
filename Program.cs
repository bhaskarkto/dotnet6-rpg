global using dotnet_rpg.Models;
using dotnet_rpg.Data; 
using dotnet_rpg.Services.CharecterService;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext> (options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharecterService, CharecterService>(); 

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
