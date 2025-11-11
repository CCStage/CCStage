using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the permissions associated with the
        /// current user, serialized as a JSON array.
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Json)]
        public virtual string? Permissions { get; set; }
        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
