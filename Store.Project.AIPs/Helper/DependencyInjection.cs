using Microsoft.EntityFrameworkCore;
using Store.Project.Core.Services.Contract;
using Store.Project.Core;
using Store.Project.Repository;
using Store.Project.Repository.Data.Contexts;
using Store.Project.Service.Services.Products;
using Store.Project.Core.Mapping;
using Microsoft.AspNetCore.Mvc;
using Store.Project.APIs.Errors;
using Store.Project.Core.Repositories.Contract;
using Store.Project.Repository.Repositories;
using StackExchange.Redis;
using Store.Project.Core.Mapping.Basket;

namespace Store.Project.APIs.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);

            return services;
        }
        private static IServiceCollection AddBuiltInService (this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {

                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            return services;
        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            return services;
        }

        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {

           services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                              .SelectMany(P => P.Value.Errors)
                                              .Select(E => E.ErrorMessage)
                                              .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            }
            );
            return services;
        }

        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
               var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });


            
            return services;
        }
    }
}
