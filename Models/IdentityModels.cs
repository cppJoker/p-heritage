#define l

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Projet_Heritage.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SerialKey> SerialKeys { get; set; }
        public DbSet<PlatformSetting> PlatformSettings { get; set; }
#if l
        public ApplicationDbContext()
            : base(
                    "LocalDB", throwIfV1Schema: false)
        {
        }
#elif t
        public ApplicationDbContext()
            : base(
                    "TestDB", throwIfV1Schema: false)
        {
        }
#elif p
        public ApplicationDbContext()
            : base(
                    "ProductionDB", throwIfV1Schema: false)
        {
        }

#endif

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}