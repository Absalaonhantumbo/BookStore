﻿using Aplication.Interfaces;
using Infrastruture.Data;
using Infrastruture.Services;

namespace API.Extensions;

public static  class ApplicationServiceExtensions
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserAcessor, UserAcessor>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }
}