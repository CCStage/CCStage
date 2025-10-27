using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }


        public DbSet<OpenIdApplication> OpenIddictApplications { get; set; } = default!;
        public DbSet<OpenIdScope> OpenIddictScopes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpenIdApplication>().HasKey(x => x.ClientId);
            modelBuilder.Entity<OpenIdScope>().HasKey(x => x.Name);
        }
    }

    public class OpenIdApplication
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ClientId { get; set; } = string.Empty;
        public string? ClientType { get; set; }
        public string? DisplayName { get; set; }
        public string? Permissions { get; set; }
        public string? Properties { get; set; }
        public string? RedirectUris { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = Array.Empty<byte>();
    }
    public class OpenIdScope
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? Resources { get; set; }

        //   [Timestamp]
        //  public byte[] Version { get; set; } = Array.Empty<byte>();
    }
}
