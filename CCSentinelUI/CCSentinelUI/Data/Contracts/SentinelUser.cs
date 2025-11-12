using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class SentinelUser : IdentityUser<Guid>
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [StringSyntax(StringSyntaxAttribute.Json)]
    public virtual string? Permissions { get; set; }

    [Timestamp]
    public byte[] Version { get; set; } = [];

    public override string UserName { get; set; } = string.Empty;
    public override string Email { get; set; } = string.Empty;
    public override string? NormalizedUserName { get; set; } = string.Empty;
    public override string? NormalizedEmail { get; set; } = string.Empty;
    public override string? PasswordHash { get; set; }

    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool EmailConfirmed { get; set; }
}
