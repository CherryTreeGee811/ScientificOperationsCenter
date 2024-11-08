using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.DAL;


namespace ScientificOperationsCenter.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ScientificOperationsCenterContext dbContext =
                scope.ServiceProvider.GetRequiredService<ScientificOperationsCenterContext>();

            dbContext.Database.Migrate();
        }
    }
}
