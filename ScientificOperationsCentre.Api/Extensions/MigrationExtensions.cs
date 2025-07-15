using Microsoft.EntityFrameworkCore;
using ScientificOperationsCentre.Api.DAL;


namespace ScientificOperationsCentre.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ScientificOperationsCentreContext dbContext =
                scope.ServiceProvider.GetRequiredService<ScientificOperationsCentreContext>();

            dbContext.Database.Migrate();
        }
    }
}
