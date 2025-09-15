using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AspNetCore.Identity.CosmosDb;

namespace LoginServer.Azure
{
    public class ApplicationDbContext : CosmosIdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
          : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDiscriminatorInJsonIds();
        }
    }
}