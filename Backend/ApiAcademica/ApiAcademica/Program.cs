using Api.Filters;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Extensions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using Application.Behaviors;
using Infrastructure.DataSource;
using System.Reflection;
using Shared.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });

});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomainServices();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDomainServices();
Assembly data = Assembly.Load(ProjectConst.Application);

builder.Services.AddValidatorsFromAssembly(data);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(data));


// 3. Pipeline behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

// 4. AutoLoadServices para repositorios y Dapper/EF


builder.Configuration.AddUserSecrets<Program>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
