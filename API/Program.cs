using API.Extensions;
using API.Middlewares;
using Aplication.Helpers.MappingProfiles;
using Application.Features.Users;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(MappingProfiles));


builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

builder.Services.AddMediatR(typeof(CreateUser.CreateUserCommand).Assembly);//configuracao do mediatr

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(x=>
    x.RegisterValidatorsFromAssemblyContaining<CreateUser.CreateUserCommand>()
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options=>
{
    options.AddPolicy("MyCors",builder=>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyCors");
app.MapControllers();
app.Run();