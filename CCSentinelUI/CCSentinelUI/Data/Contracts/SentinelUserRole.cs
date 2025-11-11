using Microsoft.AspNetCore.Identity;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelUserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// Gets or sets the primary key for this user role.
        /// </summary>
        [PersonalData]
        public virtual Guid Id { get; set; } = default!;
    }
}
