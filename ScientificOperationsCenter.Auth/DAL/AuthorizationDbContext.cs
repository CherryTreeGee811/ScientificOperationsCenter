using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ScientificOperationsCenter.Auth.DAL
{
    public class AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
        : IdentityDbContext<IdentityUser>(options)
    {
        public async Task SeedData(UserManager<IdentityUser> userManager)
        {
            var soTestUser = await userManager.FindByNameAsync("sciops_test");
            if (soTestUser == null)
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

            var gcTestUser = await userManager.FindByNameAsync("ground_control_sa");
            if (gcTestUser == null)
            {
                var newGCServiceAccountUser = new IdentityUser()
                {
                    UserName = "ground_control_sa",
                    Email = "groundcontrolsa@gmail.com",
                    NormalizedUserName = "GROUND_CONTROL_SA",
                    NormalizedEmail = "GROUND_CONTROL_SA@GMAIL.COM",
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(newGCServiceAccountUser, "ExploreSpace223*");
            }
        }


        public new DbSet<IdentityUser> Users { get; set; }
    }
}
