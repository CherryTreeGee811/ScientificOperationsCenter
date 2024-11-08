using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AuthorizationServer.DAL
{
    public class AuthorizationDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) :
        base(options)
        { }


        public async Task SeedData(UserManager<IdentityUser> userManager)
        {
            var testUser = await userManager.FindByNameAsync("sciops_test");
            if (testUser == null)
            {
                var newTestUser = new IdentityUser
                {
                    UserName = "sciops_test",
                    Email = "sciops@gmail.com",
                    NormalizedUserName = "SCIOPS_TEST",
                    NormalizedEmail = "SCIOPS@GMAIL.COM",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(newTestUser, "Hello123*");
            }
        }


        public DbSet<IdentityUser> Users { get; set; }
    }
}
