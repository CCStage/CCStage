using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelUserClaim : IdentityUserClaim<Guid>
    {
        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
