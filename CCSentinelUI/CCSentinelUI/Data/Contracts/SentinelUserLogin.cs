using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CCSentinelUI.Data.Contracts
{
    public class SentinelUserLogin : IdentityUserLogin<Guid>
    {
        /// <summary>
        /// Gets or sets the primary key for this user login.
        /// </summary>
        [PersonalData]
        public virtual Guid Id { get; set; } = default!;
        [Timestamp]
        public byte[] Version { get; set; } = [];
    }
}
