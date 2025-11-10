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
        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OpenIdApplication>(builder =>
            {
                builder.HasKey(x => x.ClientId);
                builder.ToTable("OpenIddictApplications");


                builder.HasDiscriminator<string>("Discriminator")
                       .HasValue<OpenIdApplication>("OpenIddictEntityFrameworkCoreApplication");
            });


            modelBuilder.Entity<OpenIdScope>(builder =>
            {
                builder.HasKey(x => x.Name);
                builder.ToTable("OpenIddictScopes");


                builder.HasDiscriminator<string>("Discriminator")
                       .HasValue<OpenIdScope>("OpenIddictEntityFrameworkCoreScope");
            });


            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.Id);
                builder.ToTable("Users");
            });
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
    }

    // Nieuwe User entity voor overzichtspagina
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }

        public bool LockoutEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
    }

}
