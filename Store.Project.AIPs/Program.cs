
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Project.APIs.Errors;
using Store.Project.APIs.Helper;
using Store.Project.APIs.Middlewares;
using Store.Project.Core;
using Store.Project.Core.Mapping;
using Store.Project.Core.Services.Contract;
using Store.Project.Repository;
using Store.Project.Repository.Data;
using Store.Project.Repository.Data.Contexts;
using Store.Project.Service.Services.Products;

namespace Store.Project.AIPs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDependency(builder.Configuration);
           


            var app = builder.Build();

            await app.ConfigureMiddlewareAsync();

            app.Run();
        }
    }
}
