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
            // 👈 This is the correct way to set the partition key with EF Core
            modelBuilder.Entity<IdentityUser>()
                .ToContainer("Identity") // The name of your container
                .HasPartitionKey(u => u.Id);

            // Remove this if you don't use IdentityRole
            modelBuilder.Entity<IdentityRole>()
                .ToContainer("IdentityRoles") // Or whatever your role container is named
                .HasPartitionKey(r => r.Id);
        }
    }
}