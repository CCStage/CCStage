using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelRoleClaim : IdentityRoleClaim<Guid>
    {
        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
