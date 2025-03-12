using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Application.Behaviors;
using WB.Seller.Companies.Application.Mappers;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application
{
    public static class ServicesRegisterExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFromMapper<CompanyEntity, CompanyDto>, CompanyTransferFromEntityMapper>();

            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(DeepValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            return serviceCollection;
        }
    }
}
