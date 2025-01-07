using MarketToolsV3.ConfigurationManager.Models;
using Proto.Contract.Identity;

namespace MarketToolsV3.ApiGateway;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection AddAuthGrpcClient(this IServiceCollection collection,
        ServicesAddressesConfig servicesAddressesConfig)
    {
        string? identityGrpcAddress = servicesAddressesConfig.Identity.Grpc;

        if (string.IsNullOrEmpty(identityGrpcAddress) == false)
        {
            collection.AddGrpcClient<Auth.AuthClient>(c =>
            {
                c.Address = new Uri(identityGrpcAddress);
            });

            collection.AddGrpcClient<Session.SessionClient>(c =>
            {
                c.Address = new Uri(identityGrpcAddress);
            });
        }

        return collection;
    }
}