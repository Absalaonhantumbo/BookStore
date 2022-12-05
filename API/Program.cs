using API.Extensions;
using API.Middlewares;
using Aplication.Helpers.MappingProfiles;
using Application.Features.Users;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder("Bearer")
        .RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMemoryCache();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

// Add Application Extensions
builder.Services.AddAplicationServices();
builder.Services.AddSwaggerDocumentation();

//configuration of mediatr
builder.Services.AddMediatR(typeof(CreateUser.CreateUserCommand).Assembly);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(x=>
    x.RegisterValidatorsFromAssemblyContaining<CreateUser.CreateUserCommand>()
);


builder.Services.AddCors(options=>
{
    options.AddPolicy("MyCors",builder=>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

//Seed Database
SeedDatabase();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyCors");
app.MapControllers();
app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        dbInitializer.Initilize();
    }
}