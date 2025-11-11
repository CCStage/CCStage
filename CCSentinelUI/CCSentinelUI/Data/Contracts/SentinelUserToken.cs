using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelUserToken : IdentityUserToken<Guid>
    {
        /// <summary>
        /// Gets or sets the primary key for this user role.
        /// </summary>
        [PersonalData]
        public virtual Guid Id { get; set; } = default!;
    }
}
