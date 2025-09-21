using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AspNetCore.Identity.CosmosDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LoginServer.Azure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
          : base(options) { }
    }
}