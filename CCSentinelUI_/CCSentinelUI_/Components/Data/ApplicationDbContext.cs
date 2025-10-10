using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OpenIdApplication> OpenIddictApplications { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpenIdApplication>()
                .HasKey(x => x.ClientId);
        }
    }

    public class OpenIdApplication
    {
        public string ClientId { get; set; } = string.Empty;
        public string? ClientType { get; set; }
        public string? DisplayName { get; set; }
        public string? Permissions { get; set; }
        public string? Properties { get; set; }
        public string? RedirectUris { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = Array.Empty<byte>();
    }

}
