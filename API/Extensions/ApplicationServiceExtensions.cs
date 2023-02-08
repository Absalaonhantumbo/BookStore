using Aplication.Interfaces;
using Application.Interfaces;
using Infrastruture.Data;
using Infrastruture.Services;
using Persistence.Seed;

namespace API.Extensions;

public static  class ApplicationServiceExtensions
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserAcessor, UserAcessor>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IListOfAuthorsService, ListOfAuthorsService>();
        
        return services;
    }
}