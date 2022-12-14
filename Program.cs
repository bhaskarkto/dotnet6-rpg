global using dotnet_rpg.Models;
using dotnet_rpg.Data; 
using dotnet_rpg.Services.CharecterService;
using dotnet_rpg.Services.CharecterService.WeaponService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext> (options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme{
            Description = "Standard Authorization header using the Bearer Scheme, e.g. \"bearer {token} \"",
            In = ParameterLocation.Header, 
            Name = "Authorization", 
            Type = SecuritySchemeType.ApiKey 
        });
        c.OperationFilter<SecurityRequirementsOperationFilter>(); 
    }
);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharecterService, CharecterService>(); 
builder.Services.AddScoped<IAuthRepository, AuthRepository>(); 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>   
    {  
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)), 
                ValidateIssuer = false, 
                ValidateAudience = false
        }; 

    }); 

//builder.Services.AddHttpContextAccessor(); 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IWeaponService, WeaponService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
