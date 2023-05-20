using DownNotifier.Entity.UrlDefinitions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DownNotifier.Entity
{
    public class DownNotifierDbContext : IdentityDbContext
    {
        public DownNotifierDbContext(DbContextOptions<DownNotifierDbContext> options) : base(options) { }

        public virtual DbSet<UrlDefinition> UrlDefinition { get; set; }
    }
}
