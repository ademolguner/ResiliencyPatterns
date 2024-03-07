using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet;
using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Implementations;
using ResiliencyPatterns.SpinningWheels.Api.Clients.Wallet.Interfaces;
using ResiliencyPatterns.SpinningWheels.Api.Configurations.Settings;
using ResiliencyPatterns.SpinningWheels.Api.Services;
using ResiliencyPatterns.SpinningWheels.Api.Services.Interfaces;
using ResiliencyPatterns.SpinningWheels.Api.StrategyOptions;

namespace ResiliencyPatterns.SpinningWheels.Api;

public static class ServiceDependencyRegistration
{
    
    public static void DependencyRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureExternalServiceSettings(configuration);
        services.ServiceRegistration();
        services.AddClientDependencies(configuration);
    }


    private static void ConfigureExternalServiceSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExternalServiceSettings>(configuration.GetSection("ExternalServiceSettings"));
    }

    private static void ServiceRegistration(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWalletClient, WalletClient>();
    }

    private static IServiceCollection AddClientDependencies(this IServiceCollection services, 
                                                                 IConfiguration configuration)
    {
        var httpClients = services.AddHttpClient();
        httpClients.RegisterWalletClient(configuration);
        return services;
    }

    private static void RegisterWalletClient(this IServiceCollection services, 
                                                  IConfiguration configuration)
    {
       services
           .AddWalletClient(configuration)
           .AddTransientHttpErrorPolicy(_=>ResiliencyStrategies.GetRetryPolicy());
    }
}