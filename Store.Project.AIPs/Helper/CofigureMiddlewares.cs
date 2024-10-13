using Store.Project.AIPs;
using Store.Project.APIs.Middlewares;
using Store.Project.Repository.Data.Contexts;
using Store.Project.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.Project.APIs.Helper
{
    public static class CofigureMiddlewares
    {
        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are An Error During The Migration. ");

            }

            app.UseMiddleware<ExceptionMiddleware>(); // Configure user defined middleware  [ExceptionMiddelware]

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}
