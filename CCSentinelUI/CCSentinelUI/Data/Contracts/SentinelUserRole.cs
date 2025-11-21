using Microsoft.AspNetCore.Identity;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelUserRole : IdentityUserRole<Guid>
    {
        public Guid Id { get; set; }

        public SentinelUser User { get; set; }
        public SentinelRole Role { get; set; }
    }
}
